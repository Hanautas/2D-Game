using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public Team team;

    public string unitName;

    public bool isDead;

    public int maxHealth;
    public int currentHealth;

    public Weapon weapon;

    [Header("Movement")]
    public bool moveToOriginalPosition;
    public bool moveToTargetPosition;
    public float movementSpeed;
    public Vector3 originalPosition;
    public Vector3 targetPosition;

    [Header("Sprite Renderers")]
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer weaponRenderer;

    [Header("Animators")]
    public Animator spriteAnimator;
    public Animator weaponAnimator;

    [Header("Sliders")]
    public Slider healthSlider;

    [Header("Buttons")]
    public Button selectButton;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (moveToOriginalPosition && !moveToTargetPosition)
        {
            if (Vector3.Distance(transform.position, originalPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * movementSpeed);
            }
        }
        else if (!moveToOriginalPosition && moveToTargetPosition)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * movementSpeed);
            }
            else
            {
                moveToOriginalPosition = true;
                moveToTargetPosition = false;
            }
        }
    }

    public void SetUnitData(UnitData unitData)
    {
        team = unitData.team;

        if (team == Team.Enemy)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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

    public void MoveToPosition(Vector3 position)
    {
        targetPosition = position;

        moveToOriginalPosition = false;
        moveToTargetPosition = true;
    }

    public Vector3 GetPosition()
    {
        Vector3 position = new Vector3();
        
        if (team == Team.Player)
        {
            position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        else
        {
            position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }

        return position;
    }
}