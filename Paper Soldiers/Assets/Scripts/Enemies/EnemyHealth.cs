using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : EntityHealth
{
  public int ScorePoints = 1;

  protected override void Death()
  {
    GameManager.Instance.UpdateScore(ScorePoints);
  }
}
