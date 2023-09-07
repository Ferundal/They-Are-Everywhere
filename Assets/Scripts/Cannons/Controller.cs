using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private CannonController _cannonController;
    private Vector2 rotation;
    private bool isTriggerOn;

    [SerializeField] private Cannon cannon;

    private void Awake()
    {
        _cannonController = new CannonController();
    }

    private void OnEnable()
    {
        _cannonController.Enable();
    }

    private void OnDisable()
    {
        _cannonController.Disable();
    }

    private void Start()
    {
        _cannonController.Movement.Rotation.performed += Rotate;
        _cannonController.Movement.Rotation.canceled += Rotate;
        _cannonController.Movement.Shoot.performed += Shoot;
        _cannonController.Movement.Shoot.canceled += Shoot;
    }

    private void Rotate(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        isTriggerOn = context.ReadValueAsButton();
        cannon.SetTrigger(isTriggerOn);
    }

    private void Update()
    {
        if (rotation != Vector2.zero) cannon.Rotate(rotation);
    }
}
