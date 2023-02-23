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
    public List<Unit> playerUnitList;
    public List<Unit> enemyUnitList;

    public List<UnitData> enemyUnitData;

    private GameObject unitPrefab;

    [Header("Unit Positions")]
    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    void Awake()
    {
        instance = this;

        unitPrefab = Resources.Load("Units/Unit") as GameObject;
    }

    void Start()
    {
        CreateUnits(PlayerData.instance.GetUnitList(), playerPositions);
        CreateUnits(enemyUnitData, enemyPositions);
    }

    private void CreateUnits(List<UnitData> units, Transform[] positions)
    {
        for (int i = 0; i < units.Count; i++)
        {
            GameObject unitObject = Instantiate(unitPrefab, transform.position, Quaternion.identity) as GameObject;

            unitObject.transform.SetParent(positions[i], false);

            Unit unit = unitObject.transform.GetComponent<Unit>();

            unit.SetUnitData(units[i]);

            //positions[i].Find("Button Canvas/Button").GetComponent<Button>().onClick.AddListener(() => SelectPlayerUnit(unit));
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