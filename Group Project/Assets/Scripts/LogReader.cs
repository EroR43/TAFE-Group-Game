using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Eric Hanks
//Made with help and code from https://www.youtube.com/playlist?list=PLy7lD4g7kKGDHdKhlWtZQ_8nGfw6z6yPH made by Immergo Media, AKA Josh.
//Last Edited 16/04/2020

public class LogReader : MonoBehaviour
{
    /// <summary>
    /// allows us to make use of the TextFileManager class's functions to read, write and create new txt files.
    /// </summary>
    public TextFileManager fileManager = new TextFileManager();


    // This start function runs the fileManagers Start function.
    void Start()
    {
        fileManager.Start();
    }

    /// <summary>
    /// runs the fileManager's AddKeyValuePair function with the parameters of fileManager.logName, key and value,
    /// key and value being parameters that were passed to the parent function.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SaveKeyValuePair(string key, string value)
    {
        fileManager.AddKeyValuePair(fileManager.logName, key, value);
    }

    /// <summary>
    /// returns the fileManager's result for the LocateStringByKey function with a parameter of key,
    /// which was originally passed to the parent function as a parameter.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string LoadStringByKey(string key)
    {
        return fileManager.LocateStringByKey(key);
    }

    /// <summary>
    /// sets a new local float of 'f' to equal 0,
    /// then sets f to equal the result of a TryParse of the fileManager's LocateStringByKey function with a parameter of key,
    /// which was originally passed to the parent function as a parameter.
    /// it then returns what 'f' refrences as a float value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public float LoadFloatByKey(string key)
    {
        float f = 0;
        float.TryParse(fileManager.LocateStringByKey(key), out f);
        return f;
    }

    /// <summary>
    /// sets a new local integer of 'i' to equal 0,
    /// then sets 'i' to equal the result of a TryParse of the fileManager's LocateStringByKey function with a parameter of key,
    /// which was originally passed to the parent function as a parameter.
    /// it then returns what 'i' refrences as a integer value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int LoadIntByKey(string key)
    {
        int i = 0;
        int.TryParse(fileManager.LocateStringByKey(key), out i);
        return i;
    }

    /// <summary>
    /// sets a new local string of 'v' to be the result of the fileManager's LocateStringByKey function with a parameter of key,
    /// which was originally passed to the parent function as a parameter,
    /// it then preforms a check in the form of an if statement for whether 'v' equals 'True' or 'true', capitialisation being important,
    /// and returning true.
    /// Else, if 'v' does not equal the variations of true, the bool will return false.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool LoadBoolByKey(string key)
    {
        string v = fileManager.LocateStringByKey(key);
        if (v == "True" || v == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
