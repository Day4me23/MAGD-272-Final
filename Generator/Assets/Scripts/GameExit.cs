using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
public class GameExit : MonoBehaviour
{
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.Play("MainMenu");
            StartCoroutine(Back());
        }

        IEnumerator Back()
        {

            yield return new WaitForSeconds(.1f);

            SceneManager.LoadScene("MainMenu");
        }
    }
}
