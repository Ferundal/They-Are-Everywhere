using UnityEngine;

public class Controller : MonoBehaviour
{
    private CannonController _cannonController;
    private Vector2 rotation;
    private bool _isShooting;

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
        _cannonController.Movement.Rotation.performed += _ => rotation = _.ReadValue<Vector2>();
        _cannonController.Movement.Rotation.canceled += _ => rotation = new Vector2(0, 0);
        _cannonController.Movement.Shoot.performed += _ => _isShooting = _.ReadValueAsButton();
        _cannonController.Movement.Shoot.canceled += _ => _isShooting = false;
    }

    private void Update()
    {
        cannon.GetComponent<IWeapon>().Rotate(rotation);
        cannon.GetComponent<IWeapon>().Shoot(_isShooting);
    }
}
