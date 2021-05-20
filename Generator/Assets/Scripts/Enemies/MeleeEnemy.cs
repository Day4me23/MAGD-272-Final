using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float damageAmount = 1;
    Animator animator;
    public float speed;
    public float stoppingDistance;
    public bool isPaused;
    float timer = 0;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (GetComponent<Health>().health <= 0)
            return;

        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
            return;
        StartCoroutine(Thing(collision.transform));
    }
    IEnumerator Thing(Transform player)
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("Attack", false);

        player.GetComponentInParent<Health>().Damage(damageAmount);
        StopAllCoroutines();
    }
}
