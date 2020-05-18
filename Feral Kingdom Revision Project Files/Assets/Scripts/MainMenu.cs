using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int worldIndex;

    public Text controlText;
    public Text tutorialText;

    public GameObject selectionPanel;
    public GameObject menu;

    private int lastSceneIndex;

    private void Awake()
    {
        controlText.text = $"{"Movement: W,A,S,D"}\n{"Interact: E"}\n{"Pause Menu: Escape"}";
        tutorialText.text = $"{"You Have 5 Seconds to Attack"}\n{"Your's and the Enemy's Damage will be a Value between the max Damage and min Damage"}";
        selectionPanel.SetActive(false);
        lastSceneIndex = GameManager.instance.lastSceneIndex;
    }

    public void NewGame()
    {
        if (selectionPanel.activeSelf == false)
        {
            GameManager.instance.ClearData();
            selectionPanel.SetActive(true);
            menu.SetActive(false);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && lastSceneIndex != 0)
        {
            UnPause();
        }
    }

    public void SetMonA()
    {
        GameManager.instance.SetMon(GameManager.instance.monsterDefinitions[0]);
        SceneManager.LoadScene(worldIndex);
    }

    public void SetMonB()
    {
        GameManager.instance.SetMon(GameManager.instance.monsterDefinitions[1]);
        SceneManager.LoadScene(worldIndex);
    }

    public void SetMonC()
    {
        GameManager.instance.SetMon(GameManager.instance.monsterDefinitions[2]);
        SceneManager.LoadScene(worldIndex);
    }

    public void UnPause()
    {
        SceneManager.LoadScene(lastSceneIndex);
    }
}
