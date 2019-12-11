using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public  float maxHealth = 100f;
    public float health;

    public List<Wand> wands;
    private int curWand;

    private float timeSinceLastShot;
    private float timeSinceOof = 0;

    private Transform wandContainer;

    private AudioSource audioSource;
    public AudioClip swapWandsAudio;
    public AudioClip newWandAudio;
    public AudioClip oof;

    private GameObject playerUI;

    private void Awake()
    {
        GameManager.player = gameObject;
    }

    private void Start()
    {
        playerUI = GameManager.playerUI;

        health = maxHealth;
        timeSinceLastShot = 0f;
        wands = new List<Wand>();

        audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;

        GameObject startingWand = GetComponentInChildren<Wand>().gameObject;
        wandContainer = startingWand.transform.parent;
        AddWand(startingWand);
        startingWand.SetActive(true);
        curWand = 0;
    }

    private void Update()
    {

        if (GameManager.gameOver)
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                GameManager.Restart();
            }
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                GameManager.ExitToMenu();
            }
            playerUI.GetComponent<Canvas>().enabled = false;
        } else {
            if (GameManager.isPaused) {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    GameManager.TogglePause();
                }
                if (OVRInput.GetDown(OVRInput.Button.Two))
                {
                    GameManager.ExitToMenu();
                }
            } else {
                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && timeSinceLastShot > wands[curWand].shotDelay) {
                    timeSinceLastShot = 0;
                    FireProjectile();
                }

                if (OVRInput.GetDown(OVRInput.Button.One)) {
                    CycleWandsForward();
                }

                if (OVRInput.GetDown(OVRInput.Button.Two)) {
                    CycleWandsBackwards();
                }
            }

            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                playerUI.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                playerUI.GetComponent<Canvas>().enabled = false;
            }
        }

        if (!GameManager.gameOver && OVRInput.GetDown(OVRInput.Button.Start))
        {
            GameManager.TogglePause();
        }
        timeSinceLastShot += Time.deltaTime;
        timeSinceOof += Time.deltaTime;
        
        
    }

    public void AddWand(GameObject wand)
    {
        if (wands.Count != 0) { audioSource.PlayOneShot(newWandAudio); }

        Wand wandScript = wand.GetComponent<Wand>();
        for (int i = 0; i < wands.Count; i++) //checks if the wand is a duplicate
        {
            if (wands[i].Equals(wandScript))
            {
                wands[i].LevelUp();
                Destroy(wand);
                return;
            }
        }

        wand.transform.parent = wandContainer;
        wand.transform.localPosition = Vector3.zero;
        wand.transform.localScale = Vector3.one;
        wand.transform.localRotation = Quaternion.identity;
        wands.Add(wand.GetComponent<Wand>());
        wand.SetActive(false);
    }

    public void CycleWandsForward()
    {
        SwapWands((curWand + 1) % wands.Count);
    }

    public void CycleWandsBackwards()
    {
        SwapWands((curWand + wands.Count - 1) % wands.Count);
    }

    private void SwapWands(int wandIndex)
    {
        if (wandIndex == curWand) return;
        audioSource.PlayOneShot(swapWandsAudio);
        wands[curWand].gameObject.SetActive(false);
        curWand = wandIndex;
        wands[curWand].gameObject.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (timeSinceOof > 2f || damage > 2)
        {
            audioSource.PlayOneShot(oof);
            timeSinceOof = 0;
        }

        if (health <= 0)
        {
            GameManager.EndGame();
        }
    }

    public void FireProjectile()
    {
        wands[curWand].SpawnProjectile();
    }

    public void Restart()
    {
        foreach (Wand w in wands)
        {
            if (w.id != 0)
            {
                Destroy(w.gameObject);
            }
        }
        curWand = 0;
        wands[curWand].gameObject.SetActive(true);
        health = maxHealth;
        transform.position = Vector3.zero;
    }

    public Wand getCurrentWand()
    {
        return wands[curWand];
    }
}
