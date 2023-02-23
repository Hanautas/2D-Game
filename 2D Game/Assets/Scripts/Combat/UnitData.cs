using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Player,
    Enemy
}

[Serializable]
public class UnitData
{
    public Faction faction;

    public string unitName;
    public int maxHealth;

    public Weapon weapon;

    public AnimatorOverrideController animatorController;
}