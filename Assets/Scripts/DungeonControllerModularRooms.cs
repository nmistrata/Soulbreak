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
    public const int ROOM_SIZE = 30;

    public GameObject[] enemies;
    public int numEnemiesPerRoom = 2;

    public int mainPathLength = 6;
    public int sidePathMaxLength = 3;

    // Start is called before the first frame update
    void Start()
    {
        layout = new DungeonLayout(mainPathLength, sidePathMaxLength);
        RoomIdentifier[] identifiers = layout.GetRoomIdentifiers();

        foreach (RoomIdentifier i in identifiers) {
            CreateRoom(i);
        }

        GlobalVars.dungeon = gameObject;
    }

    void CreateRoom(RoomIdentifier r)
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
        newRoomObj.transform.parent = this.transform;

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
                newRoomObj.AddComponent<BossRoom>();
                break;
            case (RoomType.ALTAR):
                newRoomObj.AddComponent<AltarRoom>();
                break;
            case (RoomType.ENEMY):
                CombatRoom combatRoom = newRoomObj.AddComponent<CombatRoom>();
                combatRoom.generateEnemies(enemies, numEnemiesPerRoom);
                break;
            default:
                break;
        }

        BoxCollider boxCollider = newRoomObj.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(ROOM_SIZE, ROOM_SIZE, ROOM_SIZE);
        boxCollider.isTrigger = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
