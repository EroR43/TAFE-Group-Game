using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsText : MonoBehaviour
{
    private Text statText;
    public Monster monster;

    public void Awake()
    {
        statText = GetComponent<Text>();
        statText.text = $"{"Max HP: " + monster.data.maxHP}\n{"Min Dmg: " + monster.data.attDmg.x}\n{"Max Dmg: " + monster.data.attDmg.y}";
    }
}
