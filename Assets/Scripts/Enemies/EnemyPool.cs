using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField] private int enemyCount;
    private List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = Instantiate(enemyPrefab);
            enemy.gameObject.SetActive(false);
            enemyList.Add(enemy);
        }
    }

    public GameObject Get()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].activeInHierarchy)
            {
                return enemyList[i];
            }
        }

        return null;
    }
}
