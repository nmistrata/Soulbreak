using UnityEngine;

struct Room
{
    public byte connections;
    public bool isActive;
    public bool isStart;
    public int size;

    public Room(bool start)
    {
        connections = 0;
        isActive = false;
        isStart = start;
        size = 1;
    }
}
public struct RoomIdentifier
{
    public byte connections;
    public int x;
    public int y;

    public RoomIdentifier(int x, int y, byte connections)
    {
        this.connections = connections;
        this.x = x;
        this.y = y;
    }
}

public class DungeonLayout
{
    private int pathLength;
    private int maxSidePathLength;
    private int maxSize;
    private int numRooms;
    private int startOffset;

    private const int JOIN_SHIFT = 4;
    private const byte WEST =  8;
    private const byte SOUTH =   4;
    private const byte EAST =  2;
    private const byte NORTH =   1;

    private Room[,] rooms;

    public DungeonLayout(int pathLength, int sidePathLength)
    {
        this.pathLength = pathLength;
        this.maxSidePathLength = sidePathLength;
        this.numRooms = 0;
        maxSize = (int)1.4*pathLength;
        maxSize += maxSize % 2;
        rooms = new Room[maxSize, maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            for (int j = 0; j < maxSize; j++)
            {
                rooms[i, j] = new Room(false);
            }
        }
        startOffset = maxSize / 2;
        rooms[startOffset, startOffset].isStart = true;
        rooms[startOffset, startOffset].isActive = true;
        numRooms += 1;
        ExtendPath(startOffset, startOffset, 0, true);
        Debug.Log(PrintLayout());
    }

    private void ExtendPath(int x, int y, int depth, bool mainPath)
    {
        byte targetDirection = GetRandomDirection(x, y);
        int nextX = x;
        int nextY = y;
        //return if direction is not valid (dead-end)
        switch (targetDirection) {
            case NORTH:
                nextY--;
                break;
            case EAST:
                nextX++;
                break;
            case SOUTH:
                nextY++;
                break;
            case WEST:
                nextX--;
                break;
            default:
                return;
        }

        rooms[x, y].connections |= targetDirection;
        rooms[nextX, nextY].isActive = true;
        numRooms += 1;
        rooms[nextX, nextY].connections |= (byte)((targetDirection << 2) % (NORTH | EAST | SOUTH | WEST));
        if (rooms[x,y].size == 1 && Random.Range(0, 10) < 3) { //30 % for a single room to become a double
            rooms[x, y].connections |= (byte) (targetDirection << JOIN_SHIFT);
            rooms[x, y].size++;
            rooms[nextX, nextY].connections |= (byte)((byte)((targetDirection << 2) % (NORTH | EAST | WEST | SOUTH)) << JOIN_SHIFT);
            rooms[nextX, nextY].size = rooms[x, y].size;
        }

        depth += 1;
        if (mainPath && depth < pathLength) {
            ExtendPath(nextX, nextY, depth, true);
            if (Random.Range(0f,1f) > .5f) { //start a side path with chance 50%
                ExtendPath(nextX, nextY, 0, false);
            }
        } else if (!mainPath) {
            if (depth <= Random.Range(0, maxSidePathLength)) { //the longer the side path, the less chance of continuing it
                ExtendPath(nextX, nextY, depth, false);
            }
        }
        return;
    }

    //returns a direction the path could continue, 0 if its a dead-end
    private byte GetRandomDirection(int x, int y)
    {
        bool[] passages = new bool[] { false, false, false, false };
        passages[0] = y != 0 && !rooms[x, y - 1].isActive; //north
        passages[1] = x != maxSize - 1 && !rooms[x + 1, y].isActive; //east
        passages[2] = y != maxSize - 1 && !rooms[x, y + 1].isActive; //south
        passages[3] = x != 0 && !rooms[x - 1, y].isActive; //west

        int openWays = 0;
        byte[] openDirections = new byte[4] { 0, 0, 0, 0 };
        for (int i = 0; i < 4; i++) {
            if (passages[i]) {
                openDirections[openWays] = (byte)(1 << i);
                openWays += 1;
            }
        }
        return openDirections[Random.Range(0, openWays)]; //direction to continue the path
    }

    //the start room is always at 0,0
    public RoomIdentifier[] GetRoomTypes()
    {
        RoomIdentifier[] roomTypes = new RoomIdentifier[numRooms];
        int curRoom = 0;
        for (int x = 0; x < maxSize; x++) {
            for(int y = 0; y < maxSize; y++) {
                if(rooms[x, y].isActive) {
                    roomTypes[curRoom] = new RoomIdentifier(x-startOffset, y-startOffset, rooms[x, y].connections);
                    curRoom++;
                }
            }
        }
        return roomTypes;
    }

    public string PrintLayout()
    {
        string returnString = "";
        for (int i = 0; i < maxSize; i++) { returnString += "xxx"; }
        returnString += "xx\n";
            for (int i = 0; i < maxSize; i++) {
                for (int j = 0; j < 3; j++) {
                    returnString += "x";
                    for (int k = 0; k < maxSize; k++) {
                        Room r = rooms[k, i];
                        if (j == 0) {
                            returnString += "  ";
                            if ((r.connections & NORTH) > 0) {
                                returnString += (((r.connections >> JOIN_SHIFT) & NORTH) > 0) ? "o" : "| ";
                            } else {
                                returnString += "  ";
                            }
                            returnString += "  ";
                        } else if (j == 1) {
                            if ((r.connections & WEST) > 0) {
                                returnString += (((r.connections >> JOIN_SHIFT) & WEST) > 0) ? "o" : "- ";
                            } else {
                                returnString += "  ";
                            }
                            returnString += r.isActive ? r.size.ToString() : "  ";
                            if ((r.connections & EAST) > 0) {
                                returnString += (((r.connections >> JOIN_SHIFT) & EAST) > 0) ? "o" : " -";
                            } else {
                                returnString += "  ";
                            }
                        } else {
                            returnString += "  ";
                            if ((r.connections & SOUTH) > 0) {
                                returnString += (((r.connections >> JOIN_SHIFT) & SOUTH) > 0) ? "o" : "| ";
                            } else {
                            returnString += "  ";
                        }
                        returnString += "  ";
                        }
                    }
                    returnString += "x";
                    returnString += "\n";
                }

        }
        for (int i = 0; i < maxSize; i++) { returnString += "xxx"; }
        returnString += "xx";
        Debug.Log(rooms[0,0].connections);
        return returnString;
    }
}
