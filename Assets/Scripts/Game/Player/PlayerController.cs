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

    void LoopBehaviour()
    {
        Idle();
        Run();
        Fall();
    }

    public void ControllerPhysic()
    {
        if (photonView.IsMine)
        {
            Jump();
        }   
    }

    void ControllerNonPhysic()
    {
        direction.x = joystick.Horizontal;
    }

    private void ResetData()
    {
        direction = Vector2.zero;
        ResetAnimation();
    }
}
