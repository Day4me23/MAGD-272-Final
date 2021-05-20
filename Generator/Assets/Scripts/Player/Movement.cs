using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    Animator animator;
    public GameObject colliderUp;
    public GameObject colliderDown;
    public GameObject colliderLeft;
    public GameObject colliderRight;
    

    bool frozen = false;
    [SerializeField] float speed = 1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    

    float Hor()
    {
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            return 0;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            return 0;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            colliderLeft.SetActive(true);
            colliderRight.SetActive(false);
            colliderUp.SetActive(false);
            colliderDown.SetActive(false);
            return -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            colliderRight.SetActive(true);
            colliderLeft.SetActive(false);
            colliderUp.SetActive(false);
            colliderDown.SetActive(false);
            return 1;
        }
        else
        {
            return 0;
        }

    }
    float Ver()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            return 0;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            return 0;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            colliderUp.SetActive(true);
            colliderDown.SetActive(false);
            colliderLeft.SetActive(false);
            colliderRight.SetActive(false);
            return 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            colliderDown.SetActive(true);
            colliderUp.SetActive(false);
            colliderRight.SetActive(false);
            colliderLeft.SetActive(false);
            return -1;
        }
        else
        {
            return 0;
        }
    }


   
    void Animation()
    {
        if (movement.y > 0) animator.SetBool("Up", true);
        else animator.SetBool("Up", false);

        if (movement.y < 0) animator.SetBool("Down", true);
        else animator.SetBool("Down", false);

        if (movement.x < 0) animator.SetBool("Left", true);
        else animator.SetBool("Left", false);

        if (movement.x > 0) animator.SetBool("Right", true);
        else animator.SetBool("Right", false);
    }
    public void FreezeCall(float time) => StartCoroutine(Freeze(time));
    IEnumerator Freeze(float time)
    {

        frozen = true;
        yield return new WaitForSeconds(time);
        frozen = false;
        StopAllCoroutines();
    }


    void Update()
    {
        movement = new Vector2(0, 0);
        if (frozen) return;
        movement = new Vector2(Hor(), Ver());
        Animation();
        
    }
    private void FixedUpdate() => rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
}
