using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDebuff : MonoBehaviour
{
  public int MaxStacksCount;
  
  public float IntervalToDeapply = 2;
  private float _deapplyTimer;

  private readonly float SPEED_MODIFIER = .075f;

  private EntityData _entityData;
  private int _stacksCount;
  private float _originalSpeed;

  public GameObject BlueDebuffIcon;

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

    if (BlueDebuffIcon.activeSelf == false)
    {
      BlueDebuffIcon.SetActive(true);
    }
    BlueDebuffIcon.transform.localScale = Vector3.one * _stacksCount * .09f;
  }

  private void Deapply()
  {
    _stacksCount = Mathf.Max(_stacksCount - 1, 0);
    _entityData.MovementSpeed = _originalSpeed - (float)_stacksCount * SPEED_MODIFIER;
    _entityData.MovementSpeed = Mathf.Max(_entityData.MovementSpeed, 0);

    if (_stacksCount <= 0 && BlueDebuffIcon.activeSelf == true)
    {
      BlueDebuffIcon.SetActive(false);
    }
    BlueDebuffIcon.transform.localScale = Vector3.one * _stacksCount * .09f;
  }
}
