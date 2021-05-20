using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject up, down, left, right;
    public Cardinal facing;
    float timer = 0;
    

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("AttackM", true);
            GetComponent<Movement>().FreezeCall(.6f);

            AudioManager.instance.Play("MeleeSwing");
        }
        else animator.SetBool("AttackM", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.Space) && timer > .667f)
        {

            if (collision.GetComponent<Health>() != null)
            {
                collision.GetComponent<Health>().Damage(1);
                timer = 0;
            }
        }
    }
}
