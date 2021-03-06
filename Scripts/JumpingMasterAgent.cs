using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class JumpingMasterAgent : Agent{

    [SerializeField] private Vector3 restartPos;
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

    private int movement;
    private int jump;

    private int count;

    public GameControl game;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        count = 0;
    }
    /**These are the observations that we pass that the AI learns from.
     * At the top, I attempted to pass position and size of each obstacle, but was giving me problems.
     **/
    public override void CollectObservations(VectorSensor sensor)
    {
        //GameObject.FindWithTag("Obstacle").transform.position;
        //GameObject tree = GameObject.Find("Snowy_Tree(Clone)");
        //GameObject snow = GameObject.Find("Snow_Flurry(Clone)");
        //GameObject rock = GameObject.Find("Rocks(Clone)");
        //GameObject jerry = GameObject.Find("Jerry(Clone)");

        //if( tree != null)
        //{
        //    sensor.AddObservation(tree.transform.position);
        //    sensor.AddObservation(tree.GetComponent<BoxCollider2D>().size);
        //}
        ////else
        ////{
        ////    sensor.AddObservation(Vector3.zero);
        ////    sensor.AddObservation(Vector2.zero);
        ////}
        //if (snow != null)
        //{
        //    sensor.AddObservation(snow.transform.position);
        //    sensor.AddObservation(snow.GetComponent<BoxCollider2D>().size);
        //}
        ////else
        ////{
        ////    sensor.AddObservation(Vector3.zero);
        ////    sensor.AddObservation(Vector2.zero);
        ////}
        //if (rock != null)
        //{
        //    sensor.AddObservation(rock.transform.position);
        //    sensor.AddObservation(rock.GetComponent<BoxCollider2D>().size);
        //}
        ////else
        ////{
        ////    sensor.AddObservation(Vector3.zero);
        ////    sensor.AddObservation(Vector2.zero);
        ////}
        //if (jerry != null)
        //{
        //    sensor.AddObservation(jerry.transform.position);
        //    sensor.AddObservation(jerry.GetComponent<BoxCollider2D>().size);
        //}
        ////else
        ////{
        ////    sensor.AddObservation(Vector3.zero);
        ////    sensor.AddObservation(Vector2.zero);
        ////}


        sensor.AddObservation(transform.position);
    }

    /**
     * Will choose random int in 0,1,2 for movement; and 0,1 for jump.
     * movement: 0 stay still, 1 to the right, 2 to the left
     * jump: 0 don't jump, 1 jump
    **/
    public override void OnActionReceived(ActionBuffers actions)
    {

        movement = Mathf.FloorToInt(actions.DiscreteActions[0]);
        jump = Mathf.FloorToInt(actions.DiscreteActions[1]);
        if (movement == 2)
        {
            movement = -1;
        }
    }
    /**
     * Penalized for running into an obstacle, count reward resets, and episode ends.
     **/
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            SetReward(-50f);
            count = 0;
            EndEpisode();
        }
    }

    //control of character based on movement and jump below
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);

    }
    /**
     * This is where the reward is given based on an incrementing count variable. Also is the control for when the character can jump.
     **/
    private void Update()
    {
        count++;
        print(count);
        SetReward((float)count);
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded && jump==1)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            //vector2.up is shorthand for (0,1)
            rb.velocity = Vector2.up * jumpForce;

        }
        if (jump==1 && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }
        if (jump==0)
        {
            isJumping = false;
        }
    }

}
