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

    public Ability currentAbility;

    [Header("Unit Lists")]
    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;

    private List<UnitData> playerUnitData;
    public List<UnitData> enemyUnitData;

    private GameObject unitPrefab;

    [Header("Unit Positions")]
    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    [Header("UI")]
    public GameObject playerActions;
    public GameObject playerAttackContent;
    public GameObject playerItemContent;
    public GameObject cancelButton;
    public List<GameObject> playerMenus;

    public GameObject winScreen;
    public GameObject loseScreen;

    void Awake()
    {
        instance = this;

        unitPrefab = Resources.Load("Units/Unit") as GameObject;

        playerUnitData = PlayerData.instance.GetUnitList();
    }

    void Start()
    {
        CreateUnits(playerUnitData, playerPositions, Team.Player);
        CreateUnits(enemyUnitData, enemyPositions, Team.Enemy);

        PlayerTurn();
    }

    private void CreateUnits(List<UnitData> units, Transform[] positions, Team team)
    {
        int count = units.Count;

        if (count > 4)
        {
            count = 4;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject unitObject = Instantiate(unitPrefab, transform.position, Quaternion.identity) as GameObject;

            unitObject.transform.SetParent(positions[i], false);

            Unit unit = unitObject.transform.GetComponent<Unit>();

            if (team == Team.Player)
            {
                playerUnits.Add(unit);
            }
            else
            {
                enemyUnits.Add(unit);
            }

            unit.SetUnitData(units[i]);

            if (team == Team.Player)
            {
                unit.selectButton.onClick.AddListener(() => SelectPlayerUnit(unit));
            }
            else
            {
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

    private void SelectNextActiveUnit()
    {
        currentUnitIndex++;

        if (turn == Turn.Player)
        {
            if (currentUnitIndex < playerUnits.Count)
            {
                for (int i = currentUnitIndex; i < playerUnits.Count; i++)
                {
                    if (!playerUnits[i].IsDead())
                    {
                        currentUnitIndex = i;

                        currentUnit = playerUnits[i];

                        return;
                    }
                }
            }
            else
            {
                currentUnitIndex = 0;

                turn = Turn.Enemy;
            }
        }
        
        if (turn == Turn.Enemy)
        {
            if (currentUnitIndex < enemyUnits.Count)
            {
                for (int i = currentUnitIndex; i < enemyUnits.Count; i++)
                {
                    if (!enemyUnits[i].IsDead())
                    {
                        currentUnitIndex = i;

                        currentUnit = enemyUnits[i];

                        return;
                    }
                }
            }
            else
            {
                currentUnitIndex = -1;

                turn = Turn.Player;

                NextRound();

                SelectNextActiveUnit();
            }
        }
    }

    private void PlayerTurn()
    {
        PlayerActions(true);
    }

    private void EnemyTurn()
    {
        PlayerActions(false);

        AI ai = new AI(currentUnit);

        ai.Activate();
    }

    public void EndTurn()
    {
        canAttack = false;

        PlayerActions(false);

        StartCoroutine(WaitEndTurn(1f));
    }

    private IEnumerator WaitEndTurn(float delay)
    {
        yield return new WaitForSeconds(delay);

        int isGameOver = IsGameOver();

        if (isGameOver != 0)
        {
            StartCoroutine(GameOver(isGameOver));
        }
        else
        {
            PlayerActions(false);

            SelectNextActiveUnit();

            Debug.Log("Current Unit: " + currentUnitIndex);

            //currentUnit.CheckStatusEffects();

            if (!currentUnit.IsDead())
            {
                if (turn == Turn.Player)
                {
                    PlayerTurn();
                }
                else if (turn == Turn.Enemy)
                {
                    EnemyTurn();
                }
            }
            else
            {
                EndTurn();
            }
        }
    }

    private void NextRound()
    {
        round++;

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
    }

    public void AttackMode(bool isActive)
    {
        if (isActive)
        {
            canAttack = true;

            foreach (Unit unit in enemyUnits)
            {
                if (!unit.IsDead())
                {
                    //unit.SetTargetButton(true);
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
                    //unit.SetTargetButton(false);
                }
            }
        }
    }

    public void AttackTarget(Unit target)
    {
        if (canAttack)
        {
            AttackMode(false);

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
        cancelButton.SetActive(true);

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
        if (isActive)
        {
            playerActions.SetActive(true);
            //playerActionSelect.SetActive(true);

            foreach (GameObject obj in playerMenus)
            {
                obj.SetActive(false);
            }
        }
        else if (!isActive)
        {
            playerActions.SetActive(false);
        }
    }
}