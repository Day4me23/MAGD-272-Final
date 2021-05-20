using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
    public int roomNumber;
    public Vector2 pos;
    [SerializeField] public Tile upLink, downLink, rightLink, leftLink;
    public WallType up, down, left, right;
}

public enum Cardinal
{ up, down, left, right }
public enum WallType
{ open, wall, door } 