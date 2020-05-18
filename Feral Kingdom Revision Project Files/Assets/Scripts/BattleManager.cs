using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Vector3 spawnOffset;
    public Vector3 spawnRotPlayer;
    public Vector3 spawnRotEnemy;

    public Monster playerMon, enemyMon;
    public GameObject enemyObj;
    public float turnTime;

    public Text turnText;
    public Text playerName;
    public Slider playerHPBar;
    public Text playerHPText;
    public Text enemyName;
    public Slider enemyHPBar;
    public Text enemyHPText;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;


    [SerializeField]
    private float turnTimer = 0;
    [SerializeField]
    private Monster currTurn;
    [SerializeField]
    private float currTurnTime;

    private GameObject playerObj;


    // Start is called before the first frame update
    void Start()
    {
        InitialiseBattle();
    }

    // Update is called once per frame
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
        enemyMon.data.SetCurrToMaxHP();

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

    public void OnRunButton()
    {
        if (currTurn == playerMon)
        {
            GameManager.instance.LoadWorld(false);
        }
    }

}
