using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
  public Action OnDeath;

  private EntityData _entityData;

  void Start()
  {
    _entityData = GetComponent<EntityData>();
    _entityData.HealthPoints = _entityData.InitialHealthPoints;
  }

  public void ApplyDamage(float amount)
  {
    _entityData.HealthPoints -= amount;

    if(_entityData.HealthPoints <= 0)
    {
      OnDeath?.Invoke();
      Destroy(this.gameObject);
    }
  }
}
