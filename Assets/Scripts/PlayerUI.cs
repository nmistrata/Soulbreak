using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public RectTransform healthBar;
    public Text wandLevelText;
    public Text curFloorText;
    private const float HEALTHBAR_LENGTH = 680f;

    private void Awake()
    {
        GameManager.playerUI = gameObject;
    }
    private void Update()
    {
        Player p = GameManager.player.GetComponent<Player>();
        healthBar.offsetMax = new Vector2(-HEALTHBAR_LENGTH * (1 - p.health/p.maxHealth), healthBar.offsetMax.y);

        wandLevelText.text = "Wand Level " + p.getCurrentWand().GetLevel();

        curFloorText.text = "Floor " + GameManager.dungeon.curLevel;
    }
}
