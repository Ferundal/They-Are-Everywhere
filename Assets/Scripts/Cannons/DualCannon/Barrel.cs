using System;
using UnityEngine;

namespace Assets.Scripts.Cannons.DualCannon
{
    [Serializable]
    public class Barrel
    {
        public Transform muzzle;
        [HideInInspector] public AmmunitionPool ammunitionPool;

        [SerializeField] private GameObject smokeEffect;
        [SerializeField] private Animator shutterAnimator;
        [SerializeField] private Animator barrelAnimator;

        private Ammunition ammunition;

        public void Shoot()
        {
            /*            if (muzzle.childCount == 0)
                        {
                            GameObject smoke = Instantiate(smokeEffect, muzzle.transform.position, smokeEffect.gameObject.transform._rotation);
                            smoke.transform.parent = muzzle;
                            smoke.transform.localScale = muzzle.localScale;
                            StartCoroutine(DestroySmoke(smoke));
                        }*/


            ammunition = ammunitionPool.Get();
            ammunition.transform.position = muzzle.position;
            ammunition.gameObject.SetActive(true);
            shutterAnimator.SetTrigger("ShutterMove");
            barrelAnimator.SetTrigger("BarrelMove");
            ammunition.gameObject.transform.rotation = muzzle.rotation;
            ammunition.Fire();
        }

        /*        private IEnumerator DestroySmoke(GameObject smoke)
                {
                    ParticleSystem parts = smoke.GetComponent<ParticleSystem>();
                    float totalDuration = parts.main.duration + parts.main.startLifetime.constant;

                    yield return new WaitForSeconds(totalDuration);
                    Destroy(smoke);
                }*/
    }
}