using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public static int gold;

    public PlayableCharacter[] playableCharacters;

    void Awake()
    {
        instance = this;
    }

    public void SetGold(int amount)
    {
        gold += amount;
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