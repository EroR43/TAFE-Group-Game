using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 29/04/2020

public class Player_Map : MonoBehaviour
{
    /// <summary>
    /// refrence for the gameobject that this script is attached to.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Bool to disable or enable the players movement.
    /// </summary>
    public bool movementEnabled;
    /// <summary>
    /// Reference for the HUD class with a unique identifier.
    /// </summary>
    public HUD hud;
    /// <summary>
    /// float to be used for controlling the players movement speed.
    /// </summary>
    public float currentSpeed = 0;
    /// <summary>
    /// float for the default speed the player can moved.
    /// </summary>
    public float movementSpeed = 5f;
    /// <summary>
    /// Refrence for the object that contains the MonSave class.
    /// </summary>
    public GameObject monObj;
    /// <summary>
    /// Refrence for the MonSave class with a unique identifiers
    /// </summary>
    public MonSave monSave;

    /// <summary>
    /// Vector 3 to store the last position of the player object and send it to the MonSave class.
    /// </summary>
    [SerializeField]
    Vector3 pos = new Vector3();

    public void Awake()
    {
        // If statement to find if the object containing the MonSave class exists in the scene, instantiating the monObj that contains the MonSave class if it doesnt exist.
        if (GameObject.Find("MonSave(Clone)") != true)
        {
            Instantiate(monObj);
        }
        // Sets monSave to refrence the MonSave class that is present on the MonSave game object within the scene.
        monSave = GameObject.Find("MonSave(Clone)").GetComponent<MonSave>();
        // Runs the LoadFromTxtFile function from the MonSave clase.
        monSave.LoadFromTxtFile();
    }

    public void Start()
    {
        // Sets the current speed of the player to equal the value of movementSpeed.
        currentSpeed = movementSpeed;
        /* Checks if the statsSaved variable from the MonSave class is true or false, 
         * if the stats are saved, the x and z axis of the pos vector 3 is set to be the value of the lastPos vector 3's x and z value from the MonSave class,
         * then sets the position of the player to be that position.
         */
        if (monSave.statsSaved == true)
        {
            pos.x = monSave.lastPos.x;
            pos.z = monSave.lastPos.z;
            player.transform.position = pos;
        }
        // This if statement checks if your current position is the same as the position of the town, running the TownHeal function from the MonSave.
        if ((pos.x == -21.5f) && (pos.z == -8.48f))
        {
            monSave.TownHeal();
        }
    }

    
    public void Update()
    {
        // Checks if the movementEnable bool is true, running the PlayerMovement function only while its true.
        if(movementEnabled == true)
        {
            PlayerMovement();
        }
        /* Checks for when the player hits the escape key, saving the last monster used, the current party (which monsters the player owns),
         * and runs the ShowMainMenu function within the HUD class, which enables the Main Menu UI. */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            monSave.SaveLastMon();
            monSave.SaveParty();
            hud.ShowMainMenu();
        }
        // Updates the x and z values of pos with the current players location.
        pos.x = player.transform.position.x;
        pos.z = player.transform.position.z;
    }

    /// <summary>
    /// Moves the player in a direction according to which movement key is pressed at the currentSpeed variable. 
    /// Translates forward if W or uparrow is pressed etc.
    /// </summary>
    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        }
    }


    /* This script runs while you are inside of a collider and that you are pressing the "E" key, it will specifically check if the game object your standing has a specific tag, opening the related UI for that tag,
     * disabling the players movement and saving the players current position. */
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Town"))
        {
            if (Input.GetKeyDown(KeyCode.E) && movementEnabled == true)
            {
                hud.ShowTown();
                movementEnabled = false;
                monSave.SavePos(pos);
            }
        }
        else if (other.gameObject.CompareTag("Forest"))
        {
            if (Input.GetKeyDown(KeyCode.E) && movementEnabled == true)
            {
                hud.ShowForest();
                movementEnabled = false;
                monSave.SavePos(pos);
            }
        }
        else if (other.gameObject.CompareTag("Swamp"))
        {
            if (Input.GetKeyDown(KeyCode.E) && movementEnabled == true)
            {
                hud.ShowSwamp();
                movementEnabled = false;
                monSave.SavePos(pos);
            }
        }
        else if (other.gameObject.CompareTag("Mountain"))
        {
            if (Input.GetKeyDown(KeyCode.E) && movementEnabled == true)
            {
                movementEnabled = false;
                hud.ShowMount();
                monSave.SavePos(pos);
            }
        }
    }
}