using System.Collections;
using System.Collections.Generic;
using UnityEngine;



struct RoomOrientation
{
    public GameObject roomType;
    public int degreeRotation;

    public RoomOrientation(GameObject roomType, int degreeRotation) {
        this.roomType = roomType;
        this.degreeRotation = degreeRotation;
    }
}

public class DungeonController : MonoBehaviour
{
    private const byte WEST = 8;
    private const byte SOUTH = 4;
    private const byte EAST = 2;
    private const byte NORTH = 1;

    public DungeonLayout layout;
    public GameObject fourWayRoom;
    public GameObject cornerRoom;
    public GameObject straightRoom;
    public GameObject tRoom;
    public GameObject deadEndRoom;
    public const int ROOM_SIZE = 15;
    Dictionary<int, RoomOrientation> roomOrientations;
    // Start is called before the first frame update
    void Start()
    {
        layout = new DungeonLayout(10, 4);
        RoomIdentifier[] identifiers = layout.GetRoomTypes();
        populateRoomOrientations();
        
        foreach(RoomIdentifier i in identifiers) {
            createRoom(i);
        }
    }

    void populateRoomOrientations()
    {
        roomOrientations = new Dictionary<int, RoomOrientation>();
        roomOrientations.Add(NORTH | EAST | SOUTH | WEST, new RoomOrientation(fourWayRoom, 0)); //correct
        roomOrientations.Add(EAST | SOUTH | WEST, new RoomOrientation(tRoom, 90));
        roomOrientations.Add(NORTH | SOUTH | WEST, new RoomOrientation(tRoom, 0));
        roomOrientations.Add(NORTH | EAST | WEST, new RoomOrientation(tRoom, 270));
        roomOrientations.Add(NORTH | EAST | SOUTH, new RoomOrientation(tRoom, 180)); //correct
        roomOrientations.Add(SOUTH | WEST, new RoomOrientation(cornerRoom, 90));
        roomOrientations.Add(NORTH | WEST, new RoomOrientation(cornerRoom, 0));
        roomOrientations.Add(NORTH | EAST, new RoomOrientation(cornerRoom, 270)); //correct
        roomOrientations.Add(EAST | SOUTH, new RoomOrientation(cornerRoom, 180));
        roomOrientations.Add(EAST | WEST, new RoomOrientation(straightRoom, 0)); //correct
        roomOrientations.Add(NORTH | SOUTH, new RoomOrientation(straightRoom, 90)); 
        roomOrientations.Add(NORTH, new RoomOrientation(deadEndRoom, 90));
        roomOrientations.Add(EAST, new RoomOrientation(deadEndRoom, 0));
        roomOrientations.Add(SOUTH, new RoomOrientation(deadEndRoom, 270));
        roomOrientations.Add(WEST, new RoomOrientation(deadEndRoom, 180)); //correct
    }
    void createRoom(RoomIdentifier r)
    {
        //Debug.Log(r.connections & 0b1111);
        var roomOrientation = roomOrientations[r.connections & (NORTH | EAST | SOUTH | WEST)];
        Instantiate(roomOrientation.roomType, ROOM_SIZE * new Vector3(r.x, 0, r.y), Quaternion.Euler(0,roomOrientation.degreeRotation,0), transform);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
