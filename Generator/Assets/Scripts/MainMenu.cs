using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Audio"))
            Destroy(GameObject.FindGameObjectWithTag("Audio").gameObject);
    }
    public void Play()
    {
        PlayerPrefs.SetFloat("healthCurrent", 10);
        PlayerPrefs.SetFloat("coinCurrent", 0);
        PlayerPrefs.SetFloat("timerCurrent", 0);
        PlayerPrefs.SetFloat("floorCurrent", 1);
        SceneManager.LoadScene("Game");
    }
    public void Quit() => Application.Quit();
}
