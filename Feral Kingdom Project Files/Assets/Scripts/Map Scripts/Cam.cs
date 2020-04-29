using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Last Edited: 29/04/2020

public class Cam : MonoBehaviour
{
    //Variables

    /// <summary>
    /// A transform set with a unique identfier of player to allow easier use of the position,
    /// rotation and scale values of the game object that transform is set as within the unity editor.
    /// </summary>
    public Transform player;
    /// <summary>
    /// a float variable with a value to be used in the equation to make the movement of the camera look fluid.
    /// </summary>
    public float smooth = 0.3f;

    /// <summary>
    /// a float variable to set a custom value for the height of the camera,
    /// made public to allow easier use within unity editor.
    /// </summary>
    public float height;

    /// <summary>
    /// a Custom identifier for the Vector3.zero 3D coordinates for ease of use within functions.
    /// </summary>
    private Vector3 velocity = Vector3.zero;


    //Methods
    /// <summary>
    /// Within the update function, a local Vector 3 variable is created to house a new set of Vector3 co-ordinates for ease of use.
    /// The x co-ordinate of the pos Vector is set to the world space position x co-ordinate of the player,
    /// the z co-ordinate is set to equal the players z world space position co-ordinate with the subtraction of 8,
    /// the y co-ordinate is set to equal the players y world space position co-ordinate with the addition of the height value.
    /// With the subtraction of 8 and addition of the height value to the z and y co-ordinates,
    /// the pos co-ordinates are off set from the true location of the player and places them in a position where the player is visible from a third person viewpoint.
    /// The world space position of the camera is lastly transformed as a Vector3.SmoothDamp,
    /// which smoothly moves the posistion of the camera to the new position according to the pos co-ordinates,
    /// the velocity variable determining how fast the object is currently moving and the smooth variable determining how quickly the movement to the new position should take.
    /// </summary>
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = -15f;
        pos.y = height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }
}
