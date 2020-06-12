using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Author: Eric Hanks
// Last Edited: 06/06/2020
public class BattleManager : MonoBehaviour
{
    // Vector3 to define how much the spawned monster should be offset from the spawn location
    public Vector3 spawnOffset;
    // Vector3 to define what the rotation of the players spawned monster should be from the spawn location
    public Vector3 spawnRotPlayer;
    // Vector3 to define what the rotation of the enemys spawned monster should be from the spawn location
    public Vector3 spawnRotEnemy;

    // field to refrence and access the Monster classes attached to the player's and enemy's monster
    public Monster playerMon, enemyMon;
    // GameObject field for the game object of the enemy's monster, used to rotate and move the enemy's monster to a suitable position
    public GameObject enemyObj;
    // float for the max amount of time the player has to make an action before the sides switch.
    public float turnTime;

    // Text field to utilise the Text component of the UI that displays which monsters turn it is.
    public Text turnText;
    // Text field to utilise the Text component of the UI that displays the name of the players monster, used to change the text to fit each monster type.
    public Text playerName;
    // Slider field to utilise the Slider component of the UI element that graphically displays the player monster's health.
    public Slider playerHPBar;
    // Text field to utilise the Text component of the UI that displays health of the players monster.
    public Text playerHPText;
    // Text field to utilise the Text component of the UI that displays the name of the enemy's monster, used to change the text to fit each monster type.
    public Text enemyName;
    // Slider field to utilise the Slider component of the UI element that graphically displays the enemy's monster's health.
    public Slider enemyHPBar;
    // Text field to utilise the Text component of the UI that displays health of the enemy's monster.
    public Text enemyHPText;

    // Field to hold the transform of the players battle station game object, which acts as the spawn location
    public Transform playerBattleStation;
    // Field to hold the transform of the enemy's battle station game object, which acts as the spawn location
    public Transform enemyBattleStation;


    // float that is used to count up towards the turnTime float, when reaching it, swapping the turns.
    [SerializeField]
    private float turnTimer = 0;
    // Monster field to define if it is the players turn or the enemy's and restricts the actions of each to within those turns
    [SerializeField]
    private Monster currTurn;
    // used as a flexible variable to swap between the players defined turn time and the enemy's.
    [SerializeField]
    private float currTurnTime;

    // GameObject field to refrence the game object of the player's monster
    private GameObject playerObj;


    // Start is called before the first frame update, runs the InitialiseBattle func
    void Start()
    {
        InitialiseBattle();
    }


    /* Runs the timer for swapping turns and runs the functionality for the enemy to attack and kill the player.
     * */
    void Update()
    {
        if (currTurn != null)
        {
            if (turnTimer < currTurnTime)
            {
                turnTimer += Time.deltaTime;
            }
            else
            {
                if (currTurn == enemyMon)
                {   
                    if (playerMon.data.TakeDamage(Mathf.FloorToInt(Random.Range(enemyMon.data.attDmg.x, enemyMon.data.attDmg.y) + 3)) == true)
                    {
                        Debug.Log("Player Dead");
                        GameManager.instance.LoadWorld(false);
                    }
                }
                playerHPText.text = $"{playerMon.data.GetCurrHP()} : {playerMon.data.maxHP}";
                playerHPBar.value = playerMon.data.GetCurrHP();
                SwapTurns();
            }
        }
    }

    /* Swaps the current turn between the player and the enemy, with a random turn time for the enemy */
    void SwapTurns()
    {
        if (currTurn == playerMon)
        {
            currTurn = enemyMon;
            currTurnTime = Random.Range(turnTime / 2, turnTime);
            turnTimer = 0;
            turnText.text = "Enemy Turn";
        }

        else
        {
            currTurn = playerMon;
            currTurnTime = turnTime;
            turnTimer = 0;
            turnText.text = "Player Turn";
        }
    }

    /* Sets up the monsters and their displayed stats for the battle, 
     * spawns in the monsters models and updates their health and name diplays with the correct info*/

    public void InitialiseBattle()
    {
        playerMon = GameManager.instance.GetMonster();
        playerObj = GameManager.instance.GetPlayerObject();
        GameObject playerGO = Instantiate(playerObj, playerBattleStation.transform.position + spawnOffset, playerObj.transform.rotation);
        playerGO.transform.eulerAngles = spawnRotPlayer;
        playerGO.transform.SetParent(playerBattleStation);

        GameObject enemyGO = Instantiate(enemyObj, enemyBattleStation.transform.position + spawnOffset, enemyObj.transform.rotation);
        enemyGO.transform.eulerAngles = spawnRotEnemy;
        enemyGO.transform.SetParent(enemyBattleStation);
        enemyMon = enemyGO.GetComponent<Monster>();
        enemyMon.data.InitializeMonster();


        enemyName.text = enemyObj.name;
        enemyHPText.text = $"{enemyMon.data.GetCurrHP()} : {enemyMon.data.maxHP}";
        enemyHPBar.maxValue = enemyMon.data.maxHP;
        enemyHPBar.value = enemyHPBar.maxValue;
        playerName.text = playerObj.name;
        playerHPText.text = $"{playerMon.data.GetCurrHP()} : {playerMon.data.maxHP}";
        playerHPBar.maxValue = playerMon.data.maxHP;
        playerHPBar.value = playerMon.data.GetCurrHP();
        currTurnTime = turnTime;
        currTurn = playerMon;
        turnText.text = "Player Turn";
    }

    /* Only runs on the players turn, implements the functions for the player to attack and kill the enemy, 
     * update their displayed health and load the overworld when the battle is won */
    public void OnAttackButton()
    {
        if (currTurn == playerMon)
        {
            if (enemyMon.data.TakeDamage(Mathf.FloorToInt(Random.Range(playerMon.data.attDmg.x, playerMon.data.attDmg.y))) == true)
            {
                if (GameManager.battleNodeTable.ContainsKey(GameManager.currBattle) == true)
                {
                    GameManager.battleNodeTable[GameManager.currBattle] = true;
                }
                Debug.Log("Enemy Dead");
                GameManager.instance.LoadWorld(true);
            }
            else
            {
                Debug.Log("Enemy Alive");
            }
            enemyHPText.text = $"{enemyMon.data.GetCurrHP()} : {enemyMon.data.maxHP}";
            enemyHPBar.value = enemyMon.data.GetCurrHP();
            SwapTurns();

        }
    }

    // checks if it is currently the players turn, running the LoadWorld func in the GameManager with false parameter to denote the battle was not won.
    public void OnRunButton()
    {
        if (currTurn == playerMon)
        {
            GameManager.instance.LoadWorld(false);
        }
    }

}
