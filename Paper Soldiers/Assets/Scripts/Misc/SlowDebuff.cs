using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDebuff : MonoBehaviour
{
  public int MaxStacksCount;
  
  public float IntervalToDeapply = 1;
  private float _deapplyTimer;

  private readonly float SPEED_MODIFIER = .25f;

  private EntityData _entityData;
  private int _stacksCount;

  void Start()
  {
    _entityData = GetComponent<EntityData>();
  }

  private void Update()
  {
    if (_stacksCount <= 0)
      return;

    if(_deapplyTimer > 0)
    {
      _deapplyTimer -= Time.deltaTime;
      
      if(_deapplyTimer <= 0)
      {
        Deapply();
        _deapplyTimer = IntervalToDeapply;
      }
    }
  }

  public void Apply(int stacksAmount)
  {
    _stacksCount = Mathf.Max(_stacksCount + 1, MaxStacksCount);
    _entityData.MovementSpeed = _entityData.MovementSpeed - _stacksCount * SPEED_MODIFIER;
  }

  public void Deapply()
  {
    _stacksCount = Mathf.Max(_stacksCount - 1, 0);
    _entityData.MovementSpeed = _entityData.MovementSpeed - _stacksCount * SPEED_MODIFIER;
  }
}
