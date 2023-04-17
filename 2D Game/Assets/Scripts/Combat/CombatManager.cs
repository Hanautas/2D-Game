using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public int currentDifficulty = 1;

    public Tileset currentTilesetType;

    public List<UnitGroup> unitGroupList;

    void Awake()
    {
        instance = this;
    }

    public void SetDifficulty(int value)
    {
        currentDifficulty = value;
    }

    public void SetTilesetType(Tileset tileset)
    {
        currentTilesetType = tileset;
    }

    public Tileset GetTilesetType()
    {
        return currentTilesetType;
    }

    public List<UnitData> GetUnitList()
    {
        int unitCount;

        List<UnitData> unitDataList = new List<UnitData>();

        switch(currentDifficulty) 
        {
        case 1:
            unitCount = Random.Range(1, 3);

            for (int i = 0; i < unitCount; i++)
            {
                unitDataList.Add(GetRandomUnitData(0));
            }

            break;
        case 2:
            unitCount = Random.Range(2, 4);

            unitDataList.Add(GetRandomUnitData(0));

            unitCount--;

            for (int i = 0; i < unitCount; i++)
            {
                unitDataList.Add(GetRandomUnitData(1));
            }

            break;
        case 3:
            unitCount = Random.Range(2, 5);

            unitDataList.Add(GetRandomUnitData(1));
            unitDataList.Add(GetRandomUnitData(1));

            unitCount -= 2;

            if (Random.Range(1, 3) == 2)
            {
                unitDataList.Add(GetRandomUnitData(2));
            }
            else
            {
                unitDataList.Add(GetRandomUnitData(1));
            }

            break;
        case 4:
            unitCount = Random.Range(3, 5);

            unitDataList.Add(GetRandomUnitData(1));
            unitDataList.Add(GetRandomUnitData(1));

            unitDataList.Add(GetRandomUnitData(2));
            unitDataList.Add(GetRandomUnitData(2));
            
            break;
        default:
            return new List<UnitData>();
        }

        return unitDataList;
    }

    private UnitData GetRandomUnitData(int listIndex)
    {
        return unitGroupList[listIndex].unitDataList[Random.Range(0, unitGroupList[0].unitDataList.Count)];
    }
}