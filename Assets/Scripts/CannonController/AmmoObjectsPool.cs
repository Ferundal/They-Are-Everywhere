using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoObjectsPool : MonoBehaviour
{
    public static AmmoObjectsPool instance;

    [SerializeField] private int ammoAmount;
    [SerializeField] private GameObject ammoPrefub;
    private List<GameObject> ammo = new List<GameObject>();
    private int counter = 0;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < ammoAmount; i++)
        {
            GameObject ammoShell = Instantiate(ammoPrefub);
            ammoShell.gameObject.SetActive(false);
            ammo.Add(ammoShell);
        }
    }

    public GameObject GetPooledObject()
    {
        if (counter >= ammoAmount) counter = 0;

        if (!ammo[counter].activeInHierarchy)
        {
            counter++;
            return ammo[counter - 1];
        }

        return null;
    }
}
