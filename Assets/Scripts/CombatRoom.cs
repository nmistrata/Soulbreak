using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom : Room
{
    private GameObject[] enemies = null;
    private bool active = false;
    private bool allenemiesSpawned = false;
    private const float SPAWN_DELAY = .7f;

    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !active)
        {
            TryToStartRoom();
        }

        if (allenemiesSpawned)
        {
            bool enemiesRemaining = false;
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].activeSelf) { enemiesRemaining = true; break; };
            }
            if (!enemiesRemaining) { ClearRoom(); OpenDoors(); }
        }
    }

    protected override void SetupRoom()
    {
        return;
    }

    protected override void StartRoom()
    {
        active = true;
        for (int i = 0; i < enemies.Length; i++)
        {
            StartCoroutine(SpawnEnemy(i, i*SPAWN_DELAY));
        }

    }

    IEnumerator SpawnEnemy(int index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        enemies[index].SetActive(true);
        if (index == enemies.Length - 1)
        {
            allenemiesSpawned = true;
        }
    }

    public virtual void generateEnemies(GameObject[] enemyTypes, int numEnemies)
    {

        enemies = new GameObject[numEnemies];
        for (int i = 0; i < numEnemies; i++)
        {
            float roomSize = DungeonControllerModularRooms.ROOM_SIZE;
            float xPos = Random.Range(-roomSize/2 +2f, roomSize/2 - 2f);
            float yPos = 0;
            float zPos = Random.Range(-roomSize / 2 +2f, roomSize / 2 - 2f);
            Vector3 spawnLocation = transform.position + new Vector3(xPos, yPos, zPos);
            enemies[i] = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnLocation, Quaternion.identity, transform);
            enemies[i].SetActive(false);
        }
    }
}

