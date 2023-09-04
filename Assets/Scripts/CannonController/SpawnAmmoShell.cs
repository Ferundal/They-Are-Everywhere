using UnityEngine;

public class SpawnAmmoShell : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    private Rigidbody _spawnRigidbody;
    [SerializeField] private bool _isLeft = false;
    public void Spawn()
    {
        GameObject spawnedObject = AmmoObjectsPool.instance.GetPooledObject();

        if (spawnedObject != null)
        {
            _spawnRigidbody = spawnedObject.GetComponent<Rigidbody>();
            spawnedObject.transform.position = _spawnPosition.position;
            spawnedObject.gameObject.SetActive(true);
            spawnedObject.GetComponent<MeshRenderer>().enabled = true;
            _spawnRigidbody.mass = Cannon.instance.AmmoMass;
            if (!_isLeft)
            {
                _spawnRigidbody.AddForce(_spawnPosition.right * Cannon.instance.Force, ForceMode.Impulse);
                _spawnRigidbody.AddForce(_spawnPosition.forward * Cannon.instance.Force, ForceMode.Impulse);
            }
            else
            {
                _spawnRigidbody.AddForce(-_spawnPosition.right * Cannon.instance.Force, ForceMode.Impulse);
                _spawnRigidbody.AddForce(_spawnPosition.forward * Cannon.instance.Force, ForceMode.Impulse);
            }

            StartCoroutine(spawnedObject.GetComponent<AmmoExplosion>().DestroyAmmo(3f));
        }
    }
}
