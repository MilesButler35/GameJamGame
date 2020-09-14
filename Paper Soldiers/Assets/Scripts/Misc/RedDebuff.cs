using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDebuff : MonoBehaviour
{
  public int MaxStacksCount;

  public float IntervalToDeapply = 2;
  private float _deapplyTimer;

  private readonly float DAMAGE_AMOUNT = .1f;
  private readonly float DAMAGE_INTERVAL = .75f;

  private float _damageTimer;

  private EntityData _entityData;
  private int _stacksCount;

  private EntityHealth _entityHealth;

  public GameObject RedDebuffIcon;

  void Start()
  {
    _entityData = GetComponent<EntityData>();
    _entityHealth = GetComponent<EntityHealth>();
  }

  private void Update()
  {
    if (_stacksCount <= 0)
    {
      _damageTimer = 0;
      return;
    }

    if (_deapplyTimer > 0)
    {
      _deapplyTimer -= Time.deltaTime;

      if (_deapplyTimer <= 0)
      {
        Deapply();
        _deapplyTimer = IntervalToDeapply;
      }
    }

    if(_damageTimer > 0)
    {
      _damageTimer -= Time.deltaTime;

      if(_damageTimer <= 0)
      {
        _entityHealth.ApplyDamage(_stacksCount * DAMAGE_AMOUNT);
        _damageTimer = DAMAGE_INTERVAL;
      }
    }
  }

  public void Apply(int stacksAmount)
  {
    _stacksCount = Mathf.Min(_stacksCount + stacksAmount, MaxStacksCount);

    _deapplyTimer = IntervalToDeapply;
    if(_damageTimer <= 0)
    {
      _damageTimer = DAMAGE_INTERVAL;
    }

    if (RedDebuffIcon.activeSelf == false)
    {
      RedDebuffIcon.SetActive(true);
    }
    RedDebuffIcon.transform.localScale = Vector3.one * _stacksCount * .075f;
  }

  private void Deapply()
  {
    _stacksCount = Mathf.Max(_stacksCount - 1, 0);

    if (_stacksCount <= 0 && RedDebuffIcon.activeSelf == true)
    {
      RedDebuffIcon.SetActive(false);
    }
    RedDebuffIcon.transform.localScale = Vector3.one * _stacksCount * .075f;
  }
}
