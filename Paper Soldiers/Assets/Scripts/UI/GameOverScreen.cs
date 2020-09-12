using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
  public void Retry()
  {
    SceneManager.LoadScene("GameScene");
  }

  public void ReturnToMenu()
  {
    SceneManager.LoadScene("MenuScene");
  }
}
