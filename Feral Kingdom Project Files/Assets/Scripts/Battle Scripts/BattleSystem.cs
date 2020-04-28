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

    public Unit playerUnit;
    public Unit enemyUnit;

    public Text dialogueText;
    public Text chainText;
    public CanvasGroup buttons;
    public CanvasGroup items;

    public PlayerBattleHUD playerHUD;
    public EnemyBattleHUD enemyHUD;
    public BattleMonHUD monHUD;

    public MonSave monSave;

    public int stageNum;

    public BattleState state;

    private void Awake()
    {
        monSave = GameObject.Find("MonSave(Clone)").GetComponent<MonSave>();
        playerPrefab = monSave.prefab;
        enemyPrefab = monSave.mon.EnemyMon();
    }

    void Start()
    {
        state = BattleState.START;
        CombatButtons(true);
        ItemButtons(false);
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation.transform.position + spawnOffset, playerPrefab.transform.rotation);
        playerGO.transform.eulerAngles = spawnRotPlayer;
        playerGO.transform.SetParent(playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        playerUnit.SetUnit(true);
        

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation.transform.position + spawnOffset, enemyPrefab.transform.rotation);
        enemyGO.transform.eulerAngles = spawnRotEnemy;
        enemyGO.transform.SetParent(enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyUnit.SetUnit(false);

        dialogueText.text = "A feral " + enemyUnit.unitName + " approaches...";

        playerHUD.SetPlayerHUD(playerUnit);
        enemyHUD.SetEnemyHUD(enemyUnit);

        monSave.statsSaved = true;
        StartCoroutine(monSave.SavePartyAlive(playerUnit.monIndex));
        monSave.SaveBattleStats(playerUnit);
        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.currentDamage);
        CombatButtons(false);
        enemyHUD.SetHP(enemyUnit.currentHP);
        enemyHUD.SetHPText(enemyUnit.currentHP, enemyUnit.maxHP);
        dialogueText.text = "Your " + playerUnit.unitName + " hit the feral " + enemyUnit.unitName + "!";

        yield return new WaitForSeconds(2f);

        //Check if the enemy is dead
        if (isDead)
        {
            // End the battle
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            //Enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            
        }
        //Change state based on what happened
    }

    public IEnumerator EnemyTurn()
    {
        dialogueText.text = "The feral " + enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1);

        bool isDead = playerUnit.TakeDamage(enemyUnit.currentDamage);
        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetHPText(playerUnit.currentHP, playerUnit.maxHP);
        monSave.SaveBattleStats(playerUnit);
        monSave.LoadFromTxtFile();
        monSave.LoadParty();
        monSave.SavePartyAlive(1);
        monSave.SavePartyAlive(2);
        monSave.SavePartyAlive(3);
        yield return new WaitForSeconds(1);
        StartCoroutine(monSave.SaveLastMon());
        bool slAl = monSave.LoadPartyAlive(1);
        bool swAl = monSave.LoadPartyAlive(2);
        bool wyAl = monSave.LoadPartyAlive(3);
        if (isDead && ((slAl != true) && (swAl != true) && (wyAl != true)))
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else if (isDead && ((slAl == true) || (swAl == true) || (wyAl == true)))
        {
            StartCoroutine(monHUD.ShowMonSelect());
            monHUD.back.interactable = false;
            monHUD.back.alpha = 0.5f;
            monHUD.back.blocksRaycasts = false;
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        StartCoroutine(monSave.LoadLastMon());
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            AddEXP();
            playerUnit.LevelUp();
            playerHUD.SetPlayerHUD(playerUnit);
            dialogueText.text = "You beat the feral " + enemyUnit.unitName + "!";
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadSceneAsync(0);
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "The feral " + enemyUnit.unitName + " bested you...";
            monSave.lastPos.x = -21.5f;
            monSave.lastPos.z = -8.48f;
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadSceneAsync(0);
        }
    }
    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
        CombatButtons(true);
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(playerUnit.currentHealAmnt);

        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetHPText(playerUnit.currentHP, playerUnit.maxHP);
        dialogueText.text = "Your " + playerUnit.unitName + " is rejuvenated!";

        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
    }

    public IEnumerator CatchEmAll()
    {
        GameObject enemyGO = enemyUnit.gameObject;
        float rnd = Random.value;
        if (playerUnit.monIndex == enemyUnit.monIndex)
        {
            dialogueText.text = "You Can Only Tame One of Each Feral...";
            yield return new WaitForSeconds(3f);
            PlayerTurn();
        }
        else if (monSave.chainCount >= 1)
        {
            monSave.chainCount -= 1;
            if ((enemyUnit.currentHP / enemyUnit.maxHP) <= (rnd - 0.6))
            {
                monSave.SaveEnem(enemyUnit);
                Destroy(enemyGO);
                dialogueText.text = "You Caught the Feral " + enemyUnit.unitName + "!";
                yield return new WaitForSeconds(3f);
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                dialogueText.text = "The Feral " + enemyUnit.unitName + " dodged the Chains!";
                yield return new WaitForSeconds(3f);
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "You Don't Have Any Chains...";
            yield return new WaitForSeconds(3f);
            PlayerTurn();
        }
    }

    IEnumerator PlayerRun()
    {
        dialogueText.text = "You Ran Away";
        monSave.SaveBattleStats(playerUnit);
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

    public IEnumerator LoadNewPlayer()
    {
        Destroy(playerUnit.gameObject);
        playerPrefab = monSave.prefab;
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation.transform.position + spawnOffset, playerPrefab.transform.rotation);
        playerGO.transform.eulerAngles = spawnRotPlayer;
        playerGO.transform.SetParent(playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        playerUnit.SetUnit(true);
        playerHUD.SetPlayerHUD(playerUnit);
        dialogueText.text = "You swapped in " + playerUnit.unitName + "!";
        yield return new WaitForSeconds(3f);
        StartCoroutine(EnemyTurn());
    }

    public void OnPartyButton()
    {
        StartCoroutine(monSave.SaveLastMon());
        CombatButtons(false);
        ItemButtons(false);
        StartCoroutine(monHUD.ShowMonSelect());
    }

    public void OnItemButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        else
        {
            CombatButtons(false);
            ItemButtons(true);
            chainText.text = "Chains Left: " + monSave.chainCount;
        }
    }

    public void OnChainButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(CatchEmAll());
        chainText.text = "Chains Left: " + monSave.chainCount;
        CombatButtons(false);
        ItemButtons(false);
    }

    public void OnBackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        else
        {
            CombatButtons(true);
            ItemButtons(false);
            monHUD.CloseMenu();
        }
    }

    public void AddEXP()
    {
        playerUnit.currentEXP += (enemyUnit.unitLevel * 15);
    }

    public void CombatButtons(bool show)
    {
        if (show == true)
        {
            buttons.interactable = true;
            buttons.alpha = 1;
            buttons.blocksRaycasts = true;
        }
        else if (show == false)
        {
            buttons.interactable = false;
            buttons.alpha = 0;
            buttons.blocksRaycasts = false;
        }
    }
    public void ItemButtons(bool show)
    {
        if (show == true)
        {
            items.interactable = true;
            items.alpha = 1;
            items.blocksRaycasts = true;
        }
        else if (show == false)
        {
            items.interactable = false;
            items.alpha = 0;
            items.blocksRaycasts = false;
        }
    }
}
