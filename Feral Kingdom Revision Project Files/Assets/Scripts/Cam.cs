using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Eric Hanks
// Code from: https://www.youtube.com/playlist?list=PLMKGE-XmGjMTzNpssH84xJcQ35CXJeLLl by Sykoo
// Last Edited: 30/04/2020

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
    /// Updates the cameras position when the player moves and applys offsets and smoothing
    /// </summary>
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z + -8f;
        pos.y = height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }
}
