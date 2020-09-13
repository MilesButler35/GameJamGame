using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : EntityHealth
{
  public Image HealthBar;

  public int ScorePoints = 1;

  protected override void Death()
  {
    base.Death();
    GameManager.Instance.UpdateScore(ScorePoints);
  }

  protected override void OnDamage(float amount)
  {
    base.OnDamage(amount);

    HealthBar.fillAmount = _entityData.HealthPoints / _entityData.InitialHealthPoints;
  }
}
