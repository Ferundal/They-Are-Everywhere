using Assets.Scripts.Cannons.DualCannon;
using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour, IWeapon
{
    [Header("Rotations")]

    [SerializeField] private GameObject turret;
    [SerializeField] private float xMaxRotation = 10f;
    [SerializeField] private float yMaxRotation = 50f;
    [SerializeField] private float xMinRotation = -50f;
    [SerializeField] private float yMinRotation = -50f;
    [SerializeField] private float rotationSpeed = 35f;

    [Header("Shooting")]

    [SerializeField] private AmmunitionPool ammunitionPool;
    [SerializeField] private Barrel leftBarrel;
    [SerializeField] private Barrel rightBarrel;
    [SerializeField] private float offSet = 0.5f;
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField] private LineRenderer rightLineRenderer;
    [SerializeField] private TrajectoryLine trajectoryLine;
    private bool _isTriggerOn = false;
    private Coroutine _fireCycle;
    private Ammunition _currentAmmunition;

    private float _lastShoot;

    private void Awake()
    {
        leftBarrel.ammunitionPool = ammunitionPool;
        rightBarrel.ammunitionPool = ammunitionPool;
        leftLineRenderer.gameObject.SetActive(true);
        rightLineRenderer.gameObject.SetActive(true);
    }

    private void Start()
    {
        _currentAmmunition = ammunitionPool.ammunitionPrefub;
        trajectoryLine.ShowTrajectoryLine(leftBarrel.muzzle.position, leftBarrel.muzzle.forward * _currentAmmunition.Force / _currentAmmunition.Mass, leftLineRenderer);
        trajectoryLine.ShowTrajectoryLine(rightBarrel.muzzle.position, rightBarrel.muzzle.forward * _currentAmmunition.Force / _currentAmmunition.Mass, rightLineRenderer);
    }
    public void Rotate(Vector2 input)
    {
        trajectoryLine.ShowTrajectoryLine(leftBarrel.muzzle.position, leftBarrel.muzzle.forward * _currentAmmunition.Force / _currentAmmunition.Mass, leftLineRenderer);
        trajectoryLine.ShowTrajectoryLine(rightBarrel.muzzle.position, rightBarrel.muzzle.forward * _currentAmmunition.Force / _currentAmmunition.Mass, rightLineRenderer);
        float _xRotation = turret.transform.eulerAngles.x;
        float _yRotation = turret.transform.eulerAngles.y;

        input = input * (rotationSpeed * Time.deltaTime);
        _xRotation = turret.transform.eulerAngles.x + (-input.y);
        _xRotation = (_xRotation > 180) ? _xRotation - 360 : _xRotation;
        _xRotation = Mathf.Clamp(_xRotation, xMinRotation, xMaxRotation);
        _yRotation = turret.transform.eulerAngles.y + input.x;
        _yRotation = (_yRotation > 180) ? _yRotation - 360 : _yRotation;
        _yRotation = Mathf.Clamp(_yRotation, yMinRotation, yMaxRotation);
        turret.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    public void SetTrigger(bool isTriggerOn)
    {
        _isTriggerOn = isTriggerOn;

        if (_isTriggerOn && _fireCycle == null)
        {
            _fireCycle = StartCoroutine(FireCycle());
        }
    }

    private IEnumerator FireCycle()
    {
        do
        {
            leftBarrel.Shoot();
            rightBarrel.Shoot();
            yield return new WaitForSeconds(offSet);
        } 
        while (_isTriggerOn);
        _fireCycle = null;
    }
}
