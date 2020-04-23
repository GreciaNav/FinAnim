using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour {
    private Animator ani;

    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private int extraJumps;
    public int extraJumpsValue;

    public GameObject spark; // To activate sparks, add a button for this later and use Instantiate(spark, transform.position, Quaternion.identity)

	// Use this for initialization
	void Start () {
        extraJumps = extraJumpsValue;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

    //Physics reelated component
    private void FixedUpdate()
    {
        //Movement 
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //flipping character
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    // Update is called once per frame
    void Update () {

        //TODO: checkRadius units?

        isGrounded = Physics2D.OverlapCircle(feetPos.position,checkRadius);

        //Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            ani.SetTrigger("takeOf");
            jumpTimeCounter = jumpTime;
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }

        if (Input.GetKey(KeyCode.Space))
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

        if (Input.GetKey(KeyCode.Space))
        {
            isJumping = false;
        }

        if(isGrounded)
        {
            ani.SetBool("isJumping", false);
        }

        else
        {
            ani.SetBool("isJumping", true);
        }
  

        //Movement Animations
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            ani.SetBool("isMoving", true);
        }
        else
        {
            ani.SetBool("isMoving", false);
        }

        //Sparks
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(spark, transform.position, Quaternion.identity);
        }


	}
}
