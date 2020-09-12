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

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        PickupWeapon();
    }

    private void FixedUpdate()
    {
        playerRb.velocity = new Vector2(horizontalMove * moveSpeed * Time.deltaTime, verticalMove * moveSpeed * Time.deltaTime);
    }

    //Check if the object the player is colliding with is a weapon
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Pickup an item if it has the right tag and the player isn't holding anything else
        
            if ((other.CompareTag("Sword") || other.CompareTag("Bow") || other.CompareTag("WizardHat")) && currentItem == null)
            {

                     Debug.Log("Impact with Weapon");
                    //Take a reference to Collided Object
                    currentItem = other.transform;
                    touchingWeapon = true;
                
            }
        
    }
    
    //Check if the player is leaving the vicinity of a weapon or unit
    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Sword") && currentItem == null || other.CompareTag("Bow") || other.CompareTag("WizardHat")) && currentItem == null)
        {

            Debug.Log("Leaving Weapon");
            
            currentItem = null;
            touchingWeapon = false;

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
        }
    }
    void SetColor()
    {

    }

    void GiveWeapon()
    {

    }


}
