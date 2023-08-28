using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    private CannonController _cannonController;
    private bool _isShooting;
    private float lastShoot;

    [SerializeField] private Transform leftMuzzle;
    [SerializeField] private Transform rightMuzzle;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float cannonBallMass = 30;
    [SerializeField] private float force;
    [SerializeField] private float offSet;
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField] private LineRenderer rightLineRenderer;
    [SerializeField] private TrajectoryLine trajectoryLine;

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
        _cannonController.Movement.Shoot.performed += _ => _isShooting = _.ReadValueAsButton();
        _cannonController.Movement.Shoot.canceled += _ => _isShooting = false;
    }

    private void FixedUpdate()
    {
        trajectoryLine.ShowTrajectoryLine(leftMuzzle.position, leftMuzzle.up * force / cannonBallMass, leftLineRenderer);
        trajectoryLine.ShowTrajectoryLine(rightMuzzle.position, rightMuzzle.up * force / cannonBallMass, rightLineRenderer);
        if (_isShooting && Time.time >= lastShoot + offSet)
        {
            lastShoot = Time.time;
            GameObject ball1 = Instantiate(cannonBall, leftMuzzle.position, leftMuzzle.rotation);
            GameObject ball2 = Instantiate(cannonBall, rightMuzzle.position, rightMuzzle.rotation);
            ball1.GetComponent<Rigidbody>().mass = cannonBallMass;
            ball1.GetComponent<Rigidbody>().AddForce(leftMuzzle.up * force, ForceMode.Impulse);
            ball2.GetComponent<Rigidbody>().mass = cannonBallMass;
            ball2.GetComponent<Rigidbody>().AddForce(rightMuzzle.up * force, ForceMode.Impulse);
        }
    }
}
