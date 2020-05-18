using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealNode : MonoBehaviour
{
    private Fader fader;
    private Animator anim;

    private void Awake()
    {
        fader = GetComponent<Fader>();
        anim = GetComponentInChildren<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }

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

    public void OnTriggerExit(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }
}
