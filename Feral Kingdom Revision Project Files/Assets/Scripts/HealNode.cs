using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 06/06/2020

public class HealNode : MonoBehaviour
{
    // Fader class field to use the Fader component attached to this node
    private Fader fader;
    // Animator field to utilise the Unity Animator component attached to the child object of this node
    private Animator anim;

    /* This awake function sets fader to refrence the Fader class component that is attached to the game object that this instance of code is attached to and 
     * sets the anim field to refrence the Animator component attached to a gameobject that is a child of the object that this code is on. */
    private void Awake()
    {
        fader = GetComponent<Fader>();
        anim = GetComponentInChildren<Animator>();
    }

    // when the player enters the trigger attatched to node, the Fade animation is triggered through the fader and anim components.
    public void OnTriggerEnter(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }

    // Heals the player's monster when activated.
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.instance.GetMonster().data.TakeDamage(-50);
            }
        }
    }

    // when the player exits the trigger attached to this node the fade animation is triggered off.
    public void OnTriggerExit(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }
}
