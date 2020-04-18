using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Map : MonoBehaviour
{
    //public bool atTown;
    //public bool atForest;
    //public bool atSwamp;
    //public bool atMount;

    public GameObject player;
    private bool movementEnabled;

    public void OnTriggerStay(Collider other)
    {
        MoveAnims move = player.GetComponent<MoveAnims>();
        Animator anim = player.GetComponent<Animator>();
        if (other.gameObject.CompareTag("Town"))
        {
            if (movementEnabled == true)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    move.TownForest(!anim.GetBool("moveForest"));
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Debug.Log("You cannont go that way...");
                }
            }
            
        }
        else if (other.gameObject.CompareTag("Forest"))
        {
            if (movementEnabled == true)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    move.ForestSwamp(!anim.GetBool("moveSwamp"));
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    move.TownForest(!anim.GetBool("moveForest"));
                }
            }
            
        }
        else if (other.gameObject.CompareTag("Swamp"))
        {
            if (movementEnabled == true)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    move.SwampMount(!anim.GetBool("moveMount"));
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    move.ForestSwamp(!anim.GetBool("moveSwamp"));
                }
            }
            
        }
        else if (other.gameObject.CompareTag("Mountain"))
        {
            if (movementEnabled == true)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Debug.Log("You cannont go that way...");
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    move.SwampMount(!anim.GetBool("moveMount"));
                }
            }
            
        }
    }
}
