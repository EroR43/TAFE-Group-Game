using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 29/04/2020

public class Unit : MonoBehaviour
{
    /// <summary>
    /// Used to set the level range of an enemy unit based upon which area you are in.
    /// </summary>
    public int stageNum;
    /// <summary>
    /// Name of the unit.
    /// </summary>
    public string unitName;
    /// <summary>
    /// Level of the unit, used to calculate the current damage, max health, current armor and current heal amount of the monster.
    /// </summary>
    public int unitLevel;

    /// <summary>
    /// Base health that acts as the multipier to equal to max health that the unit will have depending on their level.
    /// </summary>
    public int baseHealth;
    /// <summary>
    ///  Max Health Points of the unit.
    /// </summary>
    public int maxHP;
    /// <summary>
    /// Current health of the unit that this script is attached to.
    /// </summary>
    public int currentHP;

    /// <summary>
    /// the base armor multipler, determines current armor acccording to level.
    /// </summary>
    public int baseArmor;
    /// <summary>
    /// the current armor value of the unit, decreases the amount of damage done to this unit.
    /// </summary>
    public int currentArmor;

    /// <summary>
    /// Base damage multiplier, determines the current damage according to the units level.
    /// </summary>
    public int baseDamage;
    /// <summary>
    /// Current damage the unit does to the other monster.
    /// </summary>
    public int currentDamage;

    /// <summary>
    /// The amount of exp the unit need to reach the next level.
    /// </summary>
    public int maxEXP;
    /// <summary>
    /// the current amount of exp the unit has.
    /// </summary>
    public int currentEXP;

    /// <summary>
    /// Base heal multiplier that determines the amount the player is healed when they choose the heal action.
    /// </summary>
    public int baseHealAmnt;
    /// <summary>
    /// the amount the player heals by.
    /// </summary>
    public int currentHealAmnt;

    /// <summary>
    /// A unique identifier for the monster.
    /// </summary>
    public int monIndex;

    /// <summary>
    /// Refrence for the LogReader class.
    /// </summary>
    public LogReader reader;
    /// <summary>
    /// Refrence for the MonSave class.
    /// </summary>
    public MonSave mon;

    public void SetUnit(bool isPlayer)
    {
        reader = GameObject.Find("MonSave(Clone)").GetComponent<LogReader>();
        mon = GameObject.Find("MonSave(Clone)").GetComponent<MonSave>();
        // If statement to check if the players monster stats have been saved, running the SetPlay function with the isPlayer param passed to the SetUnit function.
        if (mon.statsSaved == true)
        {
            SetPlay(isPlayer);
        }
        // Runs the SetEnem function with the isPlayer param that is passed to this function.
        SetEnem(isPlayer);
        // Setup stats for the unit, max hp is set to equal the product of the base health int multiplyied by the level of the unit, maxExp is the level time 100, current armor is base times level etc.
        maxHP = baseHealth * unitLevel;
        maxEXP = unitLevel * 100;
        currentArmor = baseArmor * unitLevel;
        currentDamage = baseDamage * unitLevel;
        currentHealAmnt = baseHealAmnt * unitLevel;
        // This statment checks if player stats are not being saved or if the isPlayer param is false, setting the current HP to equal the max HP.
        if (mon.statsSaved != true || isPlayer == false)
        {
            currentHP = maxHP;
        }
    }


    /// <summary>
    /// This function determines how the player takes damage from the enemy, the amount of damage taken by the unit is determined by the dmg param passed to this function,
    /// which is then used in a calculation, a new int of 'd' is set to be the product of dmg minus this units currentArmor divided by 4,
    /// if d is equal to or less than the value of dmg divided by 6, your current HP will have the product of dmg divided by 6, otherwise the unit will take the value of d as damage from their current health.
    /// This function also checks if the unit has died, by checking if the units health is below 0, this function will return as true, other wise it will equal false.
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns></returns>
    public bool TakeDamage(int dmg)
    {
        int d = (dmg - (currentArmor / 4));
        if (d <= (dmg/6))
        {
            currentHP -= (dmg/6);
        }
        else
        {
            currentHP -= d;
        }

        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Heals the player by an amount passed as a param, adding the value of the amount param to the units current HP, setting the current HP to the maxHP if it goes over.
    /// </summary>
    /// <param name="amount"></param>
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    /// <summary>
    /// Increases the units level by one whenever the currentEXP exceeds the maxEXP.
    /// </summary>
    public void LevelUp()
    {
        if (currentEXP >= maxEXP)
        {
            currentEXP = 0;
            unitLevel += 1;
        }
    }

    /// <summary>
    /// Sets the unit as the player unit if the isPLayer bool is true, loading the base stats of the prefab and then the stats saved to a text file if they exist.
    /// </summary>
    /// <param name="isPlayer"></param>
    public void SetPlay(bool isPlayer)
    {
        if (isPlayer == true)
        {
            mon.LoadBaseStats(this);
            mon.LoadStats(this);
        }
    }

    /// <summary>
    /// This sets the unit as an enemy if the isPlayer bool is false, 
    /// getting the stageNum int from the battle system script to define what the randomly generated level should be within the predefined ranges.
    /// </summary>
    /// <param name="isPlayer"></param>
    public void SetEnem(bool isPlayer)
    {
        if (isPlayer == false)
        {
            stageNum = GameObject.Find("Battle System").GetComponent<BattleSystem>().stageNum;
            if (stageNum == 0)
            {
                unitLevel = Random.Range(3, 11);
            }
            else if (stageNum == 1)
            {
                unitLevel = Random.Range(10, 21);
            }
            else if (stageNum == 2)
            {
                unitLevel = Random.Range(21, 51);
            }
        }
    }
}
