using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayableCharacter
{
    public bool isOwned;

    public UnitData unitData;

    public void SetOwned()
    {
        isOwned = true;
    }

    public void SetWeapon(Weapon newWeapon)
    {
        unitData.weapon = newWeapon;
    }
}