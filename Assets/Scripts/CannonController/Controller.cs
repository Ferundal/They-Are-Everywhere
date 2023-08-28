using UnityEngine;

public class Controller : MonoBehaviour
{
    private CannonController _cannonController;

    [SerializeField] private GameObject turret;
    [SerializeField] private float movementOnYAxis;
    [SerializeField] private float movementOnXAxis;

    [SerializeField] private float _xRotation;
    [SerializeField] private float _yRotation;
    [SerializeField] private float _xMaxRotation = 10f;
    [SerializeField] private float _yMaxRotation = 50f;
    [SerializeField] private float _xMinRotation = -50f;
    [SerializeField] private float _yMinRotation = -50f;

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
        _cannonController.Movement.LeftRightRotation.performed += _ => movementOnYAxis = _.ReadValue<float>();
        _cannonController.Movement.LeftRightRotation.canceled += _ => movementOnYAxis = 0;
        _cannonController.Movement.UpDownRotation.performed += _ => movementOnXAxis = _.ReadValue<float>();
        _cannonController.Movement.UpDownRotation.canceled += _ => movementOnXAxis = 0;
    }

    private void Update()
    {
        _xRotation = turret.transform.eulerAngles.x;
        _yRotation = turret.transform.eulerAngles.y;

        if (_xRotation <= _xMinRotation || _xRotation >= _xMaxRotation)
        {
            _xRotation = turret.transform.eulerAngles.x + movementOnXAxis;
        }
        else if (_xRotation > _xMinRotation && _xRotation < 180f)
        {
            _xRotation -= 0.1f;
        }
        else if (_xRotation < _xMaxRotation && _xRotation > 180f)
        {
            _xRotation += 0.1f;
        }

        if (_yRotation <= _yMinRotation || _yRotation >= _yMaxRotation)
        {
            _yRotation = turret.transform.eulerAngles.y + movementOnYAxis;
        }
        else if (_yRotation > _yMinRotation && _yRotation < 180f)
        {
            _yRotation -= 0.1f;
        }
        else if (_yRotation < _yMaxRotation && _yRotation > 180f)
        {
            _yRotation += 0.1f;
        }

        turret.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
}
