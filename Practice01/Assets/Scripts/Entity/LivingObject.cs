using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class LivingObject : IDamageable
{

    public float MaxHealthPoint = 100;
    public float CurrentHealthPoint = 100;



    public void TakeDamage(Damage damage)
    {
        this.CurrentHealthPoint -= damage.DamageValue;
    }
}
