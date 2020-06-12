using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Author: Eric Hanks
// Last Edited: 06/06/2020

public class MainMenu : MonoBehaviour
{
    // int with a unique id that is used as a flexible value for the scene index, is changed to load specific scenes.
    public int worldIndex;

    // field with id to utilise the Text component of the controls ui display
    public Text controlText;
    // field with id to utilise the text component of the tutorial text ui
    public Text tutorialText;

    // field to refrence the selectionPanel ui, used to disable and enable it when needed.
    public GameObject selectionPanel;
    // field to refrence the menu ui, used to disable and enable it when needed.
    public GameObject menu;

    // int to record the scene index of the last scene that the user had loaded.
    private int lastSceneIndex;


    /* Sets the displayed text for the controlText and tutorialText, sets the selectionPanel to be disabled and sets lastSceneIndex to the GameManagers int of the same name. */
    private void Awake()
    {
        controlText.text = $"{"Movement: W,A,S,D"}\n{"Interact: E"}\n{"Pause Menu: Escape"}";
        tutorialText.text = $"{"You Have 5 Seconds to Attack"}\n{"Your's and the Enemy's Damage will be a Value between the max Damage and min Damage"}";
        selectionPanel.SetActive(false);
        lastSceneIndex = GameManager.instance.lastSceneIndex;
    }

    // when this funct runs it checks if the selection panel UI is active, if it is inactive it will run the ClearData func in the GameManager, set the selection panel to be active and set the menu ui to be inactive. 
    public void NewGame()
    {
        if (selectionPanel.activeSelf == false)
        {
            GameManager.instance.ClearData();
            selectionPanel.SetActive(true);
            menu.SetActive(false);
        }
    }

    // checks if the escape key is pressed and the lastSceneIndex does not have a value of 0 every frame, running the UnPause func if both are true.
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && lastSceneIndex != 0)
        {
            UnPause();
        }
    }

    // When run, runs the the SetMon function in the GameManager with the parameter of the 0th (1st) item in the monsterDefinitions array and runs the LoadScene func in the SceneManager with the parameter of worldIndex.
    public void SetMonA()
    {
        GameManager.instance.SetMon(GameManager.instance.monsterDefinitions[0]);
        SceneManager.LoadScene(worldIndex);
    }

    // When run, runs the the SetMon function in the GameManager with the parameter of the 1st (2nd) item in the monsterDefinitions array and runs the LoadScene func in the SceneManager with the parameter of worldIndex.
    public void SetMonB()
    {
        GameManager.instance.SetMon(GameManager.instance.monsterDefinitions[1]);
        SceneManager.LoadScene(worldIndex);
    }

    // When run, runs the the SetMon function in the GameManager with the parameter of the 2nd (3rd) item in the monsterDefinitions array and runs the LoadScene func in the SceneManager with the parameter of worldIndex.
    public void SetMonC()
    {
        GameManager.instance.SetMon(GameManager.instance.monsterDefinitions[2]);
        SceneManager.LoadScene(worldIndex);
    }

    // when run, runs the LoadScene func in the SceneManager class with the lastSceneIndex as a parameter, loading the scene with the same scene index value as the lastSceneIndex value.
    public void UnPause()
    {
        SceneManager.LoadScene(lastSceneIndex);
    }
}
