using UnityEngine;

public class FactoryManager : MonoBehaviour
{
  public GameObject GameOverScreen;

  private void Awake()
  {
    GetComponent<EntityHealth>().OnDeath += OnFactoryDestroyed;
  }

  private void OnFactoryDestroyed()
  {
    GameOverScreen.SetActive(true);

    PaperSoldierAI[] allPaperSoldiers = FindObjectsOfType<PaperSoldierAI>();
    foreach (var paperSoldier in allPaperSoldiers)
    {
      Destroy(paperSoldier.gameObject);
    }

    EnemyAI[] allEnemies = FindObjectsOfType<EnemyAI>();
    foreach (var enemy in allEnemies)
    {
      Destroy(enemy.gameObject);
    }
  }
}
