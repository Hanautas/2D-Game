using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/Weapon")]
public class Weapon : Item
{
    public int maxDamage;
    public int minDamage;
    public int accuracy;

    public int Damage()
    {
        return Utility.GetRandomValue(minDamage, maxDamage);
    }
}