using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public CanvasGroup travel;
    public CanvasGroup town;
    public CanvasGroup barn;
    public CanvasGroup forest;
    public CanvasGroup swamp;
    public CanvasGroup mount;

    public CanvasGroup mainMenu;

    public CanvasGroup monSelect;

    public CanvasGroup slimeSelect;
    public CanvasGroup slimeStat;
    public CanvasGroup swordSelect;
    public CanvasGroup swordStat;
    public CanvasGroup wyvernSelect;
    public CanvasGroup wyvernStat;
    public CanvasGroup chains;

    public Text slimeName;
    public Text slimeMax;
    public Text slimeCurr;
    public Text swordName;
    public Text swordMax;
    public Text swordCurr;
    public Text wyvernName;
    public Text wyvernMax;
    public Text wyvernCurr;

    public Text chainText;

    public InputField changeSlime;
    public InputField changeSword;
    public InputField changeWyvern;

    public Animator anim;

    public Player_Map player;

    public MonSave mon;

    private void Awake()
    {
        CloseMenu();
        anim = GetComponentInChildren<Animator>();
        mon = GameObject.Find("MonSave(Clone)").GetComponent<MonSave>();
        if (mon.statsSaved != true)
        {
            ShowMainMenu();
            HideChains();
        }
        else if(mon.statsSaved == true)
        {
            ShowChains();
        }
    }


    public void ShowTown()
    {
        travel.interactable = true;
        travel.alpha = 1;
        travel.blocksRaycasts = true;
        town.interactable = true;
        town.alpha = 1;
        town.blocksRaycasts = true;
        barn.interactable = true;
        barn.alpha = 1;
        barn.blocksRaycasts = true;
    }

    public void ShowForest()
    {
        travel.interactable = true;
        travel.alpha = 1;
        travel.blocksRaycasts = true;
        forest.interactable = true;
        forest.alpha = 1;
        forest.blocksRaycasts = true;
    }

    public void ShowSwamp()
    {
        travel.interactable = true;
        travel.alpha = 1;
        travel.blocksRaycasts = true;
        swamp.interactable = true;
        swamp.alpha = 1;
        swamp.blocksRaycasts = true;
    }

    public void ShowMount()
    {
        travel.interactable = true;
        travel.alpha = 1;
        travel.blocksRaycasts = true;
        mount.interactable = true;
        mount.alpha = 1;
        mount.blocksRaycasts = true;
    }

    public void ShowMonSelect()
    {
        StartCoroutine(mon.SaveLastMon());
        CloseMenu();
        HideChains();
        player.movementEnabled = false;
        monSelect.interactable = true;
        monSelect.alpha = 1;
        monSelect.blocksRaycasts = true;
        if (mon.statsSaved == true)
        {
            mon.LoadParty();
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
            else if (mon.slimeCaught == false)
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
            else if (mon.swordCaught == false)
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
            else if (mon.wyvernCaught == false)
            {
                wyvernSelect.interactable = false;
                wyvernSelect.alpha = 0.5f;
                wyvernSelect.blocksRaycasts = false;
                wyvernStat.interactable = false;
                wyvernStat.alpha = 0;
                wyvernStat.blocksRaycasts = false;
            }
        }
        else if (mon.statsSaved == false)
        {
            slimeSelect.interactable = true;
            slimeSelect.alpha = 1;
            slimeSelect.blocksRaycasts = true;
            slimeStat.interactable = false;
            slimeStat.alpha = 0;
            slimeStat.blocksRaycasts = false;
            slimeName.text = "Slime:";
            swordSelect.interactable = true;
            swordSelect.alpha = 1;
            swordSelect.blocksRaycasts = true;
            swordStat.interactable = false;
            swordStat.alpha = 0;
            swordStat.blocksRaycasts = false;
            swordName.text = "Sword:";
            wyvernSelect.interactable = true;
            wyvernSelect.alpha = 1;
            wyvernSelect.blocksRaycasts = true;
            wyvernStat.interactable = false;
            wyvernStat.alpha = 0;
            wyvernStat.blocksRaycasts = false;
            wyvernName.text = "Wyvern:";
        }
    }

    public void QuitSave()
    {
        mon.StartCoroutine(mon.SaveLastMon());
    }

    public void ShowChains()
    {
        chains.interactable = true;
        chains.alpha = 1;
        chains.blocksRaycasts = true;
        if(mon.statsSaved == true)
        {
            chainText.text = "Chains: " + mon.chainCount;
        }
        else if(mon.statsSaved == false)
        {
            mon.chainCount = 6;
            chainText.text = "Chains: " + mon.chainCount;
        }
    }

    public void HideChains()
    {
        chains.interactable = false;
        chains.alpha = 0;
        chains.blocksRaycasts = false;
    }


    public void CloseMenu()
    {
        mainMenu.interactable = false;
        mainMenu.alpha = 0;
        mainMenu.blocksRaycasts = false;
        travel.interactable = false;
        travel.alpha = 0;
        travel.blocksRaycasts = false;
        town.interactable = false;
        town.alpha = 0;
        town.blocksRaycasts = false;
        forest.interactable = false;
        forest.alpha = 0;
        forest.blocksRaycasts = false;
        swamp.interactable = false;
        swamp.alpha = 0;
        swamp.blocksRaycasts = false;
        mount.interactable = false;
        mount.alpha = 0;
        mount.blocksRaycasts = false;
        monSelect.interactable = false;
        monSelect.alpha = 0;
        monSelect.blocksRaycasts = false;
        barn.interactable = false;
        barn.alpha = 0;
        barn.blocksRaycasts = false;
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
        ShowChains();
        player.movementEnabled = true;
    }

    public void PressTown()
    {
        if (mon.statsSaved == true)
        {
            mon.TownHeal();
        }
        else
        {
            NoGo();
        }
        CloseMenu();
        ShowChains();
    }

    public void PressBarn()
    {
        CloseMenu();
        HideChains();
        if(mon.statsSaved == true)
        {
            ShowMonSelect();
        }
        else
        {
            NoGo();
        }
    }

    public void NewGame()
    {
        HideChains();
        mon.statsSaved = false;
        mon.slimeCaught = false;
        mon.swordCaught = false;
        mon.wyvernCaught = false;
        ShowMonSelect();
    }

    public void MonHUD(int monNum)
    {
        if(mon.statsSaved == false)
        {
            mon.SetNewMon(monNum);
        }
        else if(mon.statsSaved == true)
        {
            mon.LoadMon(monNum);
        }
        CloseMenu();
        ShowChains();
    }

    public void ContinueGame()
    {
        CloseMenu();
        mon.statsSaved = true;
        mon.LoadParty();
        StartCoroutine(mon.LoadLastMon());
        ShowChains();
    }

    public void ShowMainMenu()
    {
        CloseMenu();
        HideChains();
        mainMenu.interactable = true;
        mainMenu.alpha = 1;
        mainMenu.blocksRaycasts = true;
        player.movementEnabled = false;
    }

    public void NoGo()
    {
        anim.SetTrigger("noGo");
    }

    public void QuitGame()
    {
        StartCoroutine(mon.SaveLastMon());
        Application.Quit();
    }

    public void NameChange(int monIndex)
    {
        StartCoroutine(mon.SaveLastMon());
        mon.LoadMon(monIndex);
        if (monIndex == 1)
        {
            mon.unitName = changeSlime.text;
            slimeName.text = mon.unitName + ":";
        }
        else if (monIndex == 2)
        {
            mon.unitName = changeSword.text;
            swordName.text = mon.unitName + ":";
        }
        else if (monIndex == 3)
        {
            mon.unitName = changeWyvern.text;
            wyvernName.text = mon.unitName + ":";
        }
        mon.SaveMapStats();
        StartCoroutine(mon.LoadLastMon());
    }

    public void MonStats(int monIndex)
    {
        StartCoroutine(mon.SaveLastMon());
        mon.LoadMon(monIndex);
        if(monIndex == 1)
        {
            slimeName.text = mon.unitName + ":";
            slimeMax.text = "Level: " + mon.unitLevel + "\n" + "Max HP: " + mon.maxHP + "\n" + "Max EXP: " + mon.maxEXP + "\n" + "Curr Dmg: " + mon.currentDamage;
            slimeCurr.text = " Curr HP: " + mon.currentHP + "\n" + " Curr EXP: " + mon.currentEXP + "\n" + " Curr Armr: " + mon.currentArmor + "\n" + " Curr Heal: " + mon.currentHealAmnt;
        }
        if(monIndex == 2)
        {
            swordName.text = mon.unitName + ":";
            swordMax.text = "Level: " + mon.unitLevel + "\n" + "Max HP: " + mon.maxHP + "\n" + "Max EXP: " + mon.maxEXP + "\n" + "Curr Dmg: " + mon.currentDamage;
            swordCurr.text = " Curr HP: " + mon.currentHP + "\n" + " Curr EXP: " + mon.currentEXP + "\n" + " Curr Armr: " + mon.currentArmor + "\n" + " Curr Heal: " + mon.currentHealAmnt;
        }
        if(monIndex == 3)
        {
            wyvernName.text = mon.unitName + ":";
            wyvernMax.text = "Level: " + mon.unitLevel + "\n" + "Max HP: " + mon.maxHP + "\n" + "Max EXP: " + mon.maxEXP + "\n" + "Curr Dmg: " + mon.currentDamage;
            wyvernCurr.text = " Curr HP: " + mon.currentHP + "\n" + " Curr EXP: " + mon.currentEXP + "\n" + " Curr Armr: " + mon.currentArmor + "\n" + " Curr Heal: " + mon.currentHealAmnt;
        }
        StartCoroutine(mon.LoadLastMon());
    }
}
