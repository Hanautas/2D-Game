using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public string unitName;

    public bool isDead;

    public int maxHealth;
    public int currentHealth;

    public Weapon weapon;

    [Header("Sprite Renderers")]
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer weaponRenderer;

    [Header("Animators")]
    public Animator spriteAnimator;
    public Animator weaponAnimator;

    [Header("Sliders")]
    public Slider healthSlider;

    public void SetUnitData(UnitData unitData)
    {
        if (unitData.faction == Faction.Enemy)
        {
            spriteRenderer.flipX = true;
        }

        unitName = unitData.unitName;
        maxHealth = unitData.maxHealth;
        weapon = unitData.weapon;

        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue;

        spriteAnimator.runtimeAnimatorController = unitData.animatorController;

        weaponRenderer.sprite = weapon.itemSprite;
    }

    public void SetHealth(int amount)
    {
        currentHealth += amount;
    }

    public int Attack()
    {        
        if (!IsDead())
        {
            return weapon.Damage();
        }
        else
        {
            return 0;
        }
    }

    public void Damage(int damage)
    {
        int newHealth = currentHealth - damage;

        if (newHealth <= 0)
        {
            isDead = true;
        }
        else
        {
            currentHealth -= damage;
        }

        SetHealthSlider(currentHealth);
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void SetHealthSlider(int value)
    {
        healthSlider.value = value;
    }
}