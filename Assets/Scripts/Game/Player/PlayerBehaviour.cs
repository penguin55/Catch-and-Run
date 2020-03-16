using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerPointer pointer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private bool onJump, onGround, onFall;

    protected Vector2 direction;

    // Setting up the first value of player after spawned
    protected void Initialize()
    {
        direction = Vector2.zero;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        onJump = false;
        onGround = false;
        onFall = false;
        if (photonView.IsMine)
        {
            pointer.SetActivePointer(true);
        }
    }

    // To setting the controller of this player
    public virtual void SetController(VariableJoystick joystick, Button button)
    {

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

    // To check when the player is falling or not and trigger the faling animation when the payer is falling
    protected void Fall()
    {
        if (rigidBody.velocity.y < -0.1f && !onGround && !onFall)
        {
            onFall = true;
            FallAnimation();
        }
    }

    // To flip the player image to left or right according to direction of its move
    private void Flipping()
    {
        if (direction.x > 0)
        {
            sprite.flipX = false;
            photonView.RPC("FlippingOverNetwork", RpcTarget.All, false);
        }
        else if (direction.x < 0)
        {
            sprite.flipX = true;
            photonView.RPC("FlippingOverNetwork", RpcTarget.All, true);
        }
    }

    // To flipping the player in other client device, so it will sync every time
    [PunRPC]
    public void FlippingOverNetwork(bool flag = false)
    {
        sprite.flipX = flag;
    }

    // To set the pointer status of this player
    public void SetPointerStatus(string command)
    {
        pointer.SetPointerStatus(command);

        if (command.ToLower().Equals("player"))
        {
            AnalyticsUnity.analytics.EndCatch((int) PhotonNetwork.Time);
        } 
        else if (command.ToLower().Equals("catcher"))
        {
            AnalyticsUnity.analytics.BeingCatch((int)PhotonNetwork.Time);
        }
    }

    // To change the pointer status of other player. The pointer will be appear for our player and other player when the other is being a catcher
    protected void PointerViewOtherClient()
    {
        if (GameManagement.catchStatus.currentCatch == photonView.Owner)
        {
            pointer.SetActivePointer(true);
            pointer.SetPointerStatus("catcher");
        } else
        {
            pointer.SetActivePointer(false);
        }
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

    public void ResetAnimation()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Fall", false);
    }
    #endregion

    // To trigger and change the pointer status with condition the interactable player is not the last catcher. Player who be the current catcher will updating the pointer
    // and we will toggle the pointer. If we are "not a catcher" we will being "a catcher" and otherwise.
    // This ontrigger function is also handle the ordering sprite with randomize, so it will randomly gives the order of sprite when two player is collide
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) return;

        if (collision.CompareTag("Player"))
        {
            sprite.sortingOrder = Random.Range(1,11);
            if (GameManagement.catchStatus.currentCatch == PhotonNetwork.LocalPlayer) {
                Player owner = collision.GetComponent<PhotonView>().Owner;
                if (GameManagement.catchStatus.lastCatch != owner)
                {
                    SetPointerStatus("player");
                    GameManagement.catchStatus.currentCatch = owner;
                    GameManagement.catchStatus.lastCatch = PhotonNetwork.LocalPlayer;
                    GameManagement.instance.ChangeCatcher(owner, PhotonNetwork.LocalPlayer);
                }
            }
        }
    }

    // Change back the order of sprite to 0 when not collide
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!photonView.IsMine) return;

        if (collision.CompareTag("Player"))
        {
            sprite.sortingOrder = 0;
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
