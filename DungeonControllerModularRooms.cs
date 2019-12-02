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

    // Start is called before the first frame update
    void Start()
    {
        layout = new DungeonLayout(10, 4);
        RoomIdentifier[] identifiers = layout.GetRoomTypes();

        foreach (RoomIdentifier i in identifiers) {
            createRoom(i);
        }
    }

    void createRoom(RoomIdentifier r)
    {
        GameObject northWall = (r.connections & NORTH) > 0 ? doorway : wall;
        GameObject eastWall = (r.connections & EAST) > 0 ? doorway : wall;
        GameObject southWall = (r.connections & SOUTH) > 0 ? doorway : wall;
        GameObject westWall = (r.connections & WEST) > 0 ? doorway : wall;
        GameObject floor = this.floor;
        GameObject ceiling = this.ceiling;
        Vector3 roomOrigin = ROOM_SIZE * new Vector3(r.x, 0, r.y);

        GameObject newRoom = new GameObject("room" + r.x + r.y);
        newRoom.transform.position = roomOrigin;
        newRoom.transform.parent = this.transform;

        Instantiate(floor, roomOrigin, Quaternion.Euler(0, 0, 0), newRoom.transform);
        Instantiate(ceiling, roomOrigin, Quaternion.Euler(0, 0, 0), newRoom.transform);
        Instantiate(northWall, roomOrigin, Quaternion.Euler(0, 270, 0), newRoom.transform);
        Instantiate(eastWall, roomOrigin, Quaternion.Euler(0, 180, 0), newRoom.transform);
        Instantiate(southWall, roomOrigin, Quaternion.Euler(0, 90, 0), newRoom.transform);
        Instantiate(westWall, roomOrigin, Quaternion.Euler(0, 0, 0), newRoom.transform);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
