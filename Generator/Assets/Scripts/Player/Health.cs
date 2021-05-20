using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public GameObject drop;
    bool alive = true;
    public GameObject DieScreen;

    private void Awake()
    {
        health = maxHealth;
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    [Space]
    public Image healthBar;
    public Camera cam;
    Animator animator;
    [Space]
    public float coinCount = 0;
    public float health = 10f;
    public float maxHealth;
    public void Damage(float amount)
    {
        Debug.Log("Damage");
        health-= amount;
    }
    public void PickupCoin()
    {
        coinCount += 1;
    }
    public void Heal()
    {
        health += 1;
    }
    private void OnTriggerStay2D(Collider2D hit)
    {
        if (Input.GetKey(KeyCode.E))
        {
            

            if (hit.transform.CompareTag("Coin"))
            {
                AudioManager.instance.Play("ObjectPickup");
                Destroy(hit.gameObject);
                PickupCoin();
            }
            if (hit.transform.CompareTag("Potion"))
            {
                AudioManager.instance.Play("ObjectPickup");
                Destroy(hit.gameObject);
                Heal();
            }
            if (hit.transform.CompareTag("Skull"))
            {
                float temp = PlayerPrefs.GetFloat("floorCurrent", 0) + 1;
                Debug.Log(temp);
                PlayerPrefs.SetFloat("floorCurrent", temp);
                AudioManager.instance.Play("ObjectPickup");
                Destroy(hit.gameObject);
                SceneManager.LoadScene("Game");
            }
        }
    }
    private void Update()
    {
        if (!alive) return;

        if (health > maxHealth)
            health = maxHealth;
        if (health <= 0)
        {
            AudioManager.instance.Play("YouDied");
            if (drop != null)
                Instantiate(drop, transform.position, Quaternion.identity, transform.parent);

            if (animator != null)
                animator.SetBool("Alive", false);

            if (DieScreen != null)
                DieScreen.SetActive(true);

            alive = false;
            Destroy(this.gameObject, .5f);
        }
        if (healthBar != null)
            healthBar.fillAmount = health / maxHealth;
    }
}
