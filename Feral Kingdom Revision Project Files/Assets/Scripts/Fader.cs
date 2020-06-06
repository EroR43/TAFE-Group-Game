using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 06/06/2020

public class Fader : MonoBehaviour
{
    // Field with unique id to refrence and access teh animator attached to gameobject
    public Animator anim;

    // Gets the animator component that is attached to a child object to set anim to refrence the animator.
    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // sets the bool attached to the animator component with the name "inTrigger" to the value of the toggle parameter the function is passed.
    public void Fade(bool toggle)
    {
        anim.SetBool("inTrigger", toggle);
    }
}
