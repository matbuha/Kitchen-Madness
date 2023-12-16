/*
This script is designed to invoke a callback method (Loader.LoaderCallback()) once,
during the first frame after this script becomes active.
It uses a boolean flag (isFirstUpdate) to ensure that the callback is triggered only once in the Update method,
which is normally called every frame.
This kind of implementation is typically used for initialization purposes or to trigger events that should only happen once after a certain condition is met,
such as the completion of a loading process.
*/

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'LoaderCallback' that inherits from 'MonoBehaviour'
public class LoaderCallback : MonoBehaviour {

    // Private boolean field to track if it's the first frame since the script was enabled
    private bool isFirstUpdate = true;

    // Define the Update method which is called every frame
    private void Update() {
        if (isFirstUpdate) { // Check if this is the first frame since the script was enabled
            isFirstUpdate = false; // Set isFirstUpdate to false to ensure this block only runs once

            // Call the LoaderCallback method of the Loader class
            Loader.LoaderCallback();
        }
    }
}
