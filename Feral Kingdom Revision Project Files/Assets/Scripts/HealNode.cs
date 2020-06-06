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

    private void Awake()
    {
        // fader is set to refrence the Fader component attached to this node
        fader = GetComponent<Fader>();
        // anim is set to refrence the Animator component attached to the child object of this node.
        anim = GetComponentInChildren<Animator>();
    }

    // when the player enters the trigger attatched to node, the Fade animation is triggered through the fader and anim components.
    public void OnTriggerEnter(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }

    /* while the player remains within the trigger, checks if the player presses the "E" key, 
     * running the TakeDamage func in the return of the GetMonster func within the GameManager with a parameter of -50, 
     * which acts to add health to the monster */
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
