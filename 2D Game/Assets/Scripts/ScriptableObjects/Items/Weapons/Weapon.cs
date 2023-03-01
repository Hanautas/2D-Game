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
        int result = Utility.GetRandomValue(0, 100);

        if (accuracy > result)
        {
            Debug.Log("Hit!");

            return Utility.GetRandomValue(minDamage, maxDamage);
        }
        else
        {
            Debug.Log("Missed!");

            return 0;
        }
    }
}