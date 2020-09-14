using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
  public GameObject TutorialScreen;
  public GameObject MainScreen;
  
  public AudioClip[] ClickClips;
  public AudioSource Source;

  private void Update()
  {
    if (Input.anyKeyDown == true && MainScreen.activeSelf == false && TutorialScreen.activeSelf == true)
    {
      int randomID = Random.Range(0, ClickClips.Length);
      AudioSource.PlayClipAtPoint(ClickClips[randomID], Camera.main.transform.position);

      MainScreen.SetActive(true);
      TutorialScreen.SetActive(false);
    }
  }

  public void StartGame()
  {
    int randomID = Random.Range(0, ClickClips.Length);
    AudioSource.PlayClipAtPoint(ClickClips[randomID], Camera.main.transform.position);

    SceneManager.LoadScene("GameScene");
  }

  public void Tutorial()
  {
    int randomID = Random.Range(0, ClickClips.Length);
    AudioSource.PlayClipAtPoint(ClickClips[randomID], Camera.main.transform.position);

    MainScreen.SetActive(false);
    TutorialScreen.SetActive(true);
  }

  public void Quit()
  {
    int randomID = Random.Range(0, ClickClips.Length);
    AudioSource.PlayClipAtPoint(ClickClips[randomID], Camera.main.transform.position);

    Application.Quit();
  }
}
