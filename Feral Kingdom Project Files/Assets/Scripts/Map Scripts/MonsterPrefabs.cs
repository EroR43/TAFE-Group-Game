using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPrefabs : MonoBehaviour
{
    public GameObject slime;
    public GameObject sword;
    public GameObject wyvern;

    public GameObject PlayerMon(int prefabNum)
    {
        if (prefabNum == 1)
        {
            return slime;
        }
        else if (prefabNum == 2)
        {
            return sword;
        }
        else if (prefabNum == 3)
        {
            return wyvern;
        }
        else
        {
            return null;
        }
    }

    public GameObject EnemyMon()
    {
        int e = Random.Range(0, 3);
        if (e == 0)
        {
            return slime;
        }
        else if (e == 1)
        {
            return sword;
        }
        else if (e == 2)
        {
            return wyvern;
        }
        else
        {
            return null;
        }
    }
}
