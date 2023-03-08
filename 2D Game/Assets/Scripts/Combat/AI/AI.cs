using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    private Unit unit;

    public AI(Unit newUnit)
    {
        unit = newUnit;
    }

    public void Activate()
    {/*
        if (Random.value > 0.5f)
        {
            List<Ability> abilityList = new List<Ability>(); //unit.abilities);

            while (abilityList.Count > 0)
            {
                Ability ability = GetRandomAbility(abilityList);

                if (unit.CheckStaminaCost(ability.cost))
                {
                    ability.Activate(unit, TurnBasedCombatSystem.instance.GetRandomPlayerUnit());

                    return;
                }
                else
                {
                    abilityList.Remove(ability);
                }
            }
        }*/

        Unit target = TurnBasedCombatSystem.instance.GetRandomPlayerUnit();

        unit.canAttack = false;
        unit.MoveToPosition(target.GetPosition());

        int damage = unit.Attack();

        target.Damage(damage);

        //CombatLog.instance.CreateLog($"{unit.unitName} attacked {target.unitName} for {damage} damage!", unit.team);

        TurnBasedCombatSystem.instance.EndTurn();
    }

    private Ability GetRandomAbility(List<Ability> abilities)
    {
        return abilities[Random.Range(0, abilities.Count)];
    }
}