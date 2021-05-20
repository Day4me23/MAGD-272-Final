using System.Collections.Generic;
using UnityEngine;
public class Generator : Seed
{
    #region Singleton
    public static Generator instance;
    private void Awake() => instance = this;
    private void Start() => Generate();
    #endregion
    #region Variables
    [SerializeField] Builder builder;
    public int roomCount;
    public List<Room> rooms;
    public Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();
    [Space]
    public RoomType spawn;
    public RoomType boss;
    public RoomType [] roomWeights;
    Room newRoom;
    #endregion

    void Generate()
    {
        for (int i = 0; i < roomCount; i++)
        {
            newRoom = new Room();
            newRoom.tiles = new List<Tile>();
            
            newRoom.roomNumber = rooms.Count;
            newRoom.roomType = SetType();
            newRoom.SetRoomSize();

            if (rooms.Count == 0) SetSpawn();
            else while (!AttemptTile()) { }
        }
        builder.Build();
    }
    bool AttemptTile()
    {
        Room room = rooms[Random.Range(0, rooms.Count)];
        Tile randomTile = room.tiles[Random.Range(0, room.tiles.Count)];
        Cardinal direction = (Cardinal)Random.Range(0, 4);

        Vector2 startPos = randomTile.pos;
        if (direction == Cardinal.up)
            startPos += Vector2.up;
        if (direction == Cardinal.down)
            startPos += Vector2.down;
        if (direction == Cardinal.left)
            startPos += Vector2.left;
        if (direction == Cardinal.right)
            startPos += Vector2.right;

        Pass pass = CheckPerpendicular(direction, startPos, newRoom.roomSize);
        if (pass == null) return false;

        Build(pass.dl, newRoom.roomSize, startPos, direction);
        Tile startTile = tiles[startPos];
        Link(startTile, randomTile, direction);
        return true;
    }
    void Link(Tile start, Tile random, Cardinal dir)
    {
        // Debug.Log("- ran:" + random.pos + " - sta:" + start.pos);
        if (dir == Cardinal.up)
        {
            random.upLink = start;
            random.up = WallType.door;
            start.downLink = random;
            start.down = WallType.door;
            // Debug.Log("+ ran:" + random.upLink.pos + " - sta:" + start.downLink.pos);
            return;
        }
        if (dir == Cardinal.down)
        {
            random.downLink = start;
            random.down = WallType.door;
            start.upLink = random;
            start.up = WallType.door;
            // Debug.Log("+ ran:" + random.downLink.pos + " - sta:" + start.upLink.pos);
            return;
        }
        if (dir == Cardinal.left)
        {
            random.leftLink = start;
            random.left = WallType.door;
            start.rightLink = random;
            start.right = WallType.door;
            // Debug.Log("+ ran:" + random.leftLink.pos + " - sta:" + start.rightLink.pos);
            return;
        }
        if (dir == Cardinal.right)
        {
            random.rightLink = start;
            random.right = WallType.door;
            start.leftLink = random;
            start.left = WallType.door;
            // Debug.Log("+ ran:" + random.rightLink.pos + " - sta:" + start.leftLink.pos);
            return;
        }
    }
    bool CheckParallel(Cardinal direction, Vector2 startPos, RoomSize roomSize)
    {
        Vector2 dir = new Vector2(0,0);
        Tile tile;

        for (int i = 0; i < (int)roomSize; i++)
        {
            if (direction == Cardinal.up)
                dir = Vector2.up;
            else if (direction == Cardinal.down)
                dir = Vector2.down;
            else if (direction == Cardinal.left)
                dir = Vector2.left;
            else if (direction == Cardinal.right)
                dir = Vector2.right;

            if (tiles.TryGetValue(dir * i + startPos, out tile))
                return false;
        }
        return true;
    }
    Pass CheckPerpendicular(Cardinal direction, Vector2 startPos, RoomSize roomSize)
    {
        float ur = 0, dl = 0;

        if (!CheckParallel(direction, startPos, roomSize))
            return null;

        for (int i = 1; i < (int)roomSize; i++)
        {
            bool trueFalse = Random.value > .5f;

            if (direction == Cardinal.up || direction == Cardinal.down)
            {
                if (trueFalse)
                {
                    dl++;
                    if (!CheckParallel(direction, startPos + Vector2.left * dl, roomSize))
                    {
                        dl--;
                        ur++;
                        if (!CheckParallel(direction, startPos + Vector2.right * ur, roomSize))
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    ur++;
                    if (!CheckParallel(direction, startPos + Vector2.right * ur, roomSize))
                    {
                        ur--;
                        dl++;
                        if (!CheckParallel(direction, startPos + Vector2.left * dl, roomSize))
                        {
                            return null;
                        }
                    }
                }
            }
            if (direction == Cardinal.left || direction == Cardinal.right)
            {
                if (trueFalse)
                {
                    dl++;
                    if (!CheckParallel(direction, startPos + Vector2.down * dl, roomSize))
                    {
                        dl--;
                        ur++;
                        if (!CheckParallel(direction, startPos + Vector2.up * ur, roomSize))
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    ur++;
                    if (!CheckParallel(direction, startPos + Vector2.up * ur, roomSize))
                    {
                        ur--;
                        dl++;
                        if (!CheckParallel(direction, startPos + Vector2.down * dl, roomSize))
                        {
                            return null;
                        }
                    }
                }
            }
        }

        Pass dlPass = new Pass();
        dlPass.dl = dl;
        return dlPass;
    }
    void Build(float dl, RoomSize roomSize, Vector2 startPos, Cardinal direction)
    {
        Vector2 corner = startPos;
        if (direction == Cardinal.up)
            corner += new Vector2(-dl, 0);
        if (direction == Cardinal.down)
            corner += new Vector2(-dl, -((int)roomSize - 1));
        if (direction == Cardinal.left)
            corner += new Vector2(-((int)roomSize - 1), -dl);
        if (direction == Cardinal.right)
            corner += new Vector2(0, -dl);

        for (int x = 0; x < (int)roomSize; x++)
        {
            for (int y = 0; y < (int)roomSize; y++)
            {
                Tile tile = new Tile();
                tile.roomNumber = newRoom.roomNumber;
                tile.pos = new Vector2(x, y) + corner;

                tiles.Add(tile.pos, tile); // add tile to dictionary
                newRoom.tiles.Add(tile); // add tile to the new room


                // building walls
                if (x == 0) tile.left = WallType.wall;
                if (y == 0) tile.down = WallType.wall;
                if (x == (int)roomSize - 1) tile.right = WallType.wall;
                if (y == (int)roomSize - 1) tile.up = WallType.wall;
            }
        }

        rooms.Add(newRoom);
    }
    

    void SetSpawn() => Build(0, newRoom.roomSize, new Vector2(0, 0), Cardinal.up);
    RoomType SetType()
    {
        if (newRoom.roomNumber == 0)
            return spawn;
        if (newRoom.roomNumber == roomCount - 1)
            return boss;

        int total = 0;
        for (int index = 0; index < roomWeights.Length; index++)
            total += roomWeights[index].weight;
        total = Random.Range(0, total);

        for (int index = 0; index < roomWeights.Length; index++)
            if ((total -= roomWeights[index].weight) < 0)
                return roomWeights[index];

        return null;
    }
}

public class Pass { public float dl; }
