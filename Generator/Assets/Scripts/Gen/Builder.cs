using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private void Awake() => instance = this;
    public static Builder instance;
    public int currentRoom;

    public Generator generator;
    public List<GameObject> rooms;
    public GameObject roomHolderPrefab;
    public GameObject spacePrefab;

    public void Build()
    {
        for (int room = 0; room < generator.rooms.Count; room++)
        {
            BuildRoom(room);
            for (int tile = 0; tile < generator.rooms[room].tiles.Count; tile++)
                BuildSpace(generator.rooms[room].tiles[tile], room);
        }
        Spawn();
        ActivateRoom(0);
    }

    void BuildRoom(int roomNum)
    {
        Transform map = this.transform;
        GameObject temp = Instantiate(roomHolderPrefab, map);
        temp.name = "Room " + roomNum;
        temp.GetComponent<RoomHolder>().room = generator.rooms[roomNum];
        //Debug.Log("Room Number: " + roomNum + " " + generator.rooms[roomNum].roomType.name);
        
        rooms.Add(temp);
    }
    void BuildSpace(Tile tile, int roomNum)
    {
        Transform tiles = GameObject.Find("Room " + roomNum).transform;
        GameObject space = Instantiate(spacePrefab, (Vector3)tile.pos, Quaternion.identity, tiles);
        space.GetComponent<Space>().BuildWalls(tile.up, tile.down, tile.left, tile.right);
        space.GetComponent<Space>().tile = tile;
    }
    public void ActivateRoom(int roomNum)
    {
        if (roomNum == generator.roomCount - 1)
        { AudioManager.instance.Play("BossMusic"); }

        for (int i = 0; i < rooms.Count; i++)
        {
            if (i == roomNum)
                rooms[i].SetActive(true);
            else
                rooms[i].SetActive(false);
        }
        currentRoom = roomNum;
    }
    public void Spawn()
    {
        for (int i = 0; i < generator.roomCount; i++)
        {
            if (generator.rooms[i].roomType.spawnable == null)
                continue;

            int temp = Random.Range(1, 4);
            if (i == 0 || i == generator.roomCount - 1)
                temp = 1;

            for (int j = 0; j < temp; j++)
            {
                Instantiate(generator.rooms[i].roomType.spawnable, generator.rooms[i].tiles[Random.Range(0, generator.rooms[i].tiles.Count)].pos, Quaternion.identity, rooms[i].transform);
                // Debug.Log(generator.rooms[i].roomType.spawnable.name);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            ActivateRoom(0);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ActivateRoom(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ActivateRoom(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ActivateRoom(3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ActivateRoom(4);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            ActivateRoom(5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            ActivateRoom(6);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            ActivateRoom(7);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            ActivateRoom(8);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            ActivateRoom(9);
    }
}
