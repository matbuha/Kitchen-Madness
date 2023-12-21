/*
This script is used to reset static data in different classes when a new instance of RestStaticDataManager is created (usually at the start of a scene or a game).
The Awake method is called as the script is loaded,
and it calls the ResetStaticData methods on the CuttingCounter, BaseCounter, and TrashCounter classes.
This pattern is typically used to ensure that static fields or events in these classes are reset to their default states,
which is especially important in scenarios like restarting a level or loading a new scene,
to avoid carrying over previous state information or event subscriptions.
*/

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'RestStaticDataManager' that inherits from 'MonoBehaviour'
public class RestStaticDataManager : MonoBehaviour {

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        // Call the ResetStaticData method on the CuttingCounter class
        CuttingCounter.ResetStaticData();

        // Call the ResetStaticData method on the BaseCounter class
        BaseCounter.ResetStaticData();

        // Call the ResetStaticData method on the TrashCounter class
        TrashCounter.ResetStaticData();
    }
}
