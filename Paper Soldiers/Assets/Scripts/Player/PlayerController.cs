using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  Vector2 movement;
  public float moveSpeed = 5f;

  public Rigidbody2D playerRb;

  float horizontalMove;
  float verticalMove;

  public Transform carryLocation;
  Transform currentItem = null;
  Transform touchedWeapon = null;

  private bool touchingWeapon;
  private bool hasWeapon;
  private bool touchingSoldier;
  private bool touchingPaintBucket;

  Transform soldier;
  GameObject paintBucket;

  private Transform _factoryBound;

  private Animator _animator;

  // Start is called before the first frame update
  void Start()
  {
    playerRb = GetComponent<Rigidbody2D>();
    _animator = GetComponentInChildren<Animator>();

    _factoryBound = FindObjectOfType<FactoryManager>().Bound;
  }

  // Update is called once per frame
  void Update()
  {
    horizontalMove = Input.GetAxisRaw("Horizontal");
    verticalMove = Input.GetAxisRaw("Vertical");

    PickupWeapon();
    GiveWeapon();
    PaintWeapon();
  }

  private void FixedUpdate()
  {
    float horizontal = horizontalMove * moveSpeed;
    float vertical = verticalMove * moveSpeed;
    playerRb.velocity = new Vector2(horizontal, vertical);

    if (transform.position.x  > _factoryBound.position.x)
    {
      Vector3 correctedPosition = transform.position;
      correctedPosition.x = _factoryBound.position.x;
      transform.position = correctedPosition;
    }
  }

  //Check if the object the player is colliding with is a weapon
  private void OnTriggerEnter2D(Collider2D other)
  {
    //Check to see if the player is colliding with a weapon, and mark true if the player is colliding with an item
    if ((other.CompareTag("Sword") || other.CompareTag("Bow") || other.CompareTag("WizardHat")) && touchedWeapon == null)
    {

      //Debug.Log("Impact with " + other.tag);
      //Take a reference to Collided Object
      touchingWeapon = true;
      touchedWeapon = other.transform;
    }

    //if (other.CompareTag("PaperSoldier") && currentItem != null)
    //{
    //  Debug.Log("Impact with Soldier");
    //  touchingSoldier = true;
    //  soldier = other.transform;
    //}

    if (other.CompareTag("PaintBucket"))
    {
      //Debug.Log("Impact with Paint Bucket");
      touchingPaintBucket = true;
      paintBucket = other.gameObject;
    }
  }

  //Check if the player is leaving the vicinity of a weapon or unit
  private void OnTriggerExit2D(Collider2D other)
  {
    if ((other.CompareTag("Sword") || other.CompareTag("Bow") || other.CompareTag("WizardHat")) && touchedWeapon != null)
    {

      //Debug.Log("Leaving " + other.tag);
      touchingWeapon = false;
      touchedWeapon = null;
    }

    //if (other.CompareTag("PaperSoldier") && currentItem != null)
    //{
    //  Debug.Log("Leaving Soldier");
    //  touchingSoldier = false;
    //  soldier = null;
    //}

    if (other.CompareTag("PaintBucket"))
    {
      //Debug.Log("Leaving Paint Bucket");
      touchingPaintBucket = false;
      paintBucket = null;
    }
  }

  //Lets the player pick up a weapon if touchingWeapon is true
  void PickupWeapon()
  {
    if (Input.GetKeyDown(KeyCode.Space) && touchingWeapon == true)
    {
      //Debug.Log("Pickup Weapon");
      //Move it to carrying point
      currentItem = touchedWeapon;
      currentItem.position = carryLocation.position;

      //Make it a child of the player so that it moves along with the player
      currentItem.parent = carryLocation;
      currentItem.rotation = carryLocation.rotation;

      touchingWeapon = false;
      hasWeapon = true;

      if (hasWeapon == true)
      {
        //Debug.Log("Player has a weapon");
      }

      _animator.SetBool("Holding", true);
    }
  }

  void GiveWeapon()
  {
    if (Input.GetKeyDown(KeyCode.Space) && hasWeapon == true)
    {
      //Debug.Log("Weapon Given");

      Collider2D[] soldiers = Physics2D.OverlapCircleAll(transform.position, 1, 1 << LayerMask.NameToLayer("PaperSoldier"));
      float closestDistance = float.MaxValue;
      GameObject closestSoldier = null;
      foreach (var soldier in soldiers)
      {
        float distanceToSoldier = Vector2.Distance(transform.position, soldier.transform.position);
        if(distanceToSoldier < closestDistance)
        {
          closestDistance = distanceToSoldier;
          closestSoldier = soldier.gameObject;
        }
      }

      if (closestSoldier == null)
        return;

      Weapon weapon = currentItem.GetComponent<Weapon>();
      PaperSoldierTransformation soldierTransformation = closestSoldier.GetComponent<PaperSoldierTransformation>();

      if (soldierTransformation == null)
        return;

      soldierTransformation.PerformTransformation(weapon.TransformationType);
      Destroy(currentItem.gameObject);

      hasWeapon = false;
      currentItem = null;
      touchingWeapon = false;

      _animator.SetBool("Holding", false);
    }
  }

  private void PaintWeapon()
  {
    if(Input.GetKeyDown(KeyCode.Space) && touchingPaintBucket && hasWeapon == true)
    {
      PaintColor targetColor = paintBucket.GetComponent<PaintBucket>().MyColor;
      currentItem.GetComponent<Weapon>().TransformWeapon(targetColor);
    }
  }
}
