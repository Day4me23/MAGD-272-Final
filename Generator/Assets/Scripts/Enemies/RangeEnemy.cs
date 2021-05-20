using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBtwShots;
    public float startTimeBtwShots;
    public bool isPaused;
    public float slope;


    Vector2 movement;
    public GameObject projectile;

    public Transform player;
    Rigidbody2D rb;
    Animator animator;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        if (GetComponent<Health>().health <= 0)
            return;

        Vector2 lookDir = (Vector2)player.position - (Vector2)this.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 45f;

        if (angle < 45 && angle > -45)
        {
            animator.SetFloat("Horizontal", 1);
        }
        else if(angle > 45 && angle < 135)
        {
            animator.SetFloat("Vertical", 1);
        }
        else if(angle < -45 && angle > -135)
        {
            animator.SetFloat("Vertical", -1);
        }
        else
        {
            animator.SetFloat("Horizontal", -1);
        }
        

        if (timeBtwShots <= 0)
        {
            animator.SetBool("Attack", true);
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots; //Prevents the ranged AI from shooting every frame and gives the shot a cooldown
        }
        else
        {
            animator.SetBool("Attack", false);
            timeBtwShots -= Time.deltaTime;
        }
    } //end update
} //end class