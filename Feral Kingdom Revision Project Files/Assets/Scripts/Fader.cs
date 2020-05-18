using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public Animator anim;

    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Fade(bool toggle)
    {
        anim.SetBool("inTrigger", toggle);
    }
}
