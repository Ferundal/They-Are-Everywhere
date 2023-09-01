using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour, IWeapon
{
    [Header("Rotations")]

    [SerializeField] private GameObject turret;
    [SerializeField] private float _xMaxRotation = 10f;
    [SerializeField] private float _yMaxRotation = 50f;
    [SerializeField] private float _xMinRotation = -50f;
    [SerializeField] private float _yMinRotation = -50f;
    [SerializeField] private float rotationSpeed = 35f;

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
    [SerializeField] private GameObject smokeEffect;

    private float lastShoot;

    private void Awake()
    {
        leftLineRenderer.gameObject.SetActive(true);
        rightLineRenderer.gameObject.SetActive(true);
    }

    public void Rotate(Vector2 input)
    {
        float _xRotation = turret.transform.eulerAngles.x;
        float _yRotation = turret.transform.eulerAngles.y;

        input = input * rotationSpeed * Time.deltaTime;
        _xRotation = turret.transform.eulerAngles.x + (-input.y);
        _xRotation = (_xRotation > 180) ? _xRotation - 360 : _xRotation;
        _xRotation = Mathf.Clamp(_xRotation, _xMinRotation, _xMaxRotation);
        _yRotation = turret.transform.eulerAngles.y + input.x;
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
            GameObject smoke1 = Instantiate(smokeEffect, leftMuzzle.transform.position, smokeEffect.gameObject.transform.rotation);
            GameObject smoke2 = Instantiate(smokeEffect, leftMuzzle.transform.position, smokeEffect.gameObject.transform.rotation);
            StartCoroutine(DestroySmoke(smoke1));
            StartCoroutine(DestroySmoke(smoke2));
            GameObject ball1 = CannonBallPool.instance.GetPooledObject();
            
            if (ball1 != null)
            {
                ball1.transform.position = leftMuzzle.position;
                ball1.transform.rotation = leftMuzzle.rotation;
                ball1.gameObject.SetActive(true);
                ball1.GetComponent<MeshRenderer>().enabled = true;
            }
            GameObject ball2 = CannonBallPool.instance.GetPooledObject();

            if (ball2 != null)
            {
                ball2.transform.position = rightMuzzle.position;
                ball2.transform.rotation = rightMuzzle.rotation;
                ball2.gameObject.SetActive(true);
                ball2.GetComponent<MeshRenderer>().enabled = true;
            }
            var rb1 = ball1.GetComponent<Rigidbody>();
            var rb2 = ball2.GetComponent<Rigidbody>();
            rb1.mass = cannonBallMass;
            rb2.mass = cannonBallMass;
            rb1.AddForce(leftMuzzle.up * force, ForceMode.Impulse);
            rb2.AddForce(rightMuzzle.up * force, ForceMode.Impulse);
            StartCoroutine(ball1.GetComponent<CannonBallExplosion>().DestroyBall(3f));
            StartCoroutine(ball2.GetComponent<CannonBallExplosion>().DestroyBall(3f));
        }
    }

    private IEnumerator DestroySmoke(GameObject smoke)
    {
        ParticleSystem parts = smoke.GetComponent<ParticleSystem>();
        float totalDuration = parts.duration + parts.startLifetime;

        yield return new WaitForSeconds(totalDuration);
        Destroy(smoke);
    }
}
