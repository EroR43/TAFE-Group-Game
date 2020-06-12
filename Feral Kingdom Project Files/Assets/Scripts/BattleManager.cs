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


    /* This update function checks if the currTurn is set to anything, either the player or the enemy,
     * then checking if the value of turnTimer is less than the value of currTurnTime, adding time to the turnTimer if it is.
     * if it isn't, it is checked if it's currently the enemy's turn, 
     * then checking if the value of the random damage between the enemy monster's max damage and min damage applied to the players monster,
     * through the Monster class' TakeDamage func has caused the death of the player monster, printing "Player Dead" in the unity console and running the LoadWorld func of the GameManager with a false parameter,
     * regardless of if the player is dead or not, the displayed text and slider display which displays the player monster's health,
     * is set to the monsters current health using the GetCurrHP func of the Monster class, with the text showing the monsters max health as well, 
     * then running the SwapTurns func at the end of the execution of this block */
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

    /* checks if the currTurn is the players turn at the time that the func is run, if it is, 
     * setting the turn to be the enemy monsters, setting the value of currTurnTime to be a random number 
     * between half of turnTime and the turnTime and setting the turn text to display as the enemy's turn.
     * if it is not the players turn when the func is run, currTurn will be set to the players, 
     * the currTurnTime will be set to the normal turnTime, setting the turnTimer to be 0 and setting the turn text to diplay as the players turn*/
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

    /* This function does the following:
     * Regarding the player, 
     * playerMon is set to refrence the return of the GetMonster func in the GameManager class, 
     * playerObj is set to refrence the returned game object of the GetPlayerObject func in the GameManager class,
     * a local GameObject of playerGO is set to be the Instantiated object of the playerObj with postioning on the playerBattleStation to look visually distinct,
     * the eulerAngles transform of the playerGO is set to be value of spawnRotPlayer,
     * playerGO is set to be a child of the playerBattleStation,
     * Regarding the Enemy
     * a local GameObject of enemyGO is set be the Instantiatied object of the enemyObj with postioning on the enemyBattleStation to look visually distinct,
     * the eulerAngles tranform of the enemyGO is set to be the value of spawnRotEnemy,
     * enemyGO is set to be a child of the enemyBattleStation gameObject,
     * enemyMon is to be the Monster class component attached to the enemyGO,
     * the SetCurrToMaxHP func in the Monster class of the enemyMon is run to make the enemy full health
     * Regarding the UI displays,
     * the various text displays are set to display the monsters information, name and health for both monsters.
     * Then, at the end of the function, the currTurnTime is set to be the normal turnTime, the turn is set to be the players turn and the turnText display is updated to reflect this. */

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

    /* This function is run when the player presses the attack button and checks if it is currently the players turn, 
     * checks if the random damage within the max and min damage of the player that is applied to the enemy monster through the TakeDamage func in the Monster class has killed it, 
     * if it has, it checks if the GameManager class' battleNodeTable hastable contains the key of the current battle,  setting the value of the key to true if it is, 
     * then prints "Enemy Dead" in the unity console and runs the LoadWorld func in the GameManager with a true parameter to define that the battle was won.
     * if the damage wasn't enough to kill the enemy, "Enemy Alive" is printed to the unity console.
     * At the end of whatever path that executed,
     * displayed health of the enemy monster is updated with its new values 
     * and the SwapTurns func is ran, ending the players turn */
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
