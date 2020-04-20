using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestBattle : MonoBehaviour
{
    public void LoadForestBattle(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
}
