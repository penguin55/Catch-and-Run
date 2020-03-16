using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PlayerBehaviour
{

    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Button jumpButton;

    void Awake()
    {
        Initialize();
    }

    // Setting the controller of this player
    public override void SetController(VariableJoystick joystick, Button button)
    {
        this.joystick = joystick;
        jumpButton = button;

        jumpButton.onClick.AddListener(ControllerPhysic);
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

    // Looping behaviour of player where checks every frame
    void LoopBehaviour()
    {
        Idle();
        Run();
        Fall();
    }

    // To controll the player physic (jump mechanicsm)
    public void ControllerPhysic()
    {
        if (photonView.IsMine)
        {
            Jump();
        }   
    }

    // To controll movement of player
    void ControllerNonPhysic()
    {
        direction.x = joystick.Horizontal;
    }

    // To reset state of animation and direction move to normal state
    private void ResetData()
    {
        direction = Vector2.zero;
        ResetAnimation();
    }
}
