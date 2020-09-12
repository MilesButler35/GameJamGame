using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{



    
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

    }

    //Starts the game when the corresponding button is pressed
    public void StartGame()
    {
        titleScreen.gameObject.SetActive(false); //Turns off the Title Text
        isGameActive = true; // Sets the game to active mode, which interacts with the spawn manager
        this.GetComponent<SpawnManager>().enabled = true; //Sets the spawnmanager to on.
        score = 0; // Makes sure the score is 0.
        UpdateScore(0); 
        UpdateWave(1); //Makes sure the Wave number is 1.
        
    }

    //Makes sure to stop the wave spawner if the Game is over
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

    public void UpdateWave(int waveNumber)
    {
        waveNumber = spawnManager.waveNumber;
    }
}
