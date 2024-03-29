using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    [Header("Progress")]
    public bool tutorialComplete;

    [Header("Values")]
    public int totalGold;
    public int currentGold;

    [Header("UI")]
    public Animator playerUIAnimator;
    public TMP_Text goldText;

    [Header("Items")]
    public List<Item> itemList;

    [Header("Characters")]
    public PlayableCharacter[] playableCharacters;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetGold(100);
    }

    void Update()
    {
        if (currentGold != totalGold)
        {
            if (currentGold < totalGold)
            {
                currentGold += 1;

                goldText.text = currentGold.ToString();
            }
            else if (currentGold > totalGold)
            {
                currentGold -= 1;

                goldText.text = currentGold.ToString();
            }
        }
    }

    public void SetTutorialComplete()
    {
        tutorialComplete = true;
    }

    public void SetPlayerUIActive(bool isActive)
    {
        playerUIAnimator.SetBool("IsShowing", isActive);
    }

    public void SetGold(int amount)
    {
        currentGold = totalGold;

        totalGold += amount;
    }

    public bool CheckGold(int amount)
    {
        if (amount <= totalGold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<UnitData> GetUnitList()
    {
        List<UnitData> units = new List<UnitData>();

        foreach (PlayableCharacter character in playableCharacters)
        {
            if (character.isOwned)
            {
                units.Add(character.unitData);
            }
        }

        return units;
    }
}