using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [System.Serializable]
    public class MonsterData
    {
        public int maxHP;
        public Vector2 attDmg;
        [SerializeField]
        private int currHP;

        /// <summary>
        /// Sets the monster health.
        /// </summary>
        public void InitializeMonster()
        {
            currHP = maxHP;
        }

        public int GetCurrHP()
        {
            return currHP;
        }

        public void SetCurrToMaxHP()
        {
            currHP = maxHP;
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

    public MonsterData data;


    // Start is called before the first frame update
    void Start()
    {
        data.InitializeMonster();
    }

}
