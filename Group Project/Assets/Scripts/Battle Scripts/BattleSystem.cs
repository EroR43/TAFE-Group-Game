using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Author: Eric Hanks. Last Updated: 28/02/2020
//Made following Brackeys youtube tutorial for turn based combat:https://www.youtube.com/watch?v=_1pz_ohupPs&t=455s
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, RUN }

public class BattleSystem : MonoBehaviour
{
    public Vector3 spawnOffset;
    public Vector3 spawnRotPlayer;
    public Vector3 spawnRotEnemy;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    PlayerUnit playerUnit;
    EnemyUnit enemyUnit;

    public Text dialogueText;

    public PlayerBattleHUD playerHUD;
    public EnemyBattleHUD enemyHUD;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation.transform.position + spawnOffset, playerPrefab.transform.rotation);
        playerGO.transform.eulerAngles = spawnRotPlayer;
        playerGO.transform.SetParent(playerBattleStation);
        playerUnit = playerGO.GetComponent<PlayerUnit>();


        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation.transform.position + spawnOffset, playerPrefab.transform.rotation);
        enemyGO.transform.eulerAngles = spawnRotEnemy;
        enemyGO.transform.SetParent(enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<EnemyUnit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetPlayerHUD(playerUnit);
        enemyHUD.SetEnemyHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        enemyHUD.SetHPText(enemyUnit.currentHP, enemyUnit.maxHP);
        dialogueText.text = "Your " + playerUnit.unitName + " hit the wild " + enemyUnit.unitName + "!";

        yield return new WaitForSeconds(2f);

        //Check if the enemy is dead
        if (isDead)
        {
            // End the battle
            state = BattleState.WON;
            EndBattle();
            yield return new WaitForSeconds(5f);
            StartCoroutine(SetupBattle());
        }
        else
        {
            //Enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            
        }
        //Change state based on what happened
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "The wild " + enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetHPText(playerUnit.currentHP, playerUnit.maxHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            AddEXP();
            playerUnit.LevelUp();
            playerHUD.SetPlayerHUD(playerUnit);
            dialogueText.text = "You beat the wild " + enemyUnit.unitName + "!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "The wild " + enemyUnit.unitName + " bested you...";
            SceneManager.LoadSceneAsync(0);
        }
    }
    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "Your " + playerUnit.unitName + " is rejuvenated!";

        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerRun()
    {
        dialogueText.text = "You Ran Away";
        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    public void OnRunButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerRun());
    }

    public void AddEXP()
    {
        playerUnit.currentEXP += (enemyUnit.unitLevel * 5);
    }
}
