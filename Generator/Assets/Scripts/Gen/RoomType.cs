using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomType
{
    public string name;
    public int weight;
    public List<RoomSize>roomSizes;
    
    [Space]
    public GameObject spawnable;
}

public enum RoomSize 
{ one = 1, two = 2, three = 3, four = 4, five = 5, six = 6, seven = 7, eight = 8, nine = 9, ten = 10, eleven = 11, twelve = 12}