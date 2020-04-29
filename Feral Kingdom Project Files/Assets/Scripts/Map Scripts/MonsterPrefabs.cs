using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 29/04/2020

public class MonsterPrefabs : MonoBehaviour
{
    // GameObject fields to hold the prefabs for each monster.
    public GameObject slime;
    public GameObject sword;
    public GameObject wyvern;


    /// <summary>
    /// Returns the prefab game object of the monster according to the value of the prefabNum param that the func is passed.
    /// </summary>
    /// <param name="prefabNum"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Randomly selects a prefab to return, allowing randomised enemies.
    /// </summary>
    /// <returns></returns>
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
