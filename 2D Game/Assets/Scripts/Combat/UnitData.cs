using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitData
{
    public Team team;

    public string unitName;
    public int maxHealth;

    public Weapon weapon;

    public AnimatorOverrideController animatorController;
}