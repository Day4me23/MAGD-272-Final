using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    float timer = 0;
    private void Awake()
    {
        Destroy(this.gameObject, 10);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Player"))
            return;

        

        if (timer < 1)
            return;

        collision.GetComponentInParent<Health>().Damage(1);
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
}
