using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float glideGravity = 0.5f;
    public float normalGravity = 2.5f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isGliding;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (Input.GetButton("Jump") && !isGrounded)
        {
            StartGlide();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            StopGlide();
        }
    }

    void StartGlide()
    {
        if (!isGliding)
        {
            isGliding = true;
            rb.gravityScale = glideGravity;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f); // Reduce downward speed
        }
    }

    void StopGlide()
    {
        if (isGliding)
        {
            isGliding = false;
            rb.gravityScale = normalGravity;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            StopGlide();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
