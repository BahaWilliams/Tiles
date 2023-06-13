using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    IEnumerator ExitLevel()
    {
        yield return new WaitForSecondsRealtime(3);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentScene + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings )
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScreenPresist>().ResetScenePresist();
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(ExitLevel());
        }
    }
}
