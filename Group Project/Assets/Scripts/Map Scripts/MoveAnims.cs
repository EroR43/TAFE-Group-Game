using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnims : MonoBehaviour
{
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TownForest(bool toggle)
    {
        anim.SetBool("moveForest", toggle);
    }

    public void ForestSwamp(bool toggle)
    {
        anim.SetBool("moveSwamp", toggle);
    }

    public void SwampMount(bool toggle)
    {
        anim.SetBool("moveMount", toggle);
    }
}
