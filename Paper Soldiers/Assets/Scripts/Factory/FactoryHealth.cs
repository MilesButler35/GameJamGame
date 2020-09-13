using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryHealth : EntityHealth
{
  [SerializeField] private ObjectShaker _objectShaker;

  protected override void OnDamage(float amount)
  {
    _objectShaker.Begin();
  }

  protected override void Death()
  {
  }
}
