using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Unit/UnitData")]
public class UnitData : ScriptableObject
{
    public Team team;

    public string unitName;
    public int maxHealth;

    public Weapon weapon;

    public AnimatorOverrideController animatorController;
}