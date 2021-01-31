using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheckpoint, groundCheckpoint2;
    public LayerMask whatIsGround;
    public Animator anim;

    public AudioSource jumpSound;

    public bool isDead;

    public float speed;
    public float jumpForce;
    public float fastFallSpeed;
    private bool isGrounded = false;
    private int jumpCount = 0;
    public SpriteRenderer sr;

    public float hangTime = 0.2f;
    private float hangCounter;

    public float jumpBufferLength = 0.2f;
    private float jumpBufferCounter;

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
            //var movement = Input.GetAxisRaw("Horizontal");
            //transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
            float currentSpeed = Mathf.Abs(rb.velocity.x);
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (rb.velocity.x > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x * 0.3f, rb.velocity.y);
                }
                if (currentSpeed < speed)
                {
                    rb.velocity -= new Vector2(speed * Time.deltaTime * (4 + currentSpeed), 0);
                }
            } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (rb.velocity.x < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x * 0.3f, rb.velocity.y);
                }
                if (currentSpeed < speed)
                {
                    rb.velocity += new Vector2(speed * Time.deltaTime * (4 + currentSpeed), 0);
                }
            } else
            {
                if (rb.velocity.x > 0.3f)
                {
                    rb.velocity -= new Vector2(speed * Time.deltaTime * (3 + currentSpeed), 0);
                } else if (rb.velocity.x < -0.3f)
                {
                    rb.velocity += new Vector2(speed * Time.deltaTime * (3 + currentSpeed), 0);
                } else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }


                isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, 0.05f, whatIsGround) || Physics2D.OverlapCircle(groundCheckpoint2.position, 0.05f, whatIsGround);

            // Manage hangtime + reset jumpcount 
            if (isGrounded)
            {
                hangCounter = hangTime;
                jumpCount = 0;
            }
            else
            {
                hangCounter -= Time.deltaTime;
            }

            // Manage double jump
            if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumpCount == 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpSound.Play();
                jumpCount += 1;
            }

            // Manage jumpBuffer
            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                jumpBufferCounter = jumpBufferLength;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (jumpBufferCounter >= 0 && hangCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpSound.Play();
                jumpBufferCounter = 0;
            }

            // Jump height
            if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpCount += 1;
            }

            // Fastfall
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, -fastFallSpeed);
            }

            // Flips player sprite
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                sr.flipX = false;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                sr.flipX = true;
            }
        }
        

        // Handle animation states
        if (!isDead)
        {
            if (isGrounded)
            {
                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isJumping", false);

                }
                else
                {
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isJumping", false);
                }
            }
            else
            {
                if (jumpCount == 2)
                {
                    // if you just double jumped we have another jumping animation
                    anim.Play("Player Jump", -1, 0f);
                }
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isJumping", true);
            }
        }
    }
}
