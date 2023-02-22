using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static int gold;

    public PlayableCharacter[] playableCharacters;

    public void SetGold(int amount)
    {
        gold += amount;
    }

    public int GetCharactersOwned()
    {
        int amount = 0;

        foreach (PlayableCharacter character in playableCharacters)
        {
            if (character.isOwned)
            {
                amount++;
            }
        }

        return amount;
    }
}