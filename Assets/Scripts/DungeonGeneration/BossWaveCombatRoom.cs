using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaveCombatRoom : Room
{

    public GameObject[,] enemies = null;
    private GameObject reward = null;
    public GameObject teleporter;

    private bool active = false;
    private bool allenemiesSpawned = false;
    private const float SPAWN_DELAY = .7f;

    public int curWave = 1;
    private int numWaves = 3;

    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !active)
        {
            TryToStartRoom();
        }

        if (allenemiesSpawned)
        {
            bool enemiesRemaining = false;
            for (int i = 0; i < enemies.GetLength(1); i++)
            {
                if (enemies[curWave-1, i].activeSelf) { enemiesRemaining = true; break; };
            }
            if (!enemiesRemaining)
            {
                if (!NextWave())
                {
                    ClearRoom();
                    OpenDoors();
                    SpawnReward();
                }
            }
        }
    }

    protected override void ClearRoom()
    {
        base.ClearRoom();
        teleporter.SetActive(true);
    }

    protected override void SetupRoom()
    {
        return;
    }

    private bool NextWave()
    {
        if (curWave == numWaves)
        {
            return false;
        }
        else
        {
            curWave += 1;
            SpawnWave();
            return true;
        }
    }

    private void SpawnWave()
    {
        allenemiesSpawned = false;
        for (int i = 0; i < enemies.GetLength(1); i++)
        {
            StartCoroutine(SpawnEnemy(i, i * SPAWN_DELAY));
        }
    }

    protected override void StartRoom()
    {
        active = true;
        curWave = 1;
        SpawnWave();

    }

    IEnumerator SpawnEnemy(int index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        enemies[curWave-1, index].SetActive(true);
        if (index == enemies.GetLength(1) - 1)
        {
            allenemiesSpawned = true;
        }
    }

    public void SetReward(GameObject reward)
    {
        this.reward = reward;
    }

    private void SpawnReward()
    {
        if (reward != null)
        {
            reward.SetActive(true);
        }
    }

    public virtual void generateEnemies(GameObject[] enemyTypes, int numEnemies, int numWaves)
    {
        this.numWaves = numWaves;
        enemies = new GameObject[numWaves, numEnemies];
        for (int i = 0; i < numWaves; i++)
        {
            for (int j = 0; j < numEnemies; j++)
            {
                float roomSize = DungeonControllerModularRooms.ROOM_SIZE;
                float xPos = Random.Range(-roomSize / 2 + 2f, roomSize / 2 - 2f);
                float yPos = 0;
                float zPos = Random.Range(-roomSize / 2 + 2f, roomSize / 2 - 2f);
                Vector3 spawnLocation = transform.position + new Vector3(xPos, yPos, zPos);
                enemies[i, j] = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnLocation, Quaternion.identity, transform);
                enemies[i, j].SetActive(false);
            }
        }
    }
}
