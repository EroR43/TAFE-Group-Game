using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public MonSave mon;
    public void Start()
    {
        mon = GameObject.FindGameObjectWithTag("MonSave").GetComponent<MonSave>();
    }
    public void LoadSceneAsync(int index)
    {
        if (mon.currentHP == 0 && (mon.statsSaved == true))
        {
            mon.TownHeal();
            SceneManager.LoadSceneAsync(index);
        }
        else
        {
            SceneManager.LoadSceneAsync(index);
        }

    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
