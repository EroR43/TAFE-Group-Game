using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 06/06/2020

public class PlayerMove : MonoBehaviour
{
    // float to hold the current speed the player is allowed to move
    private float currentSpeed;
    // float to hold the max speed that the player is allowed to move
    public float moveSpeed = 8f;

    // Start is called before the first frame update and sets the currentSpeed to the moveSpeed
    void Start()
    {
        currentSpeed = moveSpeed;
    }

    // Update is called once per frame, running the PlayerMovement func every frame as well
    void Update()
    {
        PlayerMovement();
    }

    // Checks if the player presses one of the commonly used movement keys on their keyboard, translating the player object in that direction at the speed of currentSpeed
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
}
