using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public void DealDamage();
    public void TakeDamage(float damage);
}
