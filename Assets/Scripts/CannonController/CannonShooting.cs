using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    private CannonController _cannonController;
    private bool _isShooting;

    [SerializeField] private Transform leftMuzzle;
    [SerializeField] private Transform rightMuzzle;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float force;

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

    private void Update()
    {
        if (_isShooting)
        {
            GameObject ball1 = Instantiate(cannonBall, leftMuzzle.position, leftMuzzle.rotation);
            GameObject ball2 = Instantiate(cannonBall, rightMuzzle.position, rightMuzzle.rotation);
            ball1.GetComponent<Rigidbody>().velocity = leftMuzzle.forward * force * Time.deltaTime;
            ball2.GetComponent<Rigidbody>().velocity = rightMuzzle.forward * force * Time.deltaTime;
        }
    }
}
