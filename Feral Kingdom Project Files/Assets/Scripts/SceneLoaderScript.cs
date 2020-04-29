using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Author: Eric Hanks
// Last Edited: 29/04/2020
public class SceneLoaderScript : MonoBehaviour
{
    /// <summary>
    ///  refrence for the MonSave script with a identifiable name.
    /// </summary>
    public MonSave mon;

    public void Start()
    {
        // Finds the GO in the scene with the "MonSave" and gets the MonSave component off of it for use as the mon refrence.
        mon = GameObject.FindGameObjectWithTag("MonSave").GetComponent<MonSave>();
    }

    /// <summary>
    /// Function to load the scene with the same int value that the function is passed as a parameter if certain conditions are met.
    /// </summary>
    /// <param name="index"></param>
    public void LoadSceneAsync(int index)
    {
        // Checks if your current monster has any health and whether you stats have been saved.
        if (mon.currentHP == 0 && (mon.statsSaved == true))
        {
            // Runs the TownHeal function to heal a monster from 0 health to full before entering the battle.
            mon.TownHeal();
            // Loads the scene with the same index value that this function is passed as a parameter.
            SceneManager.LoadSceneAsync(index);
        }
        else
        {
            // Loads the scene without healing the players monster.
            SceneManager.LoadSceneAsync(index);
        }

    }

    /// <summary>
    /// Function to load the scene with the same int value that the function is passed as a parameter.
    /// </summary>
    /// <param name="index"></param>
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
