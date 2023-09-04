using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour, IWeapon
{
    public static Cannon instance;

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
    [SerializeField] private Animator leftShutter;
    [SerializeField] private Animator rightShutter;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float ammoMass = 30;
    [SerializeField] private float force = 1000f;
    [SerializeField] private float offSet = 0.5f;
    [SerializeField] private float timeBeforeAmmoDestruction = 3f;
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField] private LineRenderer rightLineRenderer;
    [SerializeField] private TrajectoryLine trajectoryLine;
    [SerializeField] private GameObject smokeEffect;
    private bool _isLeftShooting = true;

    private float _lastShoot;
    public float AmmoMass
    {
        get
        {
            return ammoMass;
        }
    }

    public float Force
    {
        get
        {
            return force;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;

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
        trajectoryLine.ShowTrajectoryLine(leftMuzzle.position, leftMuzzle.forward * force / ammoMass, leftLineRenderer);
        trajectoryLine.ShowTrajectoryLine(rightMuzzle.position, rightMuzzle.forward * force / ammoMass, rightLineRenderer);

        if (isShooting && Time.time >= _lastShoot + offSet)
        {
            _lastShoot = Time.time;

            if (_isLeftShooting)
            {
                BarrelShoot(leftMuzzle);
                _isLeftShooting = false;
            }
            else
            {
                BarrelShoot(rightMuzzle);
                _isLeftShooting = true;
            }
        }
    }

    private void BarrelShoot(Transform muzzle)
    {
        if (muzzle.childCount == 0)
        {
            GameObject smoke = Instantiate(smokeEffect, muzzle.transform.position, smokeEffect.gameObject.transform.rotation);
            smoke.transform.parent = muzzle;
            smoke.transform.localScale = muzzle.localScale;
            StartCoroutine(DestroySmoke(smoke));
        }

        if (_isLeftShooting) leftShutter.SetTrigger("ShutterMove");
        else rightShutter.SetTrigger("ShutterMove");

        GameObject spawnedObject = AmmoObjectsPool.instance.GetPooledObject();

        if (spawnedObject != null)
        {
            spawnedObject.transform.position = muzzle.position;
            spawnedObject.gameObject.SetActive(true);
        }
        var rb = spawnedObject.GetComponent<Rigidbody>();
        rb.mass = AmmoMass;
        rb.AddForce(muzzle.forward * Force, ForceMode.Impulse);
        StartCoroutine(spawnedObject.GetComponent<AmmoExplosion>().DestroyAmmo(timeBeforeAmmoDestruction));
    }

    private IEnumerator DestroySmoke(GameObject smoke)
    {
        ParticleSystem parts = smoke.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetime.constant;

        yield return new WaitForSeconds(totalDuration);
        Destroy(smoke);
    }
}
