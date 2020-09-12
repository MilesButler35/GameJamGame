using UnityEngine;

public class FactoryManager : MonoBehaviour
{
  public GameObject GameOverScreen;

  public GameObject PureSoldierPrefab;
  public float SpawnInterval = 2;

  public Transform Bound;

  private float _spawnTimer;
  private GameObject[] _spawnedSoldiers;
  private Transform[] _spawnPoints;

  private void Awake()
  {
    _spawnPoints = new Transform[transform.childCount];
    _spawnedSoldiers = new GameObject[transform.childCount];
    for (int i = 0; i < transform.childCount; i++)
    {
      _spawnPoints[i] = transform.GetChild(i);
    }

    _spawnTimer = SpawnInterval;

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

  private void Update()
  {
    bool canSpawn = CheckIfCanSpawn();

    if (canSpawn && _spawnTimer > 0)
    {
      _spawnTimer -= Time.deltaTime;

      if (_spawnTimer <= 0)
      {
        SpawnPureSoldier();
        _spawnTimer = SpawnInterval;
      }
    }

    UpdateSoldiersList();
  }

  private void SpawnPureSoldier()
  {
    bool spawnSuccessful = false;
    for (int i = 0; i < _spawnedSoldiers.Length; i++)
    {
      if (_spawnedSoldiers[i] == null)
      {
        _spawnedSoldiers[i] = Instantiate(PureSoldierPrefab, _spawnPoints[i].position, Quaternion.identity);
        spawnSuccessful = true;
        break;
      }
    }

    if(spawnSuccessful == false)
    {
      _spawnTimer = SpawnInterval;
    }
  }

  private bool CheckIfCanSpawn()
  {
    bool canSpawn = false;
    for (int i = 0; i < _spawnedSoldiers.Length; i++)
    {
      if (_spawnedSoldiers[i] == null)
      {
        canSpawn = true;
        break;
      }
    }

    if (canSpawn == false)
    {
      _spawnTimer = SpawnInterval;
    }

    return canSpawn;
  }

  private void UpdateSoldiersList()
  {
    for (int i = 0; i < _spawnedSoldiers.Length; i++)
    {
      if (_spawnedSoldiers[i] != null && _spawnedSoldiers[i].transform.position.x > Bound.position.x)
      {
        _spawnedSoldiers[i] = null;
      }
    }
  }
}
