using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public Tile tile;
    [Space]
    public GameObject upDoor;
    public GameObject upWall;
    public GameObject downDoor;
    public GameObject downWall;
    public GameObject leftDoor;
    public GameObject leftWall;
    public GameObject rightDoor;
    public GameObject rightWall;

    public void BuildWalls(WallType up, WallType down, WallType left, WallType right)
    {
        upDoor.SetActive(up == WallType.door);
        downDoor.SetActive(down == WallType.door);
        leftDoor.SetActive(left == WallType.door);
        rightDoor.SetActive(right == WallType.door);
        upWall.SetActive(up == WallType.wall);
        downWall.SetActive(down == WallType.wall);
        leftWall.SetActive(left == WallType.wall);
        rightWall.SetActive(right == WallType.wall);
    }
}
