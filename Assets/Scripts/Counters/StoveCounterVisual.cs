/*
This script is designed to visually represent the state of a stove in a Unity game.
It listens to state changes in a StoveCounter object and updates the visibility of two GameObjects (stoveOnGameObject and particlesGameObject) accordingly.
When the stove is in the process of frying or has finished frying (states 'Frying' or 'Fried'),
the script activates these visual elements to reflect the state of the stove in the game.
*/
 

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Declare a public class 'StoveCounterVisual' that inherits from 'MonoBehaviour'
public class StoveCounterVisual : MonoBehaviour {

    // Declare private serialized fields for the StoveCounter and two GameObjects: one for the stove on visual and another for particles
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the 'OnStateChanged' event of 'stoveCounter'
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    // Define the event handler method for when the state of the stove counter changes
    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        // Determine whether to show the stove visual and particles based on the stove state being Frying or Fried
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        
        // Set the active state of 'stoveOnGameObject' based on 'showVisual'
        stoveOnGameObject.SetActive(showVisual);
        // Set the active state of 'particlesGameObject' based on 'showVisual'
        particlesGameObject.SetActive(showVisual);
    }
}
