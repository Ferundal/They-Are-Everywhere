using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private CannonController _cannonController;
    private Vector2 _rotation;
    private bool _isTriggerOn;

    [SerializeField] private Cannon cannon;

    private void Awake()
    {
        _cannonController = new CannonController();
        StateManager.Instance.OnStateChanged += OnStateChanged;
    }

    private void OnEnable()
    {
        _cannonController.Enable();
    }

    private void OnDestroy()
    {
        StateManager.Instance.OnStateChanged -= OnStateChanged;
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

        _rotation = context.ReadValue<Vector2>();

    }

    private void Shoot(InputAction.CallbackContext context)
    {
        _isTriggerOn = context.ReadValueAsButton();
        cannon.SetTrigger(_isTriggerOn);
    }

    private void Update()
    {
        if (_rotation != Vector2.zero) cannon.Rotate(_rotation);
    }

    private void OnStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
