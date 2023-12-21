/*
This script manages a flashing UI animation on a stove in a Unity game to indicate when the contents of the stove are close to burning.
It listens to the OnProgressChanged event from a StoveCounter and uses an animator to control a flashing animation.
The script determines whether to activate this animation based on whether the stove's contents are fried and if the progress exceeds a certain threshold.
The IS_FLASHING constant is used as a parameter name in the animator to control this flashing state.
*/

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'StoveBurnFlashingBarUI' that inherits from 'MonoBehaviour'
public class StoveBurnFlashingBarUI : MonoBehaviour {

    // Define a constant for the animator parameter
    private const string IS_FLASHING = "IsFlashing";

    // Serialized private field for a reference to StoveCounter
    [SerializeField] private StoveCounter stoveCounter; // Reference to the StoveCounter to monitor for burning progress

    // Private field for the animator component
    private Animator animator;

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        animator = GetComponent<Animator>(); // Assign the Animator component attached to this GameObject to the 'animator' field
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnProgressChanged event of the stoveCounter
        stoveCounter.OnProgressChanged += stoveCounter_OnProgressChanged;
        
        // Initially set the IS_FLASHING animator parameter to false
        animator.SetBool(IS_FLASHING, false);
    }

    // Define the event handler method for when the progress of the stove counter changes
    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        // Define a threshold for when the flashing animation should start
        float burnShowProgressAmount = .5f;
        // Determine whether the flashing animation should be shown based on the stove's fried state and progress
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        // Set the IS_FLASHING animator parameter based on the 'show' condition
        animator.SetBool(IS_FLASHING, show);
    }
}
