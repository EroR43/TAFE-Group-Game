using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMonHUD : MonoBehaviour
{
    public CanvasGroup monSelect;

    public CanvasGroup slimeSelect;
    public CanvasGroup slimeStat;
    public CanvasGroup swordSelect;
    public CanvasGroup swordStat;
    public CanvasGroup wyvernSelect;
    public CanvasGroup wyvernStat;
    public CanvasGroup back;

    public Text slimeName;
    public Text slimeMax;
    public Text slimeCurr;
    public Text swordName;
    public Text swordMax;
    public Text swordCurr;
    public Text wyvernName;
    public Text wyvernMax;
    public Text wyvernCurr;

    public BattleSystem battle;
    public MonSave mon;

    private void Awake()
    {
        CloseMenu();
        mon = GameObject.Find("MonSave(Clone)").GetComponent<MonSave>();
    }


    public void CloseMenu()
    {
        slimeSelect.interactable = false;
        slimeSelect.alpha = 0;
        slimeSelect.blocksRaycasts = false;
        slimeStat.interactable = false;
        slimeStat.alpha = 0;
        slimeStat.blocksRaycasts = false;
        swordSelect.interactable = false;
        swordSelect.alpha = 0;
        swordSelect.blocksRaycasts = false;
        swordStat.interactable = false;
        swordStat.alpha = 0;
        swordStat.blocksRaycasts = false;
        wyvernSelect.interactable = false;
        wyvernSelect.alpha = 0;
        wyvernSelect.blocksRaycasts = false;
        wyvernStat.interactable = false;
        wyvernStat.alpha = 0;
        wyvernStat.blocksRaycasts = false;
        back.interactable = false;
        back.alpha = 0;
        back.blocksRaycasts = false;
    }

    public IEnumerator ShowMonSelect()
    {
        yield return new WaitForSeconds(1);
        monSelect.interactable = true;
        monSelect.alpha = 1;
        monSelect.blocksRaycasts = true;
        back.interactable = true;
        back.alpha = 1;
        back.blocksRaycasts = true;
        mon.LoadParty();
        bool slAl = mon.LoadPartyAlive(1);
        bool swAl = mon.LoadPartyAlive(2);
        bool wyAl = mon.LoadPartyAlive(3);
        Debug.Log("Slime Alive is " + slAl);
        Debug.Log("Sword Alive is " + swAl);
        Debug.Log("Wyvern Alive is " + wyAl);

        if (mon.slimeCaught == true)
        {
            slimeSelect.interactable = true;
            slimeSelect.alpha = 1;
            slimeSelect.blocksRaycasts = true;
            slimeStat.interactable = true;
            slimeStat.alpha = 1;
            slimeStat.blocksRaycasts = true;
            MonStats(1);
        }
        else if (mon.slimeCaught == false || battle.playerUnit.monIndex == 1 || (slAl == false))
        {
            slimeSelect.interactable = false;
            slimeSelect.alpha = 0.5f;
            slimeSelect.blocksRaycasts = false;
            slimeStat.interactable = false;
            slimeStat.alpha = 0;
            slimeStat.blocksRaycasts = false;
        }
        if (mon.swordCaught == true)
        {
            swordSelect.interactable = true;
            swordSelect.alpha = 1;
            swordSelect.blocksRaycasts = true;
            swordStat.interactable = true;
            swordStat.alpha = 1;
            swordStat.blocksRaycasts = true;
            MonStats(2);
        }
        else if (mon.swordCaught == false || battle.playerUnit.monIndex == 2 || (swAl == false))
        {
            swordSelect.interactable = false;
            swordSelect.alpha = 0.5f;
            swordSelect.blocksRaycasts = false;
            swordStat.interactable = false;
            swordStat.alpha = 0;
            swordStat.blocksRaycasts = false;
        }
        if (mon.wyvernCaught == true)
        {
            wyvernSelect.interactable = true;
            wyvernSelect.alpha = 1;
            wyvernSelect.blocksRaycasts = true;
            wyvernStat.interactable = true;
            wyvernStat.alpha = 1;
            wyvernStat.blocksRaycasts = true;
            MonStats(3);
        }
        else if (mon.wyvernCaught == false || battle.playerUnit.monIndex == 3 || (wyAl == false))
        {
            wyvernSelect.interactable = false;
            wyvernSelect.alpha = 0.5f;
            wyvernSelect.blocksRaycasts = false;
            wyvernStat.interactable = false;
            wyvernStat.alpha = 0;
            wyvernStat.blocksRaycasts = false;
        }
        StartCoroutine(mon.LoadLastMon());
        yield return new WaitForSeconds(1);
    }

    public void MonHUD(int monNum)
    {
        mon.LoadMonStats(monNum);
        CloseMenu();
        StartCoroutine(mon.SaveLastMon());
        battle.StartCoroutine(battle.LoadNewPlayer());
    }

    public void MonStats(int monIndex)
    {
        mon.LoadMonStats(monIndex);
        if (monIndex == 1)
        {
            mon.SavePartyAlive(monIndex);
            slimeName.text = mon.unitName + ":";
            slimeMax.text = "Level: " + mon.unitLevel + "\n" + "Max HP: " + mon.maxHP + "\n" + "Max EXP: " + mon.maxEXP + "\n" + "Curr Dmg: " + mon.currentDamage;
            slimeCurr.text = " Curr HP: " + mon.currentHP + "\n" + " Curr EXP: " + mon.currentEXP + "\n" + " Curr Armr: " + mon.currentArmor + "\n" + " Curr Heal: " + mon.currentHealAmnt;
        }
        if (monIndex == 2)
        {
            mon.SavePartyAlive(monIndex);
            swordName.text = mon.unitName + ":";
            swordMax.text = "Level: " + mon.unitLevel + "\n" + "Max HP: " + mon.maxHP + "\n" + "Max EXP: " + mon.maxEXP + "\n" + "Curr Dmg: " + mon.currentDamage;
            swordCurr.text = " Curr HP: " + mon.currentHP + "\n" + " Curr EXP: " + mon.currentEXP + "\n" + " Curr Armr: " + mon.currentArmor + "\n" + " Curr Heal: " + mon.currentHealAmnt;
        }
        if (monIndex == 3)
        {
            mon.SavePartyAlive(monIndex);
            wyvernName.text = mon.unitName + ":";
            wyvernMax.text = "Level: " + mon.unitLevel + "\n" + "Max HP: " + mon.maxHP + "\n" + "Max EXP: " + mon.maxEXP + "\n" + "Curr Dmg: " + mon.currentDamage;
            wyvernCurr.text = " Curr HP: " + mon.currentHP + "\n" + " Curr EXP: " + mon.currentEXP + "\n" + " Curr Armr: " + mon.currentArmor + "\n" + " Curr Heal: " + mon.currentHealAmnt;
        }
    }
}
