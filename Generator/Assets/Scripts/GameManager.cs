using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private void Awake() { 
        instance = this;

        floor = PlayerPrefs.GetFloat("floorCurrent", 0);
        timer = PlayerPrefs.GetFloat("timerCurrent", 0);
        playerHealth.health = PlayerPrefs.GetFloat("healthCurrent", 0);
        playerHealth.coinCount = PlayerPrefs.GetFloat("coinCurrent", 0);

    }

    public static GameManager instance;
    public float timer = 0;
    public float floor = 0;

    public Health playerHealth;

    public Text coinScore;
    public Text timeScore;
    public Text floorScore;
    [Space]
    public Text dieText;

    private void Update()
    {
        if (playerHealth.health > 0)
            timer += Time.deltaTime;

        if (coinScore != null)
        coinScore.text = playerHealth.coinCount + "";
        if (timeScore != null)
        timeScore.text = (int)timer + "";
        if (floorScore != null)
            floorScore.text = PlayerPrefs.GetFloat("floorCurrent", 0) + "";

        PlayerPrefs.SetFloat("healthCurrent", playerHealth.health);
        PlayerPrefs.SetFloat("coinCurrent", playerHealth.coinCount);
        PlayerPrefs.SetFloat("timerCurrent", timer);

        if (playerHealth.health <= 0)
        {
            dieText.text = "Score   -   Coin: " + PlayerPrefs.GetFloat("coinCurrent", 0)
                + ",   Time: " + (int)PlayerPrefs.GetFloat("timerCurrent", 0) + "Sec,   Floor: " +
                PlayerPrefs.GetFloat("floorCurrent", 0);
        }
    }
}
