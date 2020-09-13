using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSoldierHealth : EntityHealth
{
  private Animator _animator;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  protected override void Death()
  {
    StartCoroutine(DeathCoroutine());
  }

  private IEnumerator DeathCoroutine()
  {
    GetComponent<PaperSoldierAI>().enabled = false;
    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    GetComponent<Collider2D>().enabled = false;
    GetComponent<SpriteRenderer>().sortingLayerName = "DeadPaperSoldiers";
    _animator.SetTrigger("Death");
    yield return new WaitForSeconds(3);
    Destroy(this.gameObject);
  }
}
