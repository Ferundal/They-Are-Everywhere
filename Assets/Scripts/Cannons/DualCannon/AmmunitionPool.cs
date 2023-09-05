using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Cannons.DualCannon
{
    public class AmmunitionPool: MonoBehaviour
    {
        public Ammunition ammunitionPrefub;

        [SerializeField] private int ammunitionAmount;
        private List<Ammunition> ammunitionList = new List<Ammunition>();
        private int counter = 0;

        private void Awake()
        {
            for (int i = 0; i < ammunitionAmount; i++)
            {
                var ammunition = Instantiate(ammunitionPrefub);
                ammunition.gameObject.SetActive(false);
                ammunitionList.Add(ammunition);
            }
        }

        public Ammunition Get()
        {
            if (counter >= ammunitionAmount) counter = 0;

            if (!ammunitionList[counter].gameObject.activeInHierarchy)
            {
                counter++;
                return ammunitionList[counter - 1];
            }

            return null;
        }
    }
}