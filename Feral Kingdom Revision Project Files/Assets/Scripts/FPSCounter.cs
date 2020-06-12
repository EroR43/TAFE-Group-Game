using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Taken From https://forum.unity.com/threads/fps-counter.505495/, the last post on the page from user balcide1
// Last Edited: 06/06/2020

public class FPSCounter : MonoBehaviour
{
    // used to restrict the scene to have only one instance of the fps counter, by using a static variable that will direct back to the original script
    public static FPSCounter instance;
    // identifiers for floats to be used for various things
    public float timer, refresh, avgFramerate;
    // string to define the format of what is displayed in the game on ui element
    string display = "{0} FPS";
    // Field to access the functionality of unity text component, used to display the current fps of the game in the ui.
    private Text m_Text;

    // Checks if this script is already present to destroy copies of the script.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Finds the game object in the scene with the "FPS" and sets the m_Text field to refrence and access the Text component attached to said game object.
    private void Start()
    {
        m_Text = GameObject.FindGameObjectWithTag("FPS").GetComponent<Text>();
    }

    // Displays the FPS of the game in the correct format
    private void Update()
    {
        //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        m_Text.text = string.Format(display, avgFramerate.ToString());
    }
}
