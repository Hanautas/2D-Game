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
    public TMP_Text goldText;

    [Header("Characters")]
    public PlayableCharacter[] playableCharacters;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetGold(0);
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