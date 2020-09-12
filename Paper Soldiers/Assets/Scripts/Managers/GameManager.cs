using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{



    private float spawnRate = 1.0f;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public bool isGameActive;
    public Button restartButton;

    public GameObject titleScreen;
    public SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {

        this.GetComponent<SpawnManager>().enabled = false;
    }

    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        this.GetComponent<SpawnManager>().enabled = true;
        score = 0;
        UpdateScore(0);
        spawnRate /= difficulty;
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
