using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
  public GameObject GameOverScreen;

  public GameObject PureSoldierPrefab;
  public float SpawnInterval = 2;

  public Transform Bound;

  private float _spawnTimer;
  private int _soldiersAtFactory;
  private Transform[] _spawnPoints;

  private const int MAX_UNITS_FACTORY = 3;

  private void Awake()
  {
    _spawnPoints = new Transform[transform.childCount];
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

    ConveryourBelt[] allBelts = FindObjectsOfType<ConveryourBelt>();
    foreach (var belt in allBelts)
    {
      belt.enabled = false;
    }

    Animator[] spawnersAnimators = FindObjectsOfType<Animator>();
    foreach (var spawnerAnimator in spawnersAnimators)
    {
      spawnerAnimator.speed = 0;
    }

    SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
    spawnManager.enabled = false;

    PlayerController playerController = FindObjectOfType<PlayerController>();
    Destroy(playerController.gameObject);
  }

  private void Update()
  {
    bool canSpawn = _soldiersAtFactory < MAX_UNITS_FACTORY;

    if (canSpawn && _spawnTimer > 0)
    {
      _spawnTimer -= Time.deltaTime;

      if (_spawnTimer <= 0)
      {
        SpawnPureSoldier();
        _spawnTimer = SpawnInterval;
      }
    }
  }

  private void SpawnPureSoldier()
  {
    bool spawnSuccessful = false;
    if (_soldiersAtFactory < MAX_UNITS_FACTORY)
    {
      int randomID = Random.Range(0, _spawnPoints.Length);
      _spawnPoints[randomID].GetComponentInChildren<Animator>().SetTrigger("Spawn");
      _soldiersAtFactory++;
      spawnSuccessful = true;
    }

    if (spawnSuccessful == false)
    {
      _spawnTimer = SpawnInterval;
    }
  }

  public void SoldierLeavedFactory()
  {
    _soldiersAtFactory--;
  }
}
