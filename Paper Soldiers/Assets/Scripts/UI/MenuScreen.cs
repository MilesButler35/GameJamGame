using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
  public GameObject TutorialScreen;
  public GameObject MainScreen;

  private void Update()
  {
    if (Input.anyKeyDown == true && MainScreen.activeSelf == false && TutorialScreen.activeSelf == true)
    {
      MainScreen.SetActive(true);
      TutorialScreen.SetActive(false);
    }
  }

  public void StartGame()
  {
    SceneManager.LoadScene("GameScene");
  }

  public void Tutorial()
  {
    MainScreen.SetActive(false);
    TutorialScreen.SetActive(true);
  }

  public void Quit()
  {
    Application.Quit();
  }
}
