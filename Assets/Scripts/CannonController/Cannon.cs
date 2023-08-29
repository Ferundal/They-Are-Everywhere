using UnityEngine;

public class Cannon : MonoBehaviour, IWeapon
{
    [Header("Rotations")]

    [SerializeField] private GameObject turret;
    [SerializeField] private float movementOnYAxis;
    [SerializeField] private float movementOnXAxis;
    [SerializeField] private float _xRotation;
    [SerializeField] private float _yRotation;
    [SerializeField] private float _xMaxRotation = 10f;
    [SerializeField] private float _yMaxRotation = 50f;
    [SerializeField] private float _xMinRotation = -50f;
    [SerializeField] private float _yMinRotation = -50f;

    [Header("Shooting")]

    [SerializeField] private Transform leftMuzzle;
    [SerializeField] private Transform rightMuzzle;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float cannonBallMass = 30;
    [SerializeField] private float force = 1000f;
    [SerializeField] private float offSet = 0.5f;
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField] private LineRenderer rightLineRenderer;
    [SerializeField] private TrajectoryLine trajectoryLine;

    private float lastShoot;

    private void Awake()
    {
        leftLineRenderer.gameObject.SetActive(true);
        rightLineRenderer.gameObject.SetActive(true);
    }
    public void Rotate(Vector2 input)
    {
        _xRotation = turret.transform.eulerAngles.x;
        _yRotation = turret.transform.eulerAngles.y;

        _xRotation = turret.transform.eulerAngles.x + input.x;
        _xRotation = (_xRotation > 180) ? _xRotation - 360 : _xRotation;
        _xRotation = Mathf.Clamp(_xRotation, _xMinRotation, _xMaxRotation);
        _yRotation = turret.transform.eulerAngles.y + input.y;
        _yRotation = (_yRotation > 180) ? _yRotation - 360 : _yRotation;
        _yRotation = Mathf.Clamp(_yRotation, _yMinRotation, _yMaxRotation);
        turret.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    public void Shoot(bool isShooting)
    {
        trajectoryLine.ShowTrajectoryLine(leftMuzzle.position, leftMuzzle.up * force / cannonBallMass, leftLineRenderer);
        trajectoryLine.ShowTrajectoryLine(rightMuzzle.position, rightMuzzle.up * force / cannonBallMass, rightLineRenderer);

        if (isShooting && Time.time >= lastShoot + offSet)
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
