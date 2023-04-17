using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : MonoBehaviour
{
    public Team team;

    public string unitName;

    public bool isDead;

    public int maxHealth;
    public int currentHealth;

    [Header("Combat")]
    public bool canAttack = true;
    public Weapon weapon;

    [Header("Movement")]
    public int movementSpeed = 10;
    public UnitState unitState;
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
    public GameObject selectButtonObject;

    [Header("UI")]
    public Transform hitContent;

    private GameObject hitPrefab;

    void Awake()
    {
        hitPrefab = Resources.Load("UI/Combat/Hit") as GameObject;
    }

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (unitState == UnitState.Return)
        {
            if (Vector3.Distance(transform.position, originalPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * movementSpeed);
            }
            else if (unitState != UnitState.Stop)
            {
                unitState = UnitState.Stop;
            }
        }
        else if (unitState == UnitState.Move)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * movementSpeed);
            }
            else if (unitState != UnitState.Stop)
            {
                StartCoroutine(PlayAttack());
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

    private IEnumerator PlayAttack()
    {
        unitState = UnitState.Stop;

        weaponAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        unitState = UnitState.Return;
    }

    public void Damage(int damage)
    {
        int newHealth = currentHealth - damage;

        if (newHealth <= 0)
        {
            isDead = true;

            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }

        SetHealthSlider(currentHealth);

        GameObject hitObject = Instantiate(hitPrefab, transform.position, Quaternion.identity) as GameObject;

        hitObject.transform.SetParent(hitContent, true);

        string damageText = "Missed!";

        if (damage > 0)
        {
            damageText = $"-{damage.ToString()}";
        }

        hitObject.transform.Find("Text").GetComponent<TMP_Text>().text = damageText;
    }

    public bool IsDead()
    {
        if (isDead || currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetHealthSlider(int value)
    {
        healthSlider.value = value;
    }

    public void ActivateSelectButton(bool isActive)
    {
        selectButtonObject.SetActive(isActive);
    }

    public void MoveToPosition(Vector3 position)
    {
        targetPosition = position;

        unitState = UnitState.Move;
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