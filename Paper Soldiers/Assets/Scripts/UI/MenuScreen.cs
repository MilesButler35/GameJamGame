using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
  public void StartGame()
  {
    SceneManager.LoadScene("GameScene");
  }

  public void Tutorial()
  {
  }

  public void Quit()
  {
    Application.Quit();
  }
}
