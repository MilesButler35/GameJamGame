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

    private bool touchingWeapon;
    private bool hasWeapon;
    private bool touchingSoldier;

    Transform soldier;
 

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        PickupWeapon();
        GiveWeapon();
    }

    private void FixedUpdate()
    {
        playerRb.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);
    }

    //Check if the object the player is colliding with is a weapon
    private void OnTriggerEnter2D(Collider2D other)
    {
        
            //Check to see if the player is colliding with a weapon, and mark true if the player is colliding with an item
            if ((other.CompareTag("Sword") || other.CompareTag("Bow") || other.CompareTag("WizardHat")) && currentItem == null)
            {
                
                     Debug.Log("Impact with " + other.tag);
                     //Take a reference to Collided Object
                     touchingWeapon = true;
                     currentItem = other.transform;
                   
                    
                
            }

            if (other.CompareTag("PaperSoldier") && currentItem != null)
            {
                Debug.Log("Impact with Soldier");
                touchingSoldier = true;
                soldier = other.transform;
            }
        
    }
    
    //Check if the player is leaving the vicinity of a weapon or unit
    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Sword") || other.CompareTag("Bow") || other.CompareTag("WizardHat")) && currentItem != null)
        {

            Debug.Log("Leaving " + other.tag);
            touchingWeapon = false;
            

        }

        if (other.CompareTag("PaperSoldier") && currentItem != null)
        {
            Debug.Log("Leaving Soldier");
            touchingSoldier = false;
            soldier = null;
        }
    }

    //Lets the player pick up a weapon if touchingWeapon is true
    void PickupWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Space) && touchingWeapon == true)
        {
            Debug.Log("Pickup Weapon");
            //Move it to carrying point 
            currentItem.position = carryLocation.position;

            //Make it a child of the player so that it moves along with the player
            currentItem.parent = transform;

            touchingWeapon = false;
            hasWeapon = true;

            if (hasWeapon == true)
            {
                Debug.Log("Player has a weapon");
            }
        }
    }
    void SetColor()
    {

    }

    void GiveWeapon()
    {
        if (touchingSoldier == true && Input.GetKeyDown(KeyCode.Space) && hasWeapon == true)
        {
            Debug.Log("Weapon Given");

            Weapon weapon = currentItem.GetComponent<Weapon>();
            PaperSoldierTransformation soldierTransformation = soldier.GetComponent<PaperSoldierTransformation>();
            soldierTransformation.PerformTransformation(weapon.WeaponType);
            Destroy(currentItem.gameObject);

            hasWeapon = false;
            currentItem = null;
            touchingWeapon = false;
        }
    }


}
