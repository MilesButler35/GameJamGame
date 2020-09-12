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

  public void ApplyDamage(float amount)
  {
    _entityData.HealthPoints -= amount;

    Debug.Log($"Damage caused {name}. Amount: {amount}. Remaining Health: {_entityData.HealthPoints}");

    if(_entityData.HealthPoints <= 0)
    {
      Destroy(this.gameObject);
    }
  }
}
