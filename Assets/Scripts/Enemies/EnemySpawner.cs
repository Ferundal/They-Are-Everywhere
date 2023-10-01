using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float offSet;
    private bool _isFinished = false;
    private Coroutine spawnCycle;


    private void Awake()
    {
        spawnCycle = StartCoroutine(Spawn(offSet));
    }

    private IEnumerator Spawn(float offset)
    {
        do
        {
            var enemy = enemyPool.Get();

            if (enemy != null)
            {
                enemy.transform.position = new Vector3(Random.Range(minX, maxX), 1, 0);
                enemy.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(offset);
        }
        while (!_isFinished);
        spawnCycle = null;
    }
}
