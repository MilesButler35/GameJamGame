using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryHealth : EntityHealth
{
  public Image HealthBar;

  public ObjectShaker ObjectShaker;

  protected override void OnDamage(float amount)
  {
    ObjectShaker.Begin();

    HealthBar.fillAmount = _entityData.HealthPoints / _entityData.InitialHealthPoints;
  }

  protected override void Death()
  {
  }
}
