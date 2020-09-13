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
    if (amount <= 0)
      return;

    _entityData.HealthPoints -= amount;
    OnDamage(amount);

    if (_entityData.HealthPoints <= 0)
    {
      OnDeath?.Invoke();
      Death();
    }
  }

  protected virtual void Death()
  {
    Destroy(this.gameObject);
  }

  protected virtual void OnDamage(float amount)
  {
    UIFloatingTextsManager.Instance.ShowText(amount, transform);
  }
}
