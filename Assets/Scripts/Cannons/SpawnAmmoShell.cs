using Assets.Scripts.Cannons.DualCannon;
using UnityEngine;

public class SpawnAmmoShell : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private AmmunitionPool ammunitionPool;

    private Rigidbody _spawnRigidbody;
    public void Spawn()
    {
        Ammunition spawnedObject = ammunitionPool.Get();

        if (spawnedObject != null)
        {
            _spawnRigidbody = spawnedObject.GetComponent<Rigidbody>();
            spawnedObject.transform.position = _spawnPosition.position;
            spawnedObject.gameObject.SetActive(true);
/*            if (!_isLeft)
            {
                _spawnRigidbody.AddForce(_spawnPosition.right * 1000f, ForceMode.Impulse);
                _spawnRigidbody.AddForce(_spawnPosition.forward * 1000f, ForceMode.Impulse);
            }
            else
            {
                _spawnRigidbody.AddForce(-_spawnPosition.right * 1000f, ForceMode.Impulse);
                _spawnRigidbody.AddForce(_spawnPosition.forward * 1000f, ForceMode.Impulse);
            }*/
        }
    }
}
