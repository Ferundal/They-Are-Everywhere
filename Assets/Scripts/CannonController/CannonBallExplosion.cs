using System.Collections;
using UnityEngine;

public class CannonBallExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject explosionObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
            return;

        explosionObject = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        Destroy(explosionObject, 0.5f);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(DestroyAfterCollision(0.5f));
    }

    public IEnumerator DestroyAfterCollision(float timeBeforeExplosion)
    {
        yield return new WaitForSeconds(timeBeforeExplosion);

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    public IEnumerator DestroyBall(float timeBeforeExplosion)
    {
        yield return new WaitForSeconds(timeBeforeExplosion);

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
