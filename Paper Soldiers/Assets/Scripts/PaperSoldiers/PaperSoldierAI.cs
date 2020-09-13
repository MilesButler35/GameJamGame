﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSoldierAI : MonoBehaviour
{
  public ESoldierType Type;

  public float AttackRange = 1;
  public float AttackInterval = 1;
  private float _attackTimer;

  private Rigidbody2D _rigidbody2D;
  private EntityData _entityData;
  private EntityAttack _entityAttack;

  private FactoryManager _factoryManager;
  private Transform _factoryBound;
  private Transform _battlefieldBound;
  private bool _insideFactory = true;

  private Animator _animator;

  void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _entityData = GetComponent<EntityData>();
    _entityAttack = GetComponent<EntityAttack>();

    _animator = GetComponent<Animator>();

    _factoryManager = FindObjectOfType<FactoryManager>();
    _factoryBound = _factoryManager.Bound;

    _battlefieldBound = GameObject.FindGameObjectWithTag("BattlefieldBound").transform;
  }

  // Update is called once per frame
  void Update()
  {
    // While inside the Factory bound
    if(transform.position.x < _factoryBound.position.x)
    {
      float movementSpeed = 1;
      if(Type != ESoldierType.None)
      {
        movementSpeed = _entityData.MovementSpeed;
      }
      else
      {
        movementSpeed = _entityData.MovementSpeed / 5;
      }
      
      _animator.SetFloat("Speed", 1);
      _rigidbody2D.velocity = transform.right * movementSpeed;
      return;
    }
    else if(_insideFactory == true)
    {
      _insideFactory = false;
      _factoryManager.SoldierLeavedFactory();
    }

    // Battle AI
    if(_attackTimer > 0)
    {
      _attackTimer -= Time.deltaTime;
    }

    float distanceToTarget = float.MaxValue;
    Transform targetTransform =  FindClosesTarget(out distanceToTarget);
    if (targetTransform == null)
    {
      if(transform.position.x > _factoryBound.position.x + 1)
      {
        _animator.SetFloat("Speed", 1);
        _rigidbody2D.velocity = -transform.right * _entityData.MovementSpeed;
      }
      else
      {
        _animator.SetFloat("Speed", 0);
        _rigidbody2D.velocity = Vector2.zero;
      }
      return;
    }

    if (distanceToTarget < AttackRange)
    {
      _rigidbody2D.velocity = Vector2.zero;

      if(_attackTimer <= 0)
      {
        _animator.SetTrigger("Attack");
        _entityAttack.Attack(targetTransform.position);
        _attackTimer = AttackInterval;
      }

      return;
    }
    
    Vector2 movementDirection = (targetTransform.position - transform.position).normalized;
    _rigidbody2D.velocity = movementDirection * _entityData.MovementSpeed;
    _animator.SetFloat("Speed", 1);
  }

  private Transform FindClosesTarget(out float distanceToTarget)
  {
    GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

    Transform targetTransform = null;
    float minDistance = float.MaxValue;
    foreach (var enemy in allEnemies)
    {
      float dist = Vector2.Distance(transform.position, enemy.transform.position);
      if(enemy.transform.position.x < _battlefieldBound.position.x && dist < minDistance)
      {
        minDistance = dist;
        targetTransform = enemy.transform;
      }
    }

    distanceToTarget = minDistance;
    return targetTransform;
  }

  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(transform.position, AttackRange);
  }
}
