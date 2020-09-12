using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDebuff : MonoBehaviour
{
  public int MaxStacksCount;
  
  public float IntervalToDeapply = 2;
  private float _deapplyTimer;

  private readonly float SPEED_MODIFIER = .125f;

  private EntityData _entityData;
  private int _stacksCount;
  private float _originalSpeed;

  void Start()
  {
    _entityData = GetComponent<EntityData>();
    _originalSpeed = _entityData.MovementSpeed;
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
    _stacksCount = Mathf.Min(_stacksCount + stacksAmount, MaxStacksCount);
    _entityData.MovementSpeed = _originalSpeed - (float)_stacksCount * SPEED_MODIFIER;
    _entityData.MovementSpeed = Mathf.Max(_entityData.MovementSpeed, 0);
    _deapplyTimer = IntervalToDeapply;
  }

  private void Deapply()
  {
    _stacksCount = Mathf.Max(_stacksCount - 1, 0);
    _entityData.MovementSpeed = _originalSpeed - (float)_stacksCount * SPEED_MODIFIER;
    _entityData.MovementSpeed = Mathf.Max(_entityData.MovementSpeed, 0);
  }
}
