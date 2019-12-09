using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonControllerModularRooms : MonoBehaviour
{
    private const byte WEST = 8;
    private const byte SOUTH = 4;
    private const byte EAST = 2;
    private const byte NORTH = 1;

    public DungeonLayout layout;
    public GameObject wall;
    public GameObject doorway;
    public GameObject floor;
    public GameObject ceiling;
    public GameObject bossTeleporter;
    public GameObject wandPickup;
    private const float PICKUP_HEIGHT = 2f;
    private float RewardChance = 1f;
    public const int ROOM_SIZE = 30;

    public GameObject[] enemies;
    public GameObject[] bosses;
    public GameObject[] wands;

    public int baseNumEnemiesPerRoom = 2;
    private int numEnemiesPerRoom;
    public int enemiesIncreasePerLevel = 1;

    public int baseMainPathLength = 2;
    private int mainPathLength;
    public int pathLengthIncreasePerLevel = 1;
    private int sidePathMaxLength = 0;

    public int curLevel;
    private GameObject curLevelObj;
    private GameObject nextLevelObj;

    private void Awake()
    {
        GameManager.dungeon = this;
    }

    void Start()
    {
        mainPathLength = baseMainPathLength;
        numEnemiesPerRoom = baseNumEnemiesPerRoom;
        curLevelObj = GenerateFirstLevel();
    }

    private GameObject GenerateFirstLevel()
    {
        curLevel = 0;
        DungeonLayout layout = new DungeonLayout(mainPathLength, sidePathMaxLength);
        RoomIdentifier[] identifiers = layout.GetRoomIdentifiers();
        GameObject newLevelObj = new GameObject("level_" + (curLevel + 1));
        newLevelObj.transform.position = Vector3.zero;
        newLevelObj.transform.parent = transform;
        foreach (RoomIdentifier i in identifiers)
        {
            CreateRoom(i, newLevelObj.transform);
        }
        numEnemiesPerRoom += enemiesIncreasePerLevel;
        mainPathLength += pathLengthIncreasePerLevel;
        sidePathMaxLength = mainPathLength / 3;
        curLevel = 1;
        StartCoroutine(GenerateNewLevelAsync());
        return newLevelObj;
    }

    IEnumerator GenerateNewLevelAsync()
    {
        DungeonLayout layout = new DungeonLayout(mainPathLength, sidePathMaxLength);
        RoomIdentifier[] identifiers = layout.GetRoomIdentifiers();
        GameObject newLevelObj = new GameObject("level_" + (curLevel+1));
        newLevelObj.transform.position = Vector3.zero;
        newLevelObj.transform.parent = transform;
        newLevelObj.SetActive(false);
        yield return null;

        foreach (RoomIdentifier i in identifiers)
        {
            CreateRoom(i, newLevelObj.transform);
            yield return null;
        }
        nextLevelObj = newLevelObj;
    }

    public void AdvanceLevel()
    {
        curLevel++;
        numEnemiesPerRoom += enemiesIncreasePerLevel;
        mainPathLength += pathLengthIncreasePerLevel;
        sidePathMaxLength = mainPathLength / 3;
        Destroy(curLevelObj);
        curLevelObj = nextLevelObj;
        curLevelObj.SetActive(true);
        StartCoroutine(GenerateNewLevelAsync());
    }

    void CreateRoom(RoomIdentifier r, Transform level)
    {
        GameObject northWall = (r.connections & NORTH) > 0 ? doorway : wall;
        GameObject eastWall = (r.connections & EAST) > 0 ? doorway : wall;
        GameObject southWall = (r.connections & SOUTH) > 0 ? doorway : wall;
        GameObject westWall = (r.connections & WEST) > 0 ? doorway : wall;
        GameObject floor = this.floor;
        GameObject ceiling = this.ceiling;
        Vector3 roomOrigin = ROOM_SIZE * new Vector3(r.x, 0, r.y);

        GameObject newRoomObj = new GameObject("room" + r.x + r.y);
        newRoomObj.transform.position = roomOrigin;
        newRoomObj.transform.parent = level;

        Instantiate(floor, roomOrigin, Quaternion.Euler(0, 0, 0), newRoomObj.transform);
        Instantiate(ceiling, roomOrigin, Quaternion.Euler(0, 0, 0), newRoomObj.transform);
        Instantiate(northWall, roomOrigin, Quaternion.Euler(0, 270, 0), newRoomObj.transform);
        Instantiate(eastWall, roomOrigin, Quaternion.Euler(0, 180, 0), newRoomObj.transform);
        Instantiate(southWall, roomOrigin, Quaternion.Euler(0, 90, 0), newRoomObj.transform);
        Instantiate(westWall, roomOrigin, Quaternion.Euler(0, 0, 0), newRoomObj.transform);

        switch (r.type)
        {
            case (RoomType.START):
                newRoomObj.AddComponent<SpawnRoom>();
                break;
            case (RoomType.END):
                GameObject teleporter = Instantiate(bossTeleporter, roomOrigin, Quaternion.Euler(0, 0, 0), newRoomObj.transform);
                teleporter.GetComponent<Teleporter>().SetDestination(new Vector3(0, 0, 0));
                teleporter.SetActive(false);
                BossWaveCombatRoom bossRoom = newRoomObj.AddComponent<BossWaveCombatRoom>();
                GameObject boss = bosses[Random.Range(0, bosses.Length)];
                bossRoom.generateEnemies(enemies, numEnemiesPerRoom, 3, curLevel+1);
                bossRoom.teleporter = teleporter;
                break;
            case (RoomType.DEAD_END):
            case (RoomType.ENEMY):
                CombatRoom combatRoom = newRoomObj.AddComponent<CombatRoom>();
                combatRoom.generateEnemies(enemies, numEnemiesPerRoom, curLevel+1);

                if (Random.Range(0f, 1f) < RewardChance)
                {
                    WandPickup wp = Instantiate(wandPickup, newRoomObj.transform.position + (Vector3.up * PICKUP_HEIGHT), Quaternion.identity, newRoomObj.transform).GetComponent<WandPickup>();
                    GameObject wand = wands[Random.Range(0, wands.Length)];
                    wp.SetWand(wand);
                    wp.gameObject.SetActive(false);
                    combatRoom.SetReward(wp.gameObject);
                }

                break;
            default:
                break;
        }

        BoxCollider boxCollider = newRoomObj.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(ROOM_SIZE, ROOM_SIZE, ROOM_SIZE);
        boxCollider.isTrigger = true;
        newRoomObj.tag = "Invisible";
    }

    public void Restart()
    {
        Destroy(curLevelObj);
        Destroy(nextLevelObj);
        Start();
    }
}
