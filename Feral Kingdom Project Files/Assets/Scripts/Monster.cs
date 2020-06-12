using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 06/06/2020

public class Monster : MonoBehaviour
{
    [System.Serializable]
    // MonsterData class to hold all the data of the monster funcs that affect data
    public class MonsterData
    {
        // int for the max HP the monster has, set in unity editor for each monster type
        public int maxHP;
        // Vector2 to hold the min and max Damage the monster can do
        public Vector2 attDmg;
        [SerializeField]
        // int that defines how much health the monster currently has
        private int currHP;

        /// <summary>
        /// Sets the monster health to the max.
        /// </summary>
        public void InitializeMonster()
        {
            currHP = maxHP;
        }

        // returns the value of the monsters current healht when ran
        public int GetCurrHP()
        {
            return currHP;
        }

        /// <summary>
        /// Applies dmg to the currHP, returns true if out of HP.
        /// </summary>
        /// <param name="dmg"></param>
        /// <returns></returns>
        public bool TakeDamage(int dmg)
        {

            currHP -= dmg;
            if (currHP <= 0)
            {
                currHP = 0;
                return true;
            }
            else if (currHP > maxHP)
            {
                currHP = maxHP;
                return false;
            }
            else
            {
                return false;
            }
        }
    }

    // MonsterData class of data to seperate the funcs from the actual data of the monster
    public MonsterData data;


    // Start is called before the first frame update, runs the InitializeMonster func in data class
    void Start()
    {
        data.InitializeMonster();
    }

}
