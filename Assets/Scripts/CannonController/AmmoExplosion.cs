using System.Collections;
using UnityEngine;

public class AmmoExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosionObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
            return;

        explosionObject = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        ParticleSystem parts = explosionObject.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetime.constant;
        Destroy(explosionObject, totalDuration);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    public IEnumerator DestroyAmmo(float timeBeforeExplosion)
    {
        yield return new WaitForSeconds(timeBeforeExplosion);

        if (gameObject.activeInHierarchy)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
