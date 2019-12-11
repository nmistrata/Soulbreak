using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom : Room
{
    private List<GameObject> enemies = null;
    private GameObject reward = null;
    private bool active = false;
    private bool allEnemiesSpawned = false;
    private const float SPAWN_DELAY = .5f;

    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !active)
        {
            TryToStartRoom();
        }

        if (allEnemiesSpawned)
        {
            bool enemiesRemaining = false;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].activeSelf) { enemiesRemaining = true; break; };
            }
            if (!enemiesRemaining) { ClearRoom(); OpenDoors(); SpawnReward(); }
        }
    }

    protected override void SetupRoom()
    {
        return;
    }

    protected override void StartRoom()
    {
        active = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            StartCoroutine(SpawnEnemy(i, i*SPAWN_DELAY));
        }

    }

    IEnumerator SpawnEnemy(int index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        enemies[index].GetComponent<Enemy>().Spawn();
        if (index == enemies.Count - 1)
        {
            allEnemiesSpawned = true;
        }
    }

    public void SetReward(GameObject reward)
    {
        this.reward = reward;
    }
    
    private void SpawnReward()
    {
        if (reward != null && !GameManager.gameOver)
        {
            reward.SetActive(true);
        }
    }

    public virtual void generateEnemies(GameObject[] enemyTypes, int numEnemies, int enemyLevel)
    {

        enemies = new List<GameObject>(numEnemies);
        for (int i = 0; i < numEnemies; i++)
        {
            float roomSize = DungeonControllerModularRooms.ROOM_SIZE;
            float xPos = Random.Range(-roomSize/2 +2f, roomSize/2 - 2f);
            float yPos = 0;
            float zPos = Random.Range(-roomSize / 2 +2f, roomSize / 2 - 2f);
            Vector3 spawnLocation = transform.position + new Vector3(xPos, yPos, zPos);
            GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
            enemies.Add(Instantiate(enemyType, spawnLocation, Quaternion.identity, transform));
            enemies[i].GetComponent<Enemy>().SetLevel(enemyLevel);
            enemies[i].SetActive(false);
            if (enemyType.GetComponent<Enemy>().baseHealth < 50)
            {
                numEnemies++;
                i++;
                xPos = Random.Range(-roomSize / 2 + 2f, roomSize / 2 - 2f);
                zPos = Random.Range(-roomSize / 2 + 2f, roomSize / 2 - 2f);
                spawnLocation = transform.position + new Vector3(xPos, yPos, zPos);
                enemies.Add(Instantiate(enemyType, spawnLocation, Quaternion.identity, transform));
                enemies[i].GetComponent<Enemy>().SetLevel(enemyLevel);
                enemies[i].SetActive(false);
            }
        }
    }
}

