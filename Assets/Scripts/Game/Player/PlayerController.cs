using UnityEngine;

public class PlayerController : PlayerBehaviour
{
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        ControllerNonPhysic();
        LoopBehaviour();
    }

    private void FixedUpdate()
    {
        ControllerPhysic();
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
}
