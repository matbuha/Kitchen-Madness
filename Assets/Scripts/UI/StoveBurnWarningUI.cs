/*
This script manages the display of a burn warning UI in a Unity game,
specifically in relation to a stove counter.
It listens to the OnProgressChanged event from a StoveCounter to determine when the stove's contents are at risk of burning.
The script shows the warning UI when the progress of the stove counter exceeds a certain threshold and hides it otherwise.
The Show and Hide methods control the visibility of this warning UI.
*/

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Declare a public class 'StoveBurnWarningUI' that inherits from 'MonoBehaviour'
public class StoveBurnWarningUI : MonoBehaviour {

    // Serialized private field for a reference to StoveCounter
    [SerializeField] private StoveCounter stoveCounter; // Reference to the StoveCounter to monitor for burning warning

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnProgressChanged event of the stoveCounter
        stoveCounter.OnProgressChanged += stoveCounter_OnProgressChanged;

        // Initially hide the warning UI
        Hide();
    }

    // Define the event handler method for when the progress of the stove counter changes
    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        // Define a threshold for when to show the burn warning
        float burnShowProgressAmount = .5f;
        // Determine whether to show the warning UI based on the stove's fried state and progress
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        if (show) {
            Show(); // Show the warning UI
        } else {
            Hide(); // Hide the warning UI
        }
    }

    // Method to show the burn warning UI
    private void Show() {
        gameObject.SetActive(true); // Make the warning UI active and visible
    }

    // Method to hide the burn warning UI
    private void Hide() {
        gameObject.SetActive(false); // Make the warning UI inactive and invisible
    }
}
