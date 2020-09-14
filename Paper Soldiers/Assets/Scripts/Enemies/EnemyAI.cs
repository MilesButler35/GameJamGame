using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
  public float DamageToFactory = 1;

  public float AttackRange = 1;
  public float AttackInterval = 1;
  private float _attackTimer;

  private Rigidbody2D _rigidbody2D;
  private EntityData _entityData;
  private EntityAttack _entityAttack;

  private Animator _animator;

  public Vector2 AttackPosition;

  public AudioClip AttackClip;
  public AudioClip AttackFactoryClip;

  void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _entityData = GetComponent<EntityData>();
    _entityAttack = GetComponent<EntityAttack>();
    _animator = GetComponentInChildren<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (_attackTimer > 0)
    {
      _attackTimer -= Time.deltaTime;
    }

    float distanceToTarget = float.MaxValue;
    Transform targetTransform = FindClosesTarget(out distanceToTarget);
    if (targetTransform != null)
    {
      _rigidbody2D.velocity = Vector2.zero;

      if (_attackTimer <= 0)
      {
        _animator.SetTrigger("Attack");
        AttackPosition = targetTransform.position;

        AudioSource.PlayClipAtPoint(AttackClip, Camera.main.transform.position);

        //_entityAttack.Attack(targetTransform.position); CALLED NOW FROM THE ANIMATION
        _attackTimer = AttackInterval;
      }

      return;
    }

    _rigidbody2D.velocity = -transform.right * _entityData.MovementSpeed;
  }

  private Transform FindClosesTarget(out float distanceToTarget)
  {
    Collider2D[] closeEnemies = Physics2D.OverlapCircleAll(transform.position, AttackRange);

    Transform targetTransform = null;
    float minDistance = float.MaxValue;
    foreach (var enemy in closeEnemies)
    {
      if (enemy.gameObject == this.gameObject)
        continue;

      if (enemy.tag != "PaperSoldier")
        continue;

      float dist = Vector2.Distance(transform.position, enemy.transform.position);
      if (dist < minDistance)
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

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if(collider.tag == "Factory")
    {
      EntityHealth factoryHealth = collider.GetComponent<EntityHealth>();
      if(factoryHealth != null)
      {
        AudioSource.PlayClipAtPoint(AttackFactoryClip, Camera.main.transform.position);

        factoryHealth.ApplyDamage(DamageToFactory);
        Destroy(this.gameObject);
      }
    }
  }
}
