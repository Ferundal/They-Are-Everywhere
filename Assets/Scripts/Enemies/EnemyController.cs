using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(BoxCollider))]
public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bloodBurst;
    private Transform playerPosition;
    private float _health;

    private void Awake()
    {
        PauseManager._instance.OnStateChanged += OnStateChanged;
    }

    private void OnEnable()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
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
        transform.gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
    }

    private void OnStateChanged(bool isPaused)
    {
        enabled = !isPaused;
    }
}
