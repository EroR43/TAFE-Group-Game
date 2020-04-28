using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Author: Eric Hanks
//Made with help and code from https://www.youtube.com/playlist?list=PLy7lD4g7kKGDHdKhlWtZQ_8nGfw6z6yPH made by Immergo Media, AKA Josh.
//Last Edited 16/04/2020

[System.Serializable]
public class TextFileManager
{
    /// <summary>
    /// A string for use in setting and getting the name of the text file we are utilising
    /// </summary>
    public string logName;

    /// <summary>
    /// This string array is used to refrence and output the contents of a text file
    /// </summary>
    public string[] logContents;

    /// <summary>
    /// Uses a local string parameter so the function can be passed a file name to be used when creating a new text file,
    /// the process of creating the new file consists of checking if the file that the dirPath string refrences,
    /// the dirPath being the file within the Resources folder within the game data folder that is the same as the fileName parameter,
    /// creating a new directory of Resources within the game data folder and writing the fileName parameter as the first line of a new txt file.
    /// </summary>
    /// <param name="fileName"></param>
    public void CreateFile(string fileName)
    {
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        if (File.Exists(dirPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources");
            File.WriteAllText(dirPath, fileName + "\n");

        }
    }

    /// <summary>
    /// Sets the dirPath to be the fileName that the function is passed within the Resources folder,
    /// makes a new string array of tContents and sets that to represent all of the lines of data present in the dirPath txt file if the dirPath file exists.
    /// It then sets logContents to represent tContents and return the tContents lines of data when the function ends.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public string[] ReadFileContents(string fileName)
    {
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        string[] tContents = new string[0];
        if (File.Exists(dirPath) == true)
        {
            tContents = File.ReadAllLines(dirPath);

        }
        logContents = tContents;
        return tContents;
    }

    /// <summary>
    /// Runs the ReadFileContents function with the fileName parameter that this function recieves,
    /// sets the dirPath to be the txt file with the name that the function recieves through the fileName parameter,
    /// sets tContents to be the fileContents parameter that this function is passed with a new line in the txt file,
    /// sets the timeStamp local string to be "Time Stamp: " with the systems current date and time,
    /// checks if the dirPath file exists and if it does,
    /// appends the timeStamp a '-' symbol and the tContents variable.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileContents"></param>
    public void AddFileLine(string fileName, string fileContents)
    {
        ReadFileContents(fileName);
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        string tContents = fileContents + "\n";
        string timeStamp = "Time Stamp: " + System.DateTime.Now;
        if (File.Exists(dirPath) == true)
        {
            File.AppendAllText(dirPath, timeStamp + " - " + tContents);
        }
    }

    /// <summary>
    /// Runs the ReadFileContents function with the fileName parameter that this function recieves,
    /// sets the dirPath to be the txt file with the name that the function recieves through the fileName parameter,
    /// sets the tContents string to be the local 'key' string that this function is passed as a parameter
    /// with a ',' symbol at the end and the value string that this function is passed as a parameter.
    /// sets the timeStamp local string to be "Time Stamp: " with the systems current date and time.
    /// If the dirPath exists, this function will set the local bool of contents to be false,
    /// for as long as the local i integer's value is less than the length of the logContents, 
    /// logContents will be checked to contain the 'key' string parameter that this function is passed as a parameter and if it does,
    /// will set logContents to represent the timeStamp with a '-' symbol and tContents appended onto the end of it,
    /// and set contentsFound to true.
    /// After the for loop has run, another if statement checks if contentsFound equals to true,
    /// writing all of logContents to the file that dirPath is refrencing,
    /// else if contentsFound equals false,
    /// it will append all the text of timeStamp, a '-' symbol and tContents to the file that the dirPath is refrencing.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddKeyValuePair(string fileName, string key, string value)
    {
        ReadFileContents(fileName);
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        string tContents = key + "," + value;
        string timeStamp = "Time Stamp: " + System.DateTime.Now;
        if (File.Exists(dirPath) == true)
        {
            bool contentsFound = false;
            for (int i = 0; i < logContents.Length; i++)
            {
                if (logContents[i].Contains(key) == true)
                {
                    logContents[i] = timeStamp + " - " + tContents;
                    contentsFound = true;
                }
            }

            if (contentsFound == true)
            {
                File.WriteAllLines(dirPath, logContents);
            }
            else
            {
                File.AppendAllText(dirPath, timeStamp + " - " + tContents + "\n");
            }
        }
    }

    /// <summary>
    /// runs the ReadFileContents function with the logName parameter to read all of the data stored in the Score Log file,
    /// sets the local string of t to return nothing if its called,
    /// starts a foreach loop for the local string of s in the logContents that checks if the s string contains the key string parameter that this function is passed,
    /// setting a new string array of splitString to refrence the split string array of 's',
    /// then setting t to refrence the splitString with an array of splitString length - 1.
    /// The function finally returns the value of 't' when it finishs executing.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string LocateStringByKey(string key)
    {
        ReadFileContents(logName);
        string t = "";
        foreach (string s in logContents)
        {
            if (s.Contains(key) == true)
            {
                string[] splitString = s.Split(",".ToCharArray());
                t = splitString[splitString.Length - 1];
            }
        }
        return t;
    }

    // This start function runs the CreatFile and ReadFileContents functions with parameters of logName to create the new logName.txt or read the contents of the existing logName.txt
    public void Start()
    {
        CreateFile(logName);
        ReadFileContents(logName);
    }
    
}