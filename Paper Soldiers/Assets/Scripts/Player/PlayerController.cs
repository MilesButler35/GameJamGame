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
    }

    private void FixedUpdate()
    {
        playerRb.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);
    }
}
