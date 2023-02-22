using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayableCharacter
{
    public bool isOwned;

    public string name;

    public int maxHealth;
    public int currentHealth;

    public Item weapon;

    public GameObject characterPrefab;

    public void SetOwned()
    {
        isOwned = true;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void SetWeapon(Item newWeapon)
    {
        weapon = newWeapon;
    }
}