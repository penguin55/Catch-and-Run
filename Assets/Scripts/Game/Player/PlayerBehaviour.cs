using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private bool onJump, onGround, onFall;

    protected Vector2 direction;

    protected void Initialize()
    {
        direction = Vector2.zero;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        onJump = false;
        onGround = false;
        onFall = false;
    }

    protected void Idle()
    {
        if (direction == Vector2.zero && !onFall && !onJump && onGround)
        {
            IdleAnimation();
        }
    }

    protected void Run()
    {
        if (!onJump && !onFall && direction != Vector2.zero) RunAnimation();

        transform.Translate(direction * speed * Time.deltaTime);
        Flipping();
    }

    protected void Jump()
    {
        if (!onJump && onGround && !onFall)
        {
            JumpAnimation();

            onJump = true;
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
    }

    protected void Grounded()
    {
        if (!onGround) GroundedAnimation();

        onGround = true;
        onJump = false;
        onFall = false;
    }

    protected void Fall()
    {
        if (rigidBody.velocity.y < -0.1f && !onGround && !onFall)
        {
            onFall = true;
            FallAnimation();
        }
    }

    private void Flipping()
    {
        if (direction.x > 0) sprite.flipX = false;
        else if (direction.x < 0) sprite.flipX = true;
    }

    #region Animation
    private void IdleAnimation()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Fall", false);
    }

    private void RunAnimation()
    {
        animator.SetBool("Run", true);
    }

    private void JumpAnimation()
    {
        animator.SetBool("Jump", true);
    }

    private void GroundedAnimation()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Fall", false);
        animator.SetTrigger("Land");
    }

    private void FallAnimation()
    {
        animator.SetBool("Fall", true);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Kena");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (rigidBody.velocity.y == 0) Grounded();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (rigidBody.velocity.y == 0) Grounded();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
