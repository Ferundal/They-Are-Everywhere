using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Ammunition : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float ammoMass;
    [SerializeField] private float tooFarAwayTime = 3f;
    [SerializeField] private GameObject explosion;
    private Rigidbody _rb;
    private Coroutine _tooFarAwayTimeCoroutine;
    private ObjectPool<Ammunition> _ammunitionPool;

    public float Force { get => force; }
    public float Mass { get => ammoMass; }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
            return;

        GameObject explosionObject = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        ParticleSystem parts = explosionObject.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetime.constant;
        Destroy(explosionObject, totalDuration);
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);

        if (_tooFarAwayTimeCoroutine == null) return;

        StopCoroutine(_tooFarAwayTimeCoroutine);
    }

    public void Fire()
    {
        _rb.AddForce(gameObject.transform.forward * force, ForceMode.Impulse);
        _rb.mass = ammoMass;
        _tooFarAwayTimeCoroutine = StartCoroutine(DeactivateAfterTime(tooFarAwayTime));
    }

    public IEnumerator DeactivateAfterTime(float deactiveTime)
    {
        yield return new WaitForSeconds(deactiveTime);
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

}
