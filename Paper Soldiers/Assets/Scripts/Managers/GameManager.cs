using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  private static GameManager _instance;
  public static GameManager Instance => _instance;

  private int score;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI waveText;
  public TextMeshProUGUI gameOverText;
  public TextMeshProUGUI enemiesLeftText;

  public bool isGameActive;
  public Button restartButton;

  public GameObject titleScreen;
  public SpawnManager spawnManager;

  public GameObject playerObject;
  public GameObject playerSpawn;

  private void Awake()
  {
    if(_instance == null)
    {
      _instance = this;
    }
  }

  //Starts the game when the corresponding button is pressed
  public void StartGame()
  {
    titleScreen.gameObject.SetActive(false); //Turns off the Title Text
    isGameActive = true; // Sets the game to active mode, which interacts with the spawn manager
    this.GetComponent<SpawnManager>().enabled = true; //Sets the spawnmanager to on.
    score = 0; // Makes sure the score is 0.
    UpdateScore(0);
    //UpdateWave(1); //Makes sure the Wave number is 1.
    Instantiate(playerObject, playerSpawn.transform.position, Quaternion.identity); // Spawn player character
  }

  private void Update()
  {
    UpdateWave();
    UpdateEnemiesLeft();
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

  public void UpdateScore(int scoreToAdd)
  {
    score += scoreToAdd;
    scoreText.text = "Score: " + score;
  }

  public void UpdateWave()
  {
    waveText.text = "Wave: " + spawnManager.waveNumber;
  }

  public void UpdateEnemiesLeft()
  {
    enemiesLeftText.text = "Enemies Left: " + spawnManager.enemyCount;
  }
}
