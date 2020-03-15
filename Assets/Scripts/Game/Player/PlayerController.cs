using UnityEngine;

public class PlayerController : PlayerBehaviour
{
    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        if (TimeManager.freeze)
        {
            ResetData();
            return;
        }

        if (photonView.IsMine)
        {
            ControllerNonPhysic();
            LoopBehaviour();
        } else
        {
            PointerViewOtherClient();
        }
    }

    private void FixedUpdate()
    {
        if (TimeManager.freeze)
        {
            ResetData();
            return;
        }

        if (photonView.IsMine)
        {
            ControllerPhysic();
        }
    }

    void LoopBehaviour()
    {
        Idle();
        Run();
        Fall();
    }

    void ControllerPhysic()
    {
        if (Input.GetButtonDown("Jump")) Jump();
    }

    void ControllerNonPhysic()
    {
        direction.x =  Input.GetAxis("Horizontal");
    }

    private void ResetData()
    {
        direction = Vector2.zero;
        ResetAnimation();
    }
}
