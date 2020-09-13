using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFloatingTextsManager : MonoBehaviour
{
  public static UIFloatingTextsManager Instance { get; private set; }

  [SerializeField] private GameObject FloatingTextPrefab = null;
  [SerializeField] private int InitialPoolSize = 8;

  private Dictionary<GameObject, TextMeshPro> _pool = new Dictionary<GameObject, TextMeshPro>();

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
    }
    else
    {
      Instance = this;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < InitialPoolSize; i++)
    {
      AddToPool();
    }
  }

  private KeyValuePair<GameObject, TextMeshPro> AddToPool()
  {
    GameObject popupObject = Instantiate(FloatingTextPrefab, Vector3.zero, Quaternion.identity, this.transform);
    popupObject.SetActive(false);

    TextMeshPro popupText = popupObject.GetComponentInChildren<TextMeshPro>();
    
    var newEntry = new KeyValuePair<GameObject, TextMeshPro>(popupObject, popupText);
    _pool.Add(popupObject, popupText);

    return newEntry;
  }

  private KeyValuePair<GameObject, TextMeshPro> GetFromPool()
  {
    foreach (var kvp in _pool)
    {
      if(kvp.Key.activeSelf == false)
      {
        return kvp;
      }
    }

    return AddToPool();
  }

  public void ShowText(float amount, Transform targetTransform)
  {
    var entry = GetFromPool();
    entry.Key.transform.position = (Vector2)targetTransform.position + Vector2.up * .5f;
    entry.Value.text = amount.ToString();
    entry.Key.gameObject.SetActive(true);
  }
}
