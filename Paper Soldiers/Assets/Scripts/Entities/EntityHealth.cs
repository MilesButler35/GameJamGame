using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
  private EntityData _entityData;

  void Start()
  {
    _entityData = GetComponent<EntityData>();
    _entityData.HealthPoints = _entityData.InitialHealthPoints;
  }

  public void ApplyDamage(int amount)
  {
    _entityData.HealthPoints -= amount;
    
    if(_entityData.HealthPoints <= 0)
    {
      Destroy(this.gameObject);
    }
  }
}
