using UnityEngine;
using UnityEngine.SceneManagement;

public class FactoryManager : MonoBehaviour
{
  private void OnDestroy()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
