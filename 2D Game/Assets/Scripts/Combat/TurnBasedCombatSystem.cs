using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Turn
{
    Player,
    Enemy
}

public class TurnBasedCombatSystem : MonoBehaviour
{
    public static TurnBasedCombatSystem instance;

    [Header("Combat System")]
    public int round;
    public Turn turn;

    [Header("Current Unit")]
    public bool canAttack;
    public int currentUnitIndex;
    public Unit currentUnit;
    //public Ability currentAbility;

    [Header("Unit Lists")]
    public GameObject[] playerUnitList;
    public GameObject[] enemyUnitList;

    [Header("Unit Positions")]
    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CreateUnits(playerUnitList, playerPositions, 4);
        //CreateUnits(enemyUnitList, enemyPositions, 4);
    }

    private void CreateUnits(GameObject[] units, Transform[] positions, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject unitObject = Instantiate(units[Utility.GetRandomValue(0, enemyUnitList.Length)], transform.position, Quaternion.identity) as GameObject;

            unitObject.transform.SetParent(positions[i], false);

            Unit unit = units[i].transform.GetComponent<Unit>();

            positions[i].Find("Button Canvas/Button").GetComponent<Button>().onClick.AddListener(() => SelectPlayerUnit(unit));
        }
    }

    public void SelectPlayerUnit(Unit target)
    {
        currentUnit = target;
    }

    public void AttackTarget(Unit Target)
    {

    }
}