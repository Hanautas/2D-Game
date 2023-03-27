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
    public Unit currentUnit;

    public Ability currentAbility;

    [Header("Unit Lists")]
    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;

    private GameObject unitPrefab;

    [Header("Unit Positions")]
    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    [Header("UI")]
    public GameObject playerActions;
    public GameObject playerAttackContent;
    public GameObject playerItemContent;

    public GameObject playerTurnText;
    public GameObject enemyTurnText;

    public GameObject winScreen;
    public GameObject loseScreen;

    void Awake()
    {
        instance = this;

        unitPrefab = Resources.Load("Units/Unit") as GameObject;
    }

    void Start()
    {
        CreateUnits(PlayerData.instance.GetUnitList(), playerPositions, Team.Player);
        CreateUnits(CombatManager.instance.GetUnitList(), enemyPositions, Team.Enemy);

        PlayerActions(false);

        StartCoroutine(PlayerTurn());
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SelectPlayerUnit(playerUnits[0]);
            PlayerActions(true);
        }
        else if (Input.GetKeyDown("2"))
        {
            SelectPlayerUnit(playerUnits[1]);
            PlayerActions(true);
        }
        else if (Input.GetKeyDown("3"))
        {
            SelectPlayerUnit(playerUnits[2]);
            PlayerActions(true);
        }
        else if (Input.GetKeyDown("4"))
        {
            SelectPlayerUnit(playerUnits[3]);
            PlayerActions(true);
        }
    }

    private void CreateUnits(List<UnitData> units, Transform[] positions, Team team)
    {
        int count = Mathf.Clamp(units.Count, 1, 4);

        for (int i = 0; i < count; i++)
        {
            GameObject unitObject = Instantiate(unitPrefab, transform.position, Quaternion.identity) as GameObject;

            unitObject.transform.SetParent(positions[i], false);

            Unit unit = unitObject.transform.GetComponent<Unit>();

            if (team == Team.Player)
            {
                playerUnits.Add(unit);

                unit.SetUnitData(units[i]);

                unit.selectButton.onClick.AddListener(() => SelectPlayerUnit(unit));
                unit.selectButton.onClick.AddListener(() => AttackMode(true));
                //unit.selectButton.onClick.AddListener(() => PlayerActions(true));
            }
            else if (team == Team.Enemy)
            {
                enemyUnits.Add(unit);

                unit.SetUnitData(units[i]);

                unit.selectButton.onClick.AddListener(() => AttackTarget(unit));
            }
        }
    }

    private IEnumerator GameOver(int isGameOver)
    {
        PlayerActions(false);

        yield return new WaitForSeconds(1f);

        if (isGameOver == 1)
        {
            winScreen.SetActive(true);
        }
        else if (isGameOver == 2)
        {
            loseScreen.SetActive(true);
        }
    }

    private int IsGameOver()
    {
        if (IsUnitListDead(enemyUnits))
        {
            return 1;
        }
        else if (IsUnitListDead(playerUnits))
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    private bool IsUnitListDead(List<Unit> unitList)
    {
        foreach (Unit unit in unitList)
        {
            if (!unit.IsDead())
            {
                return false;
            }
        }

        return true;
    }

    public void UnloadCombat()
    {
        GameManager.instance.UnloadScene("Combat");
    }

    public void SelectPlayerUnit(Unit playerUnit)
    {
        currentUnit = playerUnit;
    }

    private IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(2f);

        turn = Turn.Player;

        playerTurnText.SetActive(true);
        enemyTurnText.SetActive(false);

        foreach (Unit unit in playerUnits)
        {
            if (!unit.IsDead() && unit.canAttack)
            {
                unit.ActivateSelectButton(true);
            }
        }
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(4f);

        turn = Turn.Enemy;

        playerTurnText.SetActive(false);
        enemyTurnText.SetActive(true);

        foreach (Unit unit in enemyUnits)
        {
            if (!unit.IsDead())
            {
                AI ai = new AI(unit);

                ai.Activate();

                yield return new WaitForSeconds(2f);
            }
        }
    }

    public void EndTurn()
    {
        int isGameOver = IsGameOver();

        if (isGameOver != 0)
        {
            StartCoroutine(GameOver(isGameOver));
        }
        else
        {
            //currentUnit.CheckStatusEffects();

            int count = 0;

            if (turn == Turn.Player)
            {
                foreach (Unit unit in playerUnits)
                {
                    if (!unit.canAttack)
                    {
                        count++;
                    }
                }

                if (count == playerUnits.Count)
                {
                    StartCoroutine(EnemyTurn());
                }
            }
            else if (turn == Turn.Enemy)
            {
                foreach (Unit unit in enemyUnits)
                {
                    if (!unit.canAttack)
                    {
                        count++;
                    }
                }

                if (count == enemyUnits.Count)
                {
                    NextRound();
                }
            }
        }
    }

    private void NextRound()
    {
        round++;

        foreach (Unit unit in playerUnits)
        {
            unit.canAttack = true;
        }

        StartCoroutine(PlayerTurn());

        Debug.Log("Round: " + round);
    }

    public Unit GetCurrentUnit()
    {
        return currentUnit;
    }

    public Team GetCurrentUnitTeam()
    {
        return currentUnit.team;
    }

    public Unit GetRandomPlayerUnit()
    {
        List<Unit> aliveUnits = new List<Unit>();

        foreach (Unit unit in playerUnits)
        {
            if (!unit.IsDead())
            {
                aliveUnits.Add(unit);
            }
        }

        return aliveUnits[Random.Range(0, aliveUnits.Count - 1)];
    }
/*
    public Unit GetRandomEnemyUnit()
    {
        List<Unit> aliveUnits = new List<Unit>();

        foreach (Unit unit in enemyUnits)
        {
            if (!unit.IsDead())
            {
                aliveUnits.Add(unit);
            }
        }

        return aliveUnits[Random.Range(0, aliveUnits.Count - 1)];
    }*/

    public void AttackMode(bool isActive)
    {
        if (isActive)
        {
            canAttack = true;

            foreach (Unit unit in enemyUnits)
            {
                if (!unit.IsDead())
                {
                    unit.ActivateSelectButton(true);
                }
            }
        }
        else if (!isActive)
        {
            canAttack = false;

            foreach (Unit unit in enemyUnits)
            {
                if (!unit.IsDead())
                {
                    unit.ActivateSelectButton(false);
                }
            }
        }
    }

    public void AttackTarget(Unit target)
    {
        if (canAttack && currentUnit.canAttack)
        {
            AttackMode(false);

            currentUnit.canAttack = false;
            currentUnit.MoveToPosition(target.GetPosition());

            int damage = currentUnit.Attack();

            target.Damage(damage);

            //CombatLog.instance.CreateLog($"{currentUnit.unitName} attacked {target.unitName} for {damage} damage!", currentUnit.team);

            EndTurn();
        }
        else
        {
            Debug.Log("Can't attack");
        }
    }

    public Ability GetCurrentAbility()
    {
        return currentAbility;
    }

    public void AbilityMode(Ability ability, Team team)
    {
        currentAbility = ability;

        //playerAbilitySelect.SetActive(false);

        if (team == Team.Player)
        {
            foreach (Unit unit in playerUnits)
            {
                //unit.SetTargetButton(true);
            }
        }
        else if (team == Team.Enemy)
        {
            foreach (Unit unit in enemyUnits)
            {
                //unit.SetTargetButton(true);
            }
        }
    }

    public void ResetAbilityMode()
    {
        currentAbility = null;

        foreach (Unit unit in playerUnits)
        {
            //unit.SetTargetButton(false);
        }

        foreach (Unit unit in enemyUnits)
        {
            //unit.SetTargetButton(false);
        }
    }

    public void PlayerActions(bool isActive)
    {
        playerActions.SetActive(isActive);
    }
}