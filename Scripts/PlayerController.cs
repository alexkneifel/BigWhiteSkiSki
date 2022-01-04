using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float moveInput;
    public float jumpForce;
    private bool isJumping;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    //private bool robot;

    //public PlayerController(){
    //    print("instantiated player controller");
    //    robot = false; 
    //    }
    //public PlayerController(int Robot)
    //{
    //    print("instantiated robot controller");
    //    robot = true;
    //}
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        //print("Horizontal movement: ");
        //print(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position,checkRadius,whatIsGround);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            //vector2.up is shorthand for (0,1)
            rb.velocity = Vector2.up * jumpForce;

        }
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                    //print("Up Velocity: ");
                    //print(rb.velocity);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
}
