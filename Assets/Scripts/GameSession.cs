using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private int coinPickup;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        if (numGameSession > 1)
            Destroy(gameObject);

        else
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = coinPickup.ToString();
    }

    public void ProssesPlayerDeath()
    {
        if (playerLives > 1) { DecreaseLive(); }

        else { ResetGameSession(); }
    }

    private void DecreaseLive()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        FindObjectOfType<ScreenPresist>().ResetScenePresist();
    }

    public void AddScore(int increaseScore)
    {
        coinPickup += increaseScore;
        scoreText.text = coinPickup.ToString();
    }
}
