using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 06/06/2020

public class FinalNode : MonoBehaviour
{
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

    // when the player enters the trigger attatched to node, the Fade animation is triggered through the fader and anim components.
    public void OnTriggerEnter(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }

    // while the player within the trigger, checks if they press "E" then checks if result of the QueryCompletion func in GameManage is true, if true it runs the GoToMainMenu func in the gamemanager
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameManager.QueryCompletion() == true)
                {
                    GameManager.instance.GoToMainMenu();
                }
            }
        }
    }

    // when the player exits the trigger attached to this node the fade animation is triggered off.
    public void OnTriggerExit(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }
}
