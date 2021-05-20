using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    float timer = 0;
    float soundTimer = 0;
    public GameObject lava;
    private void Update()
    {
        timer += Time.deltaTime;
        soundTimer += Time.deltaTime;
        if (timer > 1)
        {
            Thing();
            Thing();
        }
        if (soundTimer >= 5)
        {
            AudioManager.instance.Play("Roar");
            soundTimer = 0;
        }
    }
    void Thing()
    {
        Debug.Log("Lava Spawn");
        Instantiate(lava,
                (Vector3)Generator.instance.rooms[Generator.instance.roomCount - 1].tiles [
                    (int)Random.Range(0, Generator.instance.rooms[Builder.instance.currentRoom].tiles.Count)
                    ].pos, Quaternion.identity,
                Builder.instance.rooms[Builder.instance.rooms.Count-1].transform);
        timer = 0;
    }
}
