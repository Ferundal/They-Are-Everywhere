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
        PauseManager._instance.OnStateChanged += OnStateChanged;
        spawnCycle = StartCoroutine(Spawn(offSet));
    }

    private IEnumerator Spawn(float offset)
    {
        do
        {
            yield return new WaitForSeconds(offset);
            var enemy = enemyPool.Get();

            if (enemy != null)
            {
                enemy.transform.position = new Vector3(Random.Range(minX, maxX), 1, 0);
                enemy.gameObject.SetActive(true);
            }
        }
        while (!_isFinished);
        spawnCycle = null;
    }

    private void OnStateChanged(bool isPaused)
    {
        if (!isPaused)
            spawnCycle = StartCoroutine(Spawn(offSet));
        else
            StopCoroutine(spawnCycle);
    }
}
