using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int stageNum;
    public string unitName;
    public int unitLevel;

    public int baseHealth;
    public int maxHP;
    public int currentHP;

    public int baseArmor;
    public int currentArmor;

    public int baseDamage;
    public int currentDamage;

    public int maxEXP;
    public int currentEXP;

    public int baseHealAmnt;
    public int currentHealAmnt;

    public int monIndex;

    public LogReader reader;
    public MonSave mon;

    public void SetUnit(bool isPlayer)
    {
        reader = GameObject.Find("MonSave(Clone)").GetComponent<LogReader>();
        mon = GameObject.Find("MonSave(Clone)").GetComponent<MonSave>();
        if (mon.statsSaved == true)
        {
            SetPlay(isPlayer);
        }
        SetEnem(isPlayer);
        maxHP = baseHealth * unitLevel;
        maxEXP = unitLevel * 100;
        currentArmor = baseArmor * unitLevel;
        currentDamage = baseDamage * unitLevel;
        currentHealAmnt = baseHealAmnt * unitLevel;
        if (mon.statsSaved != true || isPlayer == false)
        {
            currentHP = maxHP;
        }
    }

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

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void LevelUp()
    {
        if (currentEXP >= maxEXP)
        {
            currentEXP = 0;
            unitLevel += 1;
        }
    }

    public void SetPlay(bool isPlayer)
    {
        if (isPlayer == true)
        {
            mon.LoadBaseStats(this);
            mon.LoadStats(this);
        }
    }

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
