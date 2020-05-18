using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currBattle;

    public Monster[] monsterDefinitions;

    public int lastSceneIndex = 0;

    public static Hashtable battleNodeTable = new Hashtable();

    [SerializeField]
    private Monster playerMon;
    private GameObject playerObj;


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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    public void SetMon(Monster m)
    {
        if (playerMon == null)
        {
            playerMon = m;
            playerMon.data.InitializeMonster();
            playerObj = playerMon.gameObject;
        }
    }

    public Monster GetMonster()
    {
        return playerMon;
    }

    public GameObject GetPlayerObject()
    {
        return playerObj;
    }

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
    /// Returns true if all battles are completed.
    /// </summary>
    /// <returns></returns>
    public static bool QueryCompletion()
    {
        int completed = 0;

        //for (int i = 0; i < battleNodeTable.Count; i++)
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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        ClearData();
    }

    public void ClearData()
    {
        currBattle = 0;
        playerMon = null;
        battleNodeTable.Clear();
    }

    public void PauseMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(0);
        }
    }


    public void Quit()
    {
        Application.Quit();
    }
}
