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


    /* This awake function sets fader to refrence the Fader class component that is attached to the game object that this instance of code is attached to and 
     * sets the anim field to refrence the Animator component attached to a gameobject that is a child of the object that this code is on. */
    private void Awake()
    {
        fader = GetComponent<Fader>();
        anim = GetComponentInChildren<Animator>();
    }

    /* checks if the GameManager's battleNodeTable hashtable contains a key with the same value as the battleIndex of this node, if ther isn't, adding the battleIndex to it along with the value of completed or,
     * if it is in the hashtable, it will set completed to be the same value of the bool of the battleNodeTable key with the same value as the battleIndex */

    private void Start()
    {
        if (GameManager.battleNodeTable.ContainsKey(battleIndex) == false)
        {
            GameManager.battleNodeTable.Add(battleIndex, completed);
        }
        else
        {
            completed = (bool)GameManager.battleNodeTable[battleIndex];
        }
    }

    /// <summary>
    /// This function checks if the completed bool of this node is false and that the players health is above zero, 
    /// setting the currBattle int in gamemanager to equal the battleIndex of this node, loading the scene with the same build index as the battleIndex value and returning a value of true,
    /// if one of the conditions is not met, the function will return false
    /// </summary>
    /// <returns></returns>
    public bool GoToBattle()
    {
        if (completed == false && GameManager.instance.GetMonster().data.GetCurrHP() > 0)
        {
            GameManager.currBattle = battleIndex;
            SceneManager.LoadScene(battleIndex);
            return true;
        }
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

    // while the players model is in the trigger attached to this node, looks for if the player presses the "E" key and that the battleIndex is greater than 1, running GoToBattle if both conditions are met
    public void OnTriggerStay(Collider other)
    {
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
