using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public int roomNumber;
    public RoomType roomType;
    public RoomSize roomSize;
    public List<Tile> tiles;
    public bool locked;

    public void SetRoomSize() => roomSize = roomType.roomSizes[Random.Range(0, roomType.roomSizes.Count)];
}
