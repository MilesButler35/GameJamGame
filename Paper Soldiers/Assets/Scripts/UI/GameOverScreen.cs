using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
  public AudioClip[] ClickClips;

  public void Retry()
  {
    int randomID = Random.Range(0, ClickClips.Length);
    AudioSource.PlayClipAtPoint(ClickClips[randomID], Camera.main.transform.position);

    SceneManager.LoadScene("GameScene");
  }

  public void ReturnToMenu()
  {
    int randomID = Random.Range(0, ClickClips.Length);
    AudioSource.PlayClipAtPoint(ClickClips[randomID], Camera.main.transform.position);

    SceneManager.LoadScene("MenuScene");
  }
}
