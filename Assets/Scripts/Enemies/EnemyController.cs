using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bloodBurst;
    private float _health;

    private void OnEnable()
    {
        _health = health;
    }

    public void DealDamage()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if ((_health - damage) <= 0) DestroyEnemy();

        else _health -= damage;
    }

    private void DestroyEnemy()
    {
        GameObject bloodBurstObject = Instantiate(bloodBurst, transform.position, transform.rotation) as GameObject;
        Destroy(bloodBurstObject, 2f);
        gameObject.SetActive(false);
    }
}
