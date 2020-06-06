using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author: Eric Hanks
// Last Updated: 06/06/2020
public class StatsText : MonoBehaviour
{
    // Text field to utilize the Unity Text component that is attached to the same game object as this class
    private Text statText;
    // Monster class field to access and refrence the stats of the monsters the player has to choose from.
    public Monster monster;

    public void Awake()
    {
        // sets statText to refrence the Text component attached to same game object as script
        statText = GetComponent<Text>();
        // sets the text displayed by statText UI element to display the max HP, min and max damage of the monster in an easily read format
        statText.text = $"{"Max HP: " + monster.data.maxHP}\n{"Min Dmg: " + monster.data.attDmg.x}\n{"Max Dmg: " + monster.data.attDmg.y}";
    }
}
