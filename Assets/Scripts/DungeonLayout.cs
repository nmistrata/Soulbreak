using UnityEngine;

struct LayoutRoom
{
    public int connections;
    public RoomType type;
    public int size;

    public LayoutRoom(RoomType type)
    {
        connections = 0;
        this.type = type;
        size = 1;
    }
}

public enum RoomType
{
    START,
    END,
    ALTAR,
    ENEMY,
    DEAD_END,
    NULL
}
public struct RoomIdentifier
{
    public int connections;
    public int x;
    public int y;
    public RoomType type;

    public RoomIdentifier(int x, int y, int connections, RoomType type)
    {
        this.connections = connections;
        this.x = x;
        this.y = y;
        this.type = type;
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
    private const int WEST =  8;
    private const int SOUTH =   4;
    private const int EAST =  2;
    private const int NORTH =   1;

    private LayoutRoom[,] rooms;

    public DungeonLayout(int pathLength, int sidePathLength)
    {
        this.pathLength = pathLength;
        this.maxSidePathLength = sidePathLength;
        this.numRooms = 0;
        maxSize = (int)1.4*pathLength;
        rooms = new LayoutRoom[maxSize, maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            for (int j = 0; j < maxSize; j++)
            {
                rooms[i, j] = new LayoutRoom(RoomType.NULL);
            }
        }
        startOffset = maxSize / 2;
        rooms[startOffset, startOffset].type = RoomType.START;
        numRooms += 1;
        ExtendPath(startOffset, startOffset, 0, true);
        Debug.Log(PrintLayout());
    }

    private void ExtendPath(int x, int y, int depth, bool mainPath)
    {
        int targetDirection = GetRandomDirection(x, y);
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
        rooms[nextX, nextY].type = RoomType.ENEMY;
        numRooms += 1;
        rooms[nextX, nextY].connections |= (byte)((targetDirection << 2) % (NORTH | EAST | SOUTH | WEST)); //opposite direction

        /* WAS PREVIOUSLY USED TO CREATE DOUBLE ROOMS
        if (rooms[x,y].size == 1 && Random.Range(0, 10) < 3) { //30 % for a single room to become a double
            rooms[x, y].connections |= (byte) (targetDirection << JOIN_SHIFT);
            rooms[x, y].size++;
            rooms[nextX, nextY].connections |= (byte)((byte)((targetDirection << 2) % (NORTH | EAST | WEST | SOUTH)) << JOIN_SHIFT);
            rooms[nextX, nextY].size = rooms[x, y].size;
        }*/

        depth += 1;
        if (mainPath && depth < pathLength) {
            ExtendPath(nextX, nextY, depth, true);
            if (Random.Range(0f,1f) > .5f) { //start a side path with chance 50%
                ExtendPath(nextX, nextY, 0, false);
            }
        } else if (!mainPath) {
            if (depth <= Random.Range(0, maxSidePathLength)) { //the longer the side path, the less chance of continuing it
                ExtendPath(nextX, nextY, depth, false);
            } else {
                rooms[nextX, nextY].type = RoomType.DEAD_END;
            }
        } else {
            rooms[nextX, nextY].type = RoomType.END;
        }
        return;
    }

    //returns a direction the path could continue, 0 if its a dead-end
    private int GetRandomDirection(int x, int y)
    {
        bool[] passages = new bool[] { false, false, false, false };
        passages[0] = (y != 0 && rooms[x, y - 1].type == RoomType.NULL); //north
        passages[1] = (x != maxSize - 1 && rooms[x + 1, y].type == RoomType.NULL); //east
        passages[2] = (y != maxSize - 1 && rooms[x, y + 1].type == RoomType.NULL); //south
        passages[3] = (x != 0 && rooms[x - 1, y].type == RoomType.NULL); //west

        int openWays = 0;
        int[] openDirections = new int[4] { 0, 0, 0, 0 };
        for (int i = 0; i < 4; i++) {
            if (passages[i]) {
                openDirections[openWays] = 1 << i;
                openWays += 1;
            }
        }
        if (openWays == 0) return -1;
        return openDirections[Random.Range(0, openWays)]; //direction to continue the path
    }

    //the start room is always at 0,0
    public RoomIdentifier[] GetRoomIdentifiers()
    {
        RoomIdentifier[] roomTypes = new RoomIdentifier[numRooms];
        int curRoom = 0;
        for (int x = 0; x < maxSize; x++) {
            for(int y = 0; y < maxSize; y++) {
                if(rooms[x, y].type != RoomType.NULL) {
                    roomTypes[curRoom] = new RoomIdentifier(x - startOffset, y - startOffset, rooms[x, y].connections, rooms[x, y].type);
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
                        LayoutRoom r = rooms[k, i];
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
                            switch (r.type) {
                            case RoomType.NULL:
                                returnString += "  ";
                                break;
                            case RoomType.START:
                                returnString += "S";
                                break;
                            case RoomType.DEAD_END:
                                returnString += "D";
                                break;
                            case RoomType.END:
                                returnString += "E";
                                break;
                            case RoomType.ENEMY:
                                returnString += "O";
                                break;
                            default:
                                returnString += " ";
                                break;

                        }
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
