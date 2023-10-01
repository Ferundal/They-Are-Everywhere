using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField] private int enemyCount;
    private List<GameObject> enemyList = new List<GameObject>();
    private int counter = 0;

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
        if (counter >= enemyCount) counter = 0;

        if (!enemyList[counter].gameObject.activeInHierarchy)
        {
            counter++;
            return enemyList[counter - 1];
        }

        return null;
    }
}
