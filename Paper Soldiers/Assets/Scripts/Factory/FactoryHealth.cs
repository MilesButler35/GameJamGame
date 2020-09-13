using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryHealth : EntityHealth
{
  public Image HealthBar;

  [SerializeField] private ObjectShaker _objectShaker;

  protected override void OnDamage(float amount)
  {
    _objectShaker.Begin();

    HealthBar.fillAmount = _entityData.HealthPoints / _entityData.InitialHealthPoints;
  }

  protected override void Death()
  {
  }
}
