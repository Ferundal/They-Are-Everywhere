using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallPool : MonoBehaviour
{
    public static CannonBallPool instance;

    [SerializeField] private int cannonBallAmount;
    [SerializeField] private GameObject cannonBallPrefub;
    private List<GameObject> cannonBalls = new List<GameObject>();


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < cannonBallAmount; i++)
        {
            GameObject ball = Instantiate(cannonBallPrefub);
            ball.gameObject.SetActive(false);
            cannonBalls.Add(ball);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < cannonBalls.Count; i++)
        {
            if (!cannonBalls[i].activeInHierarchy)
            {
                return cannonBalls[i];
            }
        }

        return null;
    }
}
