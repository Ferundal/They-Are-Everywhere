using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
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
        Debug.Log(_health);
    }

    private void DestroyEnemy()
    {
        gameObject.SetActive(false);
    }
}
