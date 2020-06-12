using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Author: Eric Hanks
// Last Edited: 06/06/20

public class GameManager : MonoBehaviour
{
    // static GameManager class field to restrict the number of GameManager scripts that can operate in the scene, making the first instance the only instance
    public static GameManager instance;
    // static int field to hold the current battles scene index number.
    public static int currBattle;

    // Monster class array to hold the Monster classes of choices of monsters that the player can choose from
    public Monster[] monsterDefinitions;

    // int variable to hold the scene index number of the last scene the player had open, used for pausing and unpausing
    public int lastSceneIndex = 0;

    // static Hashtable to hold the completion status of each battle
    public static Hashtable battleNodeTable = new Hashtable();

    [SerializeField]
    // Monster class field to refrence and access the Monster class of the players currently selected monster
    private Monster playerMon;
    // GameObject field to hold the GameObject of the monster that the player has currently selected
    private GameObject playerObj;

    /* Checks if instance is equal to null, that there is no instance of GameManager currently, 
     * sets the instance to be this instance of the class and sets this instance to not be destroyed when loading another scene,
     * if there already an instance that is set, the instance of this class that is not set to instance is destroyed */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Checks every frame for if the player presses the "Escape" key to run PauseMenu when they do
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    /// <summary>
    /// Checks if the playerMon field is equal to null and sets the playerMon to be the Monster class parameter of m that the func is passed,
    /// the InitializeMonster func of the playerMon Monster class is run to get the monster ready for a new game,
    /// sets the playerObj to equal the playerMon's gameObject
    /// </summary>
    /// <param name="m"></param>
    public void SetMon(Monster m)
    {
        if (playerMon == null)
        {
            playerMon = m;
            playerMon.data.InitializeMonster();
            playerObj = playerMon.gameObject;
        }
    }

    // returns the Monster class of playerMon when run
    public Monster GetMonster()
    {
        return playerMon;
    }

    // returns the GameObject of playerObj when run
    public GameObject GetPlayerObject()
    {
        return playerObj;
    }

    /* Loads the overworld and checks the parameter of succes is true, if success is true, 
     * the battleIndex for every BattleNode in the scene checked for the one with the same value as currBattle, running the CompleteBattle func of that node. 
     * After the checks are preformed, the currbattle is set to negative -1 which is an inaccessible scene index number and un assigned battleIndex number */
    public void LoadWorld(bool success)
    {
        SceneManager.LoadScene(1);
        if (success == true)
        {
            foreach (BattleNode node in FindObjectsOfType<BattleNode>())
            {
                if (node.battleIndex == currBattle)
                {
                    node.CompleteBattle();
                }
            }
        }
        currBattle = -1;
    }

    /// <summary>
    /// Returns true if all battles are completed by,
    /// setting a local int of completed to be 0,
    /// then for every BattleNode component in the scene, given a local identifier of node, 
    /// a local bool of c is set to be to equal the bool value of each node according to their battleIndex,
    /// to be used to check the completion status of each node, and if a node is completed the value of the completed int is increased by 1
    /// if the value of the completed int is equal to the number of battle nodes in the battleNodeTable, this func returns true,
    /// otherwise if the value of completed doesn't equal the number of battle nodes the func returns false
    /// </summary>
    /// <returns></returns>
    public static bool QueryCompletion()
    {
        int completed = 0;

        foreach (BattleNode node in FindObjectsOfType<BattleNode>())
        {
            bool c = (bool)battleNodeTable[node.battleIndex];
            if (c == true)
            {
                completed++;
            }
        }
        if (completed == battleNodeTable.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // when run, loads the mainmenu and runs the ClearData func
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        ClearData();
    }

    // when run, sets currBattle to 0, playerMon to null and clears all the items of the battleNodeTable to ready this class for a new game.
    public void ClearData()
    {
        currBattle = 0;
        playerMon = null;
        battleNodeTable.Clear();
    }

    // when run, checks if the current scenes buildIndex is equal to 1, setting lastSceneIndex to this value and loading the main menu scene if it is equal to 1
    public void PauseMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(0);
        }
    }
}
