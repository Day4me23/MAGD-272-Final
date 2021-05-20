using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject back;
    public static Transition instance;
    private void Awake()
    {
        instance = this;
    }

    public Image blackness;
    public float speed = 3f;
    public float waitTime = .5f;
    public float current = 0;
    bool done = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(TransitionGo());

        if (done && current < 1)
            blackness.fillAmount = current += speed * Time.deltaTime;
        else if (current > 0 && !done)
            blackness.fillAmount = current -= speed * Time.deltaTime;
    }
    private void Start()
    {
        DoTransition();
    }
    public float DoTransition()
    {
        
        AudioManager.instance.Play("Door");
        StartCoroutine(TransitionGo());
        return waitTime;
    }

    IEnumerator TransitionGo()
    {
        done = true;
        yield return new WaitForSeconds(waitTime);
        MoveBack();
        done = false;
        StopCoroutine(TransitionGo());
    }
    void MoveBack()
    {
        back.transform.position = playerTransform.position;
    }
}
