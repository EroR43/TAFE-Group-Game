using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author: Eric Hanks
// Last Edited: 29/04/2020

public class HUD : MonoBehaviour
{
    [Header("Button Canvas Groups")]
    // Unity component CanvasGroup identifiers that allows us to show or hide certain ui elements. These elements specificall are what pops up when you press e on any of the platforms on the map.
    public CanvasGroup travel;
    public CanvasGroup town;
    public CanvasGroup barn;
    public CanvasGroup forest;
    public CanvasGroup swamp;
    public CanvasGroup mount;

    [Header("Main Menu Canvas Group")]
    // Same as previous comment, but this one relates to the main menu UI.
    public CanvasGroup mainMenu;

    [Header("Monster Selection Canvas Groups")]
    // Same as the first comment, but this canvasgroup is for the Monster Selection UI.
    public CanvasGroup monSelect;

    // These canvasgroups control the visibilty of the selection buttons for the monsters and the stats screens for the monsters.
    public CanvasGroup slimeSelect;
    public CanvasGroup slimeStat;
    public CanvasGroup swordSelect;
    public CanvasGroup swordStat;
    public CanvasGroup wyvernSelect;
    public CanvasGroup wyvernStat;

    [Header("Monster Text Objects")]
    // CanvasGroups for the individual name and stat ui's for the the monsters.
    public Text slimeName;
    public Text slimeMax;
    public Text slimeCurr;
    public Text swordName;
    public Text swordMax;
    public Text swordCurr;
    public Text wyvernName;
    public Text wyvernMax;
    public Text wyvernCurr;

    [Header("Chain Objects")]
    // CanvasGroup for the Chains UI that allows for showing and hiding it.
    public CanvasGroup chains;
    // Unity Text component refrence for the chain display text that displays how many chains you have left.
    public Text chainText;

    [Header("Input Field Objects")]
    // Unity InputField component refrences for changing the slime, sword and wyvern names.
    public InputField changeSlime;
    public InputField changeSword;
    public InputField changeWyvern;

    [Header("Unity Component & Class Refrences")]
    /// <summary>
    /// Unity Animator component refrence so we can trigger ui animations that are attached to the gameobject that this script is attached to.
    /// </summary>
    public Animator anim;
    /// <summary>
    /// allows us to inherit functions and variables from the Player_Map class and refrence them with a unique identifier.
    /// </summary>
    public Player_Map player;
    /// <summary>
    /// Inherited class of MonSave with unique identfier of mon, for use in accessing MonSave functions and variables.
    /// </summary>
    public MonSave mon;

    /* This awake function makes sure that all menus start closed when the scene loads, that the anim refrence is set to the Animator component within a child object,
     * the mon refrence is set to the MonSave component of the game object in the scene with the "MonSave" tag.
     * It then checks if the statsSaved bool from the MonSave class is true or false, showing the main menu and hiding the chain ui if false and showing the chain UI if true.
     */
    private void Awake()
    {
        CloseMenu();
        anim = GetComponentInChildren<Animator>();
        mon = GameObject.FindGameObjectWithTag("MonSave").GetComponent<MonSave>();
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

    /// <summary>
    /// Shows the town ui on through the assosciated CanvasGroups
    /// </summary>
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

    /// <summary>
    /// Shows the forest ui on through the assosciated CanvasGroups
    /// </summary>
    public void ShowForest()
    {
        travel.interactable = true;
        travel.alpha = 1;
        travel.blocksRaycasts = true;
        forest.interactable = true;
        forest.alpha = 1;
        forest.blocksRaycasts = true;
    }

    /// <summary>
    /// Shows the swamp ui on through the assosciated CanvasGroups
    /// </summary>
    public void ShowSwamp()
    {
        travel.interactable = true;
        travel.alpha = 1;
        travel.blocksRaycasts = true;
        swamp.interactable = true;
        swamp.alpha = 1;
        swamp.blocksRaycasts = true;
    }

    /// <summary>
    /// Shows the mountain ui on through the assosciated CanvasGroups
    /// </summary>
    public void ShowMount()
    {
        travel.interactable = true;
        travel.alpha = 1;
        travel.blocksRaycasts = true;
        mount.interactable = true;
        mount.alpha = 1;
        mount.blocksRaycasts = true;
    }

    /// <summary>
    /// Shows the monsters that you can select if the right conditions are met.
    /// </summary>
    public void ShowMonSelect()
    {
        // closes all open menus before opening this menu.
        CloseMenu();
        // hides the chains ui so its not in the way
        HideChains();
        // stops the player from being able to move.
        player.movementEnabled = false;
        // this little block turns the main ui for the mon select screen on.
        monSelect.interactable = true;
        monSelect.alpha = 1;
        monSelect.blocksRaycasts = true;
        // checks to see if the monsave class's statsSave variable is true or false.
        if (mon.statsSaved == true)
        {
            // Saves the last monster that you had selected as your active monster before you opened the selection screen.
            StartCoroutine(mon.SaveLastMon());
            // Loads the available party members, provides the value for slimeCaught, swordCaught and wyvernCaught bools.
            mon.LoadParty();
            // If the player owns a slime, the selection screen and the stats screen of the slime will be set to be fully visible, loading the stats for it through the MonStats function.
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
            // If the player doesn't own a slime, the stats will be completly invisible but the selection button and the base stats will  be greyed out and unable to be interacted with.
            else if (mon.slimeCaught == false)
            {
                slimeSelect.interactable = false;
                slimeSelect.alpha = 0.5f;
                slimeSelect.blocksRaycasts = false;
                slimeStat.interactable = false;
                slimeStat.alpha = 0;
                slimeStat.blocksRaycasts = false;
            }
            // If the player owns a sword, the selection screen and the stats screen of the sword will be set to be fully visible, loading the stats for it through the MonStats function.
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
            // If the player doesn't own a sword, the stats will be completly invisible but the selection button and the base stats will  be greyed out and unable to be interacted with.
            else if (mon.swordCaught == false)
            {
                swordSelect.interactable = false;
                swordSelect.alpha = 0.5f;
                swordSelect.blocksRaycasts = false;
                swordStat.interactable = false;
                swordStat.alpha = 0;
                swordStat.blocksRaycasts = false;
            }
            // If the player owns a wyvern, the selection screen and the stats screen of the wyvern will be set to be fully visible, loading the stats for it through the MonStats function.
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
            // If the player doesn't own a wyvern, the stats will be completly invisible but the selection button and the base stats will  be greyed out and unable to be interacted with.
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
        // If the statsSaved bool is false, the selection buttons and base stats will be visible but the stats will not be visible, the names displayed will also be set to their default names.
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

    /// <summary>
    /// Shows the chains ui with the current amount of chains if the statsSaved bool is true, and sets the amount of chains the player has to 6 as well as the displayed amount if the statsSaved bool is false.
    /// </summary>
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

    /// <summary>
    /// Hides the chains ui when called.
    /// </summary>
    public void HideChains()
    {
        chains.interactable = false;
        chains.alpha = 0;
        chains.blocksRaycasts = false;
    }

    /// <summary>
    /// Closes all open UI, shows the Chain UI and enables the player to move.
    /// </summary>
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

    /// <summary>
    /// Called when the Enter Town button is pressed, healing the players main monster if the stats are saved and triggering the NoGo function and animation if false, closes all menus at the end.
    /// </summary>
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
    }

    /// <summary>
    /// Called when the Enter Barn button is pressed, closes all the menus, hides the chains ui, calls the ShowMonSelect function if statsSaved is true,
    /// otherwise running the NoGo function and telling the player to go to the forest.
    /// </summary>
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

    /// <summary>
    /// Called when the player presses the new game button on the main menu, hides the chains UI sets all the Caught bools and the statsSaved bool to false,
    /// ensuring that no previous monster stats are loaded and saves the bool new value with the SaveParty function and opening the Monster Selection Screen.
    /// </summary>
    public void NewGame()
    {
        HideChains();
        mon.statsSaved = false;
        mon.slimeCaught = false;
        mon.swordCaught = false;
        mon.wyvernCaught = false;
        mon.SaveParty();
        ShowMonSelect();
    }

    /// <summary>
    /// Called when you select a monster through a select button, each of which are set to pass a different value to the monNum parameter.
    /// Will run the SetNewMon function with the monNum param value and the NewGameStats function with the monNum param value if the statsSaved bool is false,
    /// running the LoadMon function with the monNum param value if statsSaved is true, and closing all menus at the end regardless of bool value.
    /// </summary>
    /// <param name="monNum"></param>
    public void MonHUD(int monNum)
    {
        if(mon.statsSaved != true)
        {
            mon.NewGameStats(monNum);
        }
        else if(mon.statsSaved == true)
        {
            mon.LoadMon(monNum);
        }
        CloseMenu();
    }

    /// <summary>
    /// Called when the player presses the Continue Game Button on the main menu. Closes all menus, sets statsSaved to true, runs the LoadParty functions.
    /// Starts the LoadLastMon coroutine and shows the Chains UI.
    /// </summary>
    public void ContinueGame()
    {
        CloseMenu();
        mon.statsSaved = true;
        mon.LoadParty();
        StartCoroutine(mon.LoadLastMon());
        ShowChains();
    }

    /// <summary>
    /// Called on the awake function when you first load the game and is called whever you press escape while on the map.
    /// Closes all menus, hides chain UI, sets all main menu canvas group attributes to true or one to show the main menu and disables player movement.
    /// </summary>
    public void ShowMainMenu()
    {
        CloseMenu();
        HideChains();
        mainMenu.interactable = true;
        mainMenu.alpha = 1;
        mainMenu.blocksRaycasts = true;
        player.movementEnabled = false;
    }

    /// <summary>
    /// Triggers the NoGo function when called.
    /// This function is called when you attempt to go to the town straight after starting a new game.
    /// </summary>
    public void NoGo()
    {
        anim.SetTrigger("noGo");
    }

    /// <summary>
    /// Saves the last monster the player had set as their main monster then shutsdown the game.
    /// Called when the player presses the quit button.
    /// </summary>
    public void QuitGame()
    {
        StartCoroutine(mon.SaveLastMon());
        Application.Quit();
    }

    /// <summary>
    /// Called when a name is inputted in the input fields for each monster.
    /// Saves the last monster used, loads the monster with a new name depending on the value of the monIndex param,
    /// updates the monsters name in the MonSave class with the new name set in the input field and updates the name text in the UI with the new name.
    /// Saves the new name into its text file with the SaveMapStats function with the monIndex param and then loads the last monster the player was using.
    /// </summary>
    /// <param name="monIndex"></param>
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
        mon.SaveMapStats(monIndex);
        StartCoroutine(mon.LoadLastMon());
    }


    /// <summary>
    /// Called within the ShowMonSelect function to load the stats of the monsters that the player owns to display their stats in the Selection Menu.
    /// Saves the last monster that the player was using so it can be loaded after loading the stats of the party monsters and setting the stats text of the relative monster to the loaded stats.
    /// </summary>
    /// <param name="monIndex"></param>
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
