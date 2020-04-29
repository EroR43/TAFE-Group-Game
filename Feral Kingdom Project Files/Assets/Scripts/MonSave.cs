using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 29/04/2020

public class MonSave : MonoBehaviour
{
    /// <summary>
    /// string to save the name of the players current monster.
    /// </summary>
    [SerializeField]
    public string unitName;
    /// <summary>
    /// int to save the current level of the players monster.
    /// </summary>
    [SerializeField]
    public int unitLevel;

    /// <summary>
    /// int to store the base health of the players current monster. 
    /// </summary>
    [SerializeField]
    public int baseHealth;
    /// <summary>
    /// int to store the maxHP of the players current monster.
    /// </summary>
    [SerializeField]
    public int maxHP;
    /// <summary>
    /// int to store the current HP of the players current monster.
    /// </summary>
    [SerializeField]
    public int currentHP;

    /// <summary>
    /// int to store the base armor value of the current monster.
    /// </summary>
    [SerializeField]
    public int baseArmor;
    /// <summary>
    /// int to store the current armor of the current monster.
    /// </summary>
    [SerializeField]
    public int currentArmor;

    /// <summary>
    /// int to store the maxEXP of the players current monster.
    /// </summary>
    [SerializeField]
    public int maxEXP;
    /// <summary>
    /// int to store the current EXP of the players current monster.
    /// </summary>
    [SerializeField]
    public int currentEXP;

    /// <summary>
    /// int to store the base damage value of the players current monster.
    /// </summary>
    [SerializeField]
    public int baseDamage;
    /// <summary>
    /// int to store the current damage value of the players current monster.
    /// </summary>
    [SerializeField]
    public int currentDamage;
    /// <summary>
    /// int to store the base heal amount value of the players current monster.
    /// </summary>
    [SerializeField]
    public int baseHealAmnt;
    /// <summary>
    /// int to store the current heal amount value of the players current monster.
    /// </summary>
    [SerializeField]
    public int currentHealAmnt;

    /// <summary>
    /// used to identify the differnt monster types with a specific number.
    /// </summary>
    public int monIndex;

    /// <summary>
    /// number of chains the player has on hand, chains are used as a consumable item to catch monsters.
    /// </summary>
    public int chainCount;

    /// <summary>
    /// refrence to MonsterPrefabs class to make use of its stored prefab game objects and the function stored within the class.
    /// </summary>
    public MonsterPrefabs prefabs;
    /// <summary>
    /// A gameobject refrence for the prefab that the players current monster is using.
    /// </summary>
    public GameObject currPrefab;
    /// <summary>
    /// allows us to make use of the LogReader's functions and variables. Used for loading and saving info to txt files.
    /// </summary>
    public LogReader reader;

    /// <summary>
    /// bool that defines whether monster stats should be loaded from the txt file or not.
    /// </summary>
    public bool statsSaved;

    /// <summary>
    /// Bool used for defining if the slimes stats should be loaded and if the slime should be available for selection on the party screen.
    /// </summary>
    public bool slimeCaught;
    /// <summary>
    /// Bool used for defining if the swords stats should be loaded and if the sword should be available for selection on the party screen.
    /// </summary>
    public bool swordCaught;
    /// <summary>
    /// Bool used for defining if the wyvern stats should be loaded and if the wyvern should be available for selection on the party screen.
    /// </summary>
    public bool wyvernCaught;

    /// <summary>
    /// Vector 3 to store the last position of the player object in the map scene, this position is loaded after coming back to the map scene after finishing a battle.
    /// </summary>
    public Vector3 lastPos = new Vector3();
    
    public void Awake()
    {
        // sets what the reader field refrences, being the LogReader class attached to the parent game object. 
        reader = GetComponent<LogReader>();
        // sets prefabs to be the MonsterPrefabs class attached to the parent game object that this calss is attached to. 
        prefabs = GetComponent<MonsterPrefabs>();
        // sets a new obj array to to refrence the game object in the scene with the MonSave tag.
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MonSave");
        // checks if there is more than 1 object with the MonSave tag and deletes any extras.
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        // Prevents this game object from being destroyed when a new scene is loaded.
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Saves the last position of the player gameobject within the map scene to this class, allowing us to reset the players position to its last position when we reload the map scene.
    /// </summary>
    /// <param name="pos"></param>
    public void SavePos(Vector3 pos)
    {
        lastPos.x = pos.x;
        lastPos.z = pos.z;
    }

    /// <summary>
    /// Saves the index number of the last monster that the player had selected to a txt file along with the current amount of chains the player has.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SaveLastMon()
    {
        yield return new WaitForSeconds(1);
        reader.fileManager.logName = "LastMon";
        reader.Start();
        reader.SaveKeyValuePair("Index Number", monIndex.ToString());
        reader.SaveKeyValuePair("Chain Count", chainCount.ToString());
        yield return new WaitForSeconds(1);
    }

    /// <summary>
    /// Loads the the index number of the last monster that the player had selected from a txt file and then loads that mon through the LoadMon function,
    /// with the loaded index number as a parameter.
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadLastMon()
    {
        yield return new WaitForSeconds(1);
        reader.fileManager.logName = "LastMon";
        reader.Start();
        monIndex = reader.LoadIntByKey("Index Number");
        chainCount = reader.LoadIntByKey("Chain Count");
        yield return new WaitForSeconds(1);
        if (monIndex == 1)
        {
            LoadMon(monIndex);
        }
        else if (monIndex == 2)
        {
            LoadMon(monIndex);
        }
        else if (monIndex == 3)
        {
            LoadMon(monIndex);
        }
    }

    /// <summary>
    /// Saves the players currently owned monsters to a txt file and save whether they're alive through the SavePartyAlive function, 
    /// with all 3 index numbers as parameters to make sure the state of all party members are saved.
    /// </summary>
    public void SaveParty()
    {
        reader.fileManager.logName = "Party";
        reader.Start();
        reader.SaveKeyValuePair("Slime is Caught", slimeCaught.ToString());
        reader.SaveKeyValuePair("Sword is Caught", swordCaught.ToString());
        reader.SaveKeyValuePair("Wyvern is Caught", wyvernCaught.ToString());
        StartCoroutine(SavePartyAlive(1));
        StartCoroutine(SavePartyAlive(2));
        StartCoroutine(SavePartyAlive(3));
    }

    /// <summary>
    /// Loads the monsters that the player owns, this enables you to close the game and come back through the continue option with all the monsters you had before hand. 
    /// </summary>
    public void LoadParty()
    {
        reader.fileManager.logName = "Party";
        reader.Start();
        slimeCaught = reader.LoadBoolByKey("Slime is Caught");
        swordCaught = reader.LoadBoolByKey("Sword is Caught");
        wyvernCaught = reader.LoadBoolByKey("Wyvern is Caught");
    }

    /// <summary>
    /// Saves the stats of the enemy monster and saves them to the corrosponding unit files,
    /// this means the enemy will be added to your party with the stats they had when you caught it in battle.
    /// The players last monster is also saved before this so that the monster can be reloaded through its txt at the end of the execution of this function,
    /// the SaveParty function is run just before this to save the new addition to the party and make them accessible through the Barn.
    /// </summary>
    /// <param name="enemyUnit"></param>
    public void SaveEnem(Unit enemyUnit)
    {
        StartCoroutine(SaveLastMon());
        if (enemyUnit.monIndex == 1)
        {
            slimeCaught = true;
            reader.fileManager.logName = "Slime";
        }
        else if (enemyUnit.monIndex == 2)
        {
            swordCaught = true;
            reader.fileManager.logName = "Sword";
        }
        else if (enemyUnit.monIndex == 3)
        {
            wyvernCaught = true;
            reader.fileManager.logName = "Sword";
        }
        SaveBattleStats(enemyUnit);
        StartCoroutine(LoadLastMon());
        SaveParty();
    }

    /// <summary>
    /// Loads a monsters txt file depending on what the value of the monNum parameter that this function is passed,
    /// setting the name of the txt file that the text file manager is looking for according to the same parameter and loading the stats stored within the file,
    /// as well as setting the current prefab to the prefab of the selected monster.
    /// </summary>
    /// <param name="monNum"></param>
    public void LoadMon(int monNum)
    {
        if (monNum == 1)
        {
            monIndex = 1;
            reader.fileManager.logName = "Slime";
        }
        else if (monNum == 2)
        {
            monIndex = 2;
            reader.fileManager.logName = "Sword";
        }
        else if (monNum == 3)
        {
            monIndex = 3;
            reader.fileManager.logName = "Wyvern";
        }
        reader.Start();
        LoadFromTxtFile();
        currPrefab = prefabs.PlayerMon(monIndex);
    }

    public IEnumerator SavePartyAlive(int monIndex)
    {
        // Saves the last monster that was used.
        StartCoroutine(SaveLastMon());
        yield return new WaitForSeconds(1);
        // Checks the monIndex value that this function is passed as a parameter and whether the player currently owns the monster associated to that number.
        if ((monIndex == 1) && (slimeCaught == true))
        {
            /* Loads the stats of the slime from within its txt file, then checks if its current health is equal to 0, setting a new bool of slAl to false if it is and saving that bool to the txt file.
             * If the currentHP of the slime doesnt equal 0, it will set the slAl bool to true and save that to the txt file. */ 
            LoadMonStats(1);
            reader.fileManager.logName = "Party";
            reader.Start();
            if (currentHP == 0)
            {
                bool slAl = false;
                reader.SaveKeyValuePair("Slime Alive: ", slAl.ToString());
            }
            else
            {
                bool slAl = true;
                reader.SaveKeyValuePair("Slime Alive: ", slAl.ToString());
            }
        }
        // If the player does not currently own the slime, the slAl bool will be saved as false as well.
        else if (slimeCaught == false)
        {
            reader.Start();
            reader.fileManager.logName = "Party";
            bool slAl = false;
            reader.SaveKeyValuePair("Slime Alive: ", slAl.ToString());
        }
        // Checks the monIndex value that this function is passed as a parameter and whether the player currently owns the monster associated to that number.
        if ((monIndex == 2) && (swordCaught == true))
        {
            /* Loads the stats of the sword from within its txt file, then checks if its current health is equal to 0, setting a new bool of swAl to false if it is and saving that bool to the txt file.
             * If the currentHP of the sword doesnt equal 0, it will set the swAl bool to true and save that to the txt file. */
            LoadMonStats(2);
            reader.fileManager.logName = "Party";
            reader.Start();
            if (currentHP >= 1)
            {
                bool swAl = true;
                reader.SaveKeyValuePair("Sword Alive: ", swAl.ToString());
            }
            else
            {
                bool swAl = false;
                reader.SaveKeyValuePair("Sword Alive: ", swAl.ToString());
            }
        }
        // If the player does not currently own the sword, the swAl bool will be saved as false as well.
        else if (swordCaught == false)
        {
            reader.fileManager.logName = "Party";
            reader.Start();
            bool swAl = false;
            reader.SaveKeyValuePair("Sword Alive: ", swAl.ToString());
        }
        // Checks the monIndex value that this function is passed as a parameter and whether the player currently owns the monster associated to that number.
        if ((monIndex == 3) && (wyvernCaught == true))
        {
            /* Loads the stats of the wyvern from within its txt file, then checks if its current health is equal to 0, setting a new bool of wyAl to false if it is and saving that bool to the txt file.
             * If the currentHP of the wyvern doesnt equal 0, it will set the wyAl bool to true and save that to the txt file. */
            LoadMonStats(3);
            reader.fileManager.logName = "Party";
            reader.Start();
            if (currentHP == 0)
            {
                bool wyAl = false;
                reader.SaveKeyValuePair("Wyvern Alive: ", wyAl.ToString());
            }
            else
            {
                bool wyAl = true;
                reader.SaveKeyValuePair("Wyvern Alive: ", wyAl.ToString());
            }
        }
        // If the player does not currently own the wyvern, the wyAl bool will be saved as false as well.
        else if (wyvernCaught != true)
        {
            reader.fileManager.logName = "Party";
            reader.Start();
            bool wyAl = false;
            reader.SaveKeyValuePair("Wyvern Alive: ", wyAl.ToString());
        }
        // The function ends with loading the last used monster.
        yield return new WaitForSeconds(1);
        StartCoroutine(LoadLastMon());
    }

    /// <summary>
    /// This function loads the values of the alive bools for each monster and returns those values according to the partNum param's value, returning false if all conditions are false.
    /// </summary>
    /// <param name="partNum"></param>
    /// <returns></returns>
    public bool LoadPartyAlive(int partNum)
    {
        reader.fileManager.logName = "Party";
        reader.Start();
        bool slAl = reader.LoadBoolByKey("Slime Alive: ");
        bool swAl = reader.LoadBoolByKey("Sword Alive: ");
        bool wyAl = reader.LoadBoolByKey("Wyvern Alive: ");
        if((partNum == 1) && (slimeCaught == true))
        {
            return slAl;
        }
        if((partNum == 2) && (swordCaught == true))
        {
            return swAl;
        }
        if (partNum == 3 && (wyvernCaught == true))
        {
            return wyAl;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// This function is a variation of the LoadMon function that follows the same principles, 
    /// but this function is when a new monster is added to the party or when a new game is started.
    /// It saves the last used monster, then sets the txt file name that the text file manager is reading from,
    /// sets the related 'Caught' bool to true so the monster is diplayed in party selection, 
    /// it also sets the current prefab to the prefab allocated to the monIndex int that the function is passed as a parameter and saves the stats from the prefabs unit class as to the monsters associated txt file.
    /// </summary>
    /// <param name="monIndex"></param>
    public void NewGameStats(int monIndex)
    {
        if (monIndex == 1)
        {
            reader.fileManager.logName = "Slime";
            slimeCaught = true;
        }
        else if (monIndex == 2)
        {
            reader.fileManager.logName = "Sword";
            swordCaught = true;
        }
        else if (monIndex == 3)
        {
            reader.fileManager.logName = "Wyvern";
            wyvernCaught = true;
        }
        currPrefab = prefabs.PlayerMon(monIndex);
        Unit prefabStats = currPrefab.GetComponent<Unit>();
        SaveBattleStats(prefabStats);
    }

    /// <summary>
    /// Loads a monsters stats from a txt file using the logreaders load by key functions.
    /// </summary>
    public void LoadFromTxtFile()
    {
        unitName = reader.LoadStringByKey("Name");
        unitLevel = reader.LoadIntByKey("Level");
        baseHealth = reader.LoadIntByKey("Base Health");
        maxHP = reader.LoadIntByKey("Max HP");
        currentHP = reader.LoadIntByKey("Current HP");
        baseArmor = reader.LoadIntByKey("Base Armor");
        currentArmor = reader.LoadIntByKey("Current Armor");
        maxEXP = reader.LoadIntByKey("Max EXP");
        currentEXP = reader.LoadIntByKey("Current EXP");
        baseDamage = reader.LoadIntByKey("Base Damage");
        currentDamage = reader.LoadIntByKey("Current Damage");
        baseHealAmnt = reader.LoadIntByKey("Base Heal Amount");
        currentHealAmnt = reader.LoadIntByKey("Current Heal Amount");
    }

    /// <summary>
    /// Loads stats from a txt file then sets the base stats of the unit class that the function is passed as a param.
    /// </summary>
    /// <param name="unit"></param>
    public void LoadBaseStats(Unit unit)
    {
        LoadFromTxtFile();
        if (statsSaved != true)
        {
            unit.unitLevel = 5;
            unit.baseHealth = baseHealth;
            unit.baseArmor = baseArmor;
            unit.baseDamage = baseDamage;
            unit.baseHealAmnt = baseHealAmnt;
        }
    }

    /// <summary>
    /// Loads stats from a txt file then sets the current stats of the unit class that the function is passed as a param.
    /// </summary>
    /// <param name="player"></param>
    public void LoadStats(Unit player)
    {
        LoadFromTxtFile();
        if (statsSaved == true)
        {
            player.unitName = unitName;
            player.unitLevel = unitLevel;
            player.currentHP = currentHP;
            player.currentEXP = currentEXP;
        }
    }


    /// <summary>
    /// Used when you want to load only the stats of the monster related to the monNum param value and not set the prefab.
    /// </summary>
    /// <param name="monNum"></param>
    public void LoadMonStats(int monNum)
    {
        if (monNum == 1)
        {
            reader.fileManager.logName = "Slime";
        }
        else if (monNum == 2)
        {
            reader.fileManager.logName = "Sword";
        }
        else if (monNum == 3)
        {
            reader.fileManager.logName = "Wyvern";
        }
        reader.Start();
        LoadFromTxtFile();
    }

    /// <summary>
    /// Sets the chain count to 6, sets the current HP of the players selected monster to its max health, 
    /// reseting it to the max health if it goes over, then saves the new stats to appropriate txt file,
    /// saves the current party alive status
    /// and loads the files from the txt file for added precautions.
    /// </summary>
    public void TownHeal()
    {
        chainCount = 6;
        currentHP = maxHP;
        if (currentHP > maxHP)
            currentHP = maxHP;
        SaveMapStats(monIndex);
        StartCoroutine(SavePartyAlive(monIndex));
        LoadFromTxtFile();
    }

    /// <summary>
    /// Saves the stats of the unit class param this function is passed to the approriate txt file according to the class's monIndex value.
    /// </summary>
    /// <param name="player"></param>
    public void SaveBattleStats(Unit player)
    {
        if (player.monIndex == 1)
        {
            reader.fileManager.logName = "Slime";
        }
        else if (player.monIndex == 2)
        {
            reader.fileManager.logName = "Sword";
        }
        else if (player.monIndex == 3)
        {
            reader.fileManager.logName = "Wyvern";
        }
        reader.Start();
        statsSaved = true;
        reader.SaveKeyValuePair("Name", player.unitName);
        reader.SaveKeyValuePair("Level", player.unitLevel.ToString());
        reader.SaveKeyValuePair("Base Health", player.baseHealth.ToString());
        reader.SaveKeyValuePair("Max HP", player.maxHP.ToString());
        reader.SaveKeyValuePair("Current HP", player.currentHP.ToString());
        reader.SaveKeyValuePair("Base Armor", player.baseArmor.ToString());
        reader.SaveKeyValuePair("Current Armor", player.currentArmor.ToString());
        reader.SaveKeyValuePair("Max EXP", player.maxEXP.ToString());
        reader.SaveKeyValuePair("Current EXP", player.currentEXP.ToString());
        reader.SaveKeyValuePair("Base Damage", player.baseDamage.ToString());
        reader.SaveKeyValuePair("Current Damage", player.currentDamage.ToString());
        reader.SaveKeyValuePair("Base Heal Amount", player.baseHealAmnt.ToString());
        reader.SaveKeyValuePair("Current Heal Amount", player.currentHealAmnt.ToString());
    }
    /// <summary>
    /// Saves the stats within this class to the monsters txt file according to the monIndex param this function is passed.
    /// </summary>
    /// <param name="monIndex"></param>
    public void SaveMapStats(int monIndex)
    {
        if (monIndex == 1)
        {
            reader.fileManager.logName = "Slime";
        }
        else if (monIndex == 2)
        {
            reader.fileManager.logName = "Sword";
        }
        else if (monIndex == 3)
        {
            reader.fileManager.logName = "Wyvern";
        }
        reader.Start();
        statsSaved = true;
        reader.SaveKeyValuePair("Name", unitName);
        reader.SaveKeyValuePair("Level", unitLevel.ToString());
        reader.SaveKeyValuePair("Base Health", baseHealth.ToString());
        reader.SaveKeyValuePair("Max HP", maxHP.ToString());
        reader.SaveKeyValuePair("Current HP", currentHP.ToString());
        reader.SaveKeyValuePair("Base Armor", baseArmor.ToString());
        reader.SaveKeyValuePair("Current Armor", currentArmor.ToString());
        reader.SaveKeyValuePair("Max EXP", maxEXP.ToString());
        reader.SaveKeyValuePair("Current EXP", currentEXP.ToString());
        reader.SaveKeyValuePair("Base Damage", baseDamage.ToString());
        reader.SaveKeyValuePair("Current Damage", currentDamage.ToString());
        reader.SaveKeyValuePair("Base Heal Amount", baseHealAmnt.ToString());
        reader.SaveKeyValuePair("Current Heal Amount", currentHealAmnt.ToString());
    }
}