using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonSave : MonoBehaviour
{
    public LogReader reader;
    [SerializeField]
    public string unitName;
    [SerializeField]
    public int unitLevel;


    [SerializeField]
    public int baseHealth;
    [SerializeField]
    public int maxHP;
    [SerializeField]
    public int currentHP;

    [SerializeField]
    public int baseArmor;
    [SerializeField]
    public int currentArmor;

    [SerializeField]
    public int maxEXP;
    [SerializeField]
    public int currentEXP;

    [SerializeField]
    public int baseDamage;
    [SerializeField]
    public int currentDamage;
    [SerializeField]
    public int baseHealAmnt;
    [SerializeField]
    public int currentHealAmnt;

    public int monIndex;

    public int chainCount;

    public MonsterPrefabs mon;
    public GameObject prefab;

    public bool statsSaved;

    public bool slimeCaught;
    public bool swordCaught;
    public bool wyvernCaught;

    public Vector3 lastPos = new Vector3();
    
    public void Awake()
    {
        reader = GetComponent<LogReader>();
        mon = GetComponent<MonsterPrefabs>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MonSave");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SavePos(Vector3 pos)
    {
        lastPos.x = pos.x;
        lastPos.z = pos.z;
    }

    public IEnumerator SaveLastMon()
    {
        yield return new WaitForSeconds(1);
        reader.fileManager.logName = "LastMon";
        reader.Start();
        reader.SaveKeyValuePair("Index Number", monIndex.ToString());
        reader.SaveKeyValuePair("Chain Count", chainCount.ToString());
        yield return new WaitForSeconds(1);
    }

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

    public void SaveParty()
    {
        reader.fileManager.logName = "Party";
        reader.Start();
        reader.SaveKeyValuePair("Slime is Caught", slimeCaught.ToString());
        reader.SaveKeyValuePair("Sword is Caught", swordCaught.ToString());
        reader.SaveKeyValuePair("Wyvern is Caught", wyvernCaught.ToString());
        SavePartyAlive(1);
        SavePartyAlive(2);
        SavePartyAlive(3);
    }

    public void LoadParty()
    {
        reader.fileManager.logName = "Party";
        reader.Start();
        slimeCaught = reader.LoadBoolByKey("Slime is Caught");
        swordCaught = reader.LoadBoolByKey("Sword is Caught");
        wyvernCaught = reader.LoadBoolByKey("Wyvern is Caught");
    }

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
        prefab = mon.PlayerMon(monIndex);
    }

    public IEnumerator SavePartyAlive(int monIndex)
    {
        StartCoroutine(SaveLastMon());
        yield return new WaitForSeconds(1);
        if ((monIndex == 1) && (slimeCaught == true))
        {
            LoadMonStats(1);
            reader.fileManager.logName = "Party";
            reader.Start();
            if (currentHP >= 1)
            {
                bool slAl = true;
                reader.SaveKeyValuePair("Slime Alive: ", slAl.ToString());
            }
            else
            {
                bool slAl = false;
                reader.SaveKeyValuePair("Slime Alive: ", slAl.ToString());
            }
        }
        else if (slimeCaught == false)
        {
            reader.Start();
            reader.fileManager.logName = "Party";
            bool slAl = false;
            reader.SaveKeyValuePair("Slime Alive: ", slAl.ToString());
        }
        if ((monIndex == 2) && (swordCaught == true))
        {
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
                reader.SaveKeyValuePair("Slime Alive: ", swAl.ToString());
            }
        }
        else if (swordCaught == false)
        {
            reader.fileManager.logName = "Party";
            reader.Start();
            bool swAl = false;
            reader.SaveKeyValuePair("Sword Alive: ", swAl.ToString());
        }
        if ((monIndex == 3) && (wyvernCaught == true))
        {
            LoadMonStats(3);
            reader.fileManager.logName = "Party";
            reader.Start();
            if (currentHP >= 1)
            {
                bool wyAl = true;
                reader.SaveKeyValuePair("Wyvern Alive: ", wyAl.ToString());
            }
            else
            {
                bool wyAl = false;
                reader.SaveKeyValuePair("Wyvern Alive: ", wyAl.ToString());
            }
        }
        else if (wyvernCaught == false)
        {
            reader.fileManager.logName = "Party";
            reader.Start();
            bool wyAl = false;
            reader.SaveKeyValuePair("Wyvern Alive: ", wyAl.ToString());
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(LoadLastMon());
    }

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
        if ((partNum == 3 && wyvernCaught == true))
        {
            return wyAl;
        }
        else
        {
            return false;
        }
    }

    public void SetNewMon(int monNum)
    {
        if (monNum == 1)
        {
            monIndex = 1;
            reader.fileManager.logName = "Slime";
            slimeCaught = true;
        }
        else if (monNum == 2)
        {
            monIndex = 2;
            reader.fileManager.logName = "Sword";
            swordCaught = true;
        }
        else if (monNum == 3)
        {
            monIndex = 3;
            reader.fileManager.logName = "Wyvern";
            wyvernCaught = true;
        }
        reader.Start();
        LoadFromTxtFile();
        prefab = mon.PlayerMon(monIndex);
        StartCoroutine(SaveLastMon());
        SaveParty();
        StartCoroutine(LoadLastMon());
    }

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


    public void TownHeal()
    {
        chainCount = 6;
        currentHP = maxHP;
        if (currentHP > maxHP)
            currentHP = maxHP;
        SaveMapStats();
        StartCoroutine(SavePartyAlive(monIndex));
        LoadFromTxtFile();
    }


    public void SaveBattleStats(Unit player)
    {
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

    public void SaveMapStats()
    {
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