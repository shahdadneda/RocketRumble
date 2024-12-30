using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{


    void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("You pushed escape"); // Use Debug.Log to confirm it works in the editor

            // Quit the application
            Application.Quit();




        }
    }
}
