using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Author: Eric Hanks
// Last Edited: 06/06/2020

public class BattleNode : MonoBehaviour
{
    // int to hold scene index of the battle scenes, is set to each node exclusively
    public int battleIndex;

    // bool that denotes if the battle that the node loads is won or not.
    [SerializeField]
    private bool completed = false;

    // Fader class field to refrence the fader component attached to the game object
    private Fader fader;
    // Animator class field to refrence and utilise the animator unity component attached to the child of this game object
    private Animator anim;

    private void Awake()
    {
        // fader is set to refrence the Fader class component that is attached to the game object that this instance of code is attached to.
        fader = GetComponent<Fader>();
        // sets the anim field to refrence the Animator component attached to a gameobject that is a child of the object that this code is on.
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        // checks if the GameManager's battleNodeTable hashtable contains a key with the same value as the battleIndex of this node, if ther isn't, adding the battleIndex to it along with the value of completed.
        if (GameManager.battleNodeTable.ContainsKey(battleIndex) == false)
        {
            GameManager.battleNodeTable.Add(battleIndex, completed);
        }
        // if it is in the hashtable, it will set completed to be the same value of the bool of the battleNodeTable key with the same value as the battleIndex
        else
        {
            completed = (bool)GameManager.battleNodeTable[battleIndex];
        }
    }

    /// <summary>
    /// Returns false if battle has been completed.
    /// </summary>
    /// <returns></returns>
    public bool GoToBattle()
    {
        // checks if completed is false and that the health of the players monster is greater than 0
        if (completed == false && GameManager.instance.GetMonster().data.GetCurrHP() > 0)
        {
            // sets the value of the GameManager's currBattle int to be the value of the battleIndex of this node
            GameManager.currBattle = battleIndex;
            // Loads the scene with the scene index that has the same value as the battleIndex int
            SceneManager.LoadScene(battleIndex);
            // returns true
            return true;
        }
        // if it is completed, or the players monster has 0 health, the func returns false
        else
        {
            return false;
        }
    }

    // when run, sets completed to true
    public void CompleteBattle()
    {
        completed = true;
    }

    // when the player enters the trigger attatched to node, the Fade animation is triggered through the fader and anim components.
    public void OnTriggerEnter(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }


    public void OnTriggerStay(Collider other)
    {
        // while the players model is in the trigger attached to this node, looks for if the player presses the "E" key and that the battleIndex is greater than 1, running GoToBattle if both conditions are met
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && battleIndex > 1)
            {
                //Debug.Log("Triggered");
                GoToBattle();
            }
            //else
            //{
            //    Debug.Log("Triggered 2");
            //}
        }
    }

    // when the player exits the trigger attached to this node the fade animation is triggered off.
    public void OnTriggerExit(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }
}
