/*
This script manages a progress bar UI in a Unity game.
It is designed to work with a GameObject that has a component implementing the IHasProgress interface.
The script updates a UI bar (barImage) to reflect the progress reported by this component.
It subscribes to the OnProgressChanged event to update the UI in real-time as the progress changes.
The script also includes functionality to show or hide the progress bar based on the progress state (starting,
ongoing, or completed).
*/

// Import necessary namespaces for Unity functionality and UI handling
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Declare a public class 'ProgressBarUI' that inherits from 'MonoBehaviour'
public class ProgressBarUI : MonoBehaviour {

    // Serialized private fields for the GameObject that has a component implementing IHasProgress, and for the UI bar image
    [SerializeField] private GameObject hasProgressGameObject; // Reference to the GameObject that has a progress component
    [SerializeField] private Image barImage; // Reference to the Image component used to display the progress bar

    // Private field to store the IHasProgress interface
    private IHasProgress hasProgress;

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Attempt to get the IHasProgress component from the hasProgressGameObject
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null) {
            // Log an error if the component is not found
            Debug.LogError("GameObject " + hasProgressGameObject + " does not have a component that implements IHasProgress!");
        }

        // Subscribe to the OnProgressChanged event of the hasProgress component
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        // Initially set the fill amount of the progress bar to 0 (empty)
        barImage.fillAmount = 0f;

        // Initially hide the progress bar UI
        Hide();
    }

    // Define the event handler method for when the progress changes
    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        // Set the fill amount of the progress bar based on the normalized progress value
        barImage.fillAmount = e.progressNormalized;

        // Show or hide the progress bar based on whether the progress is at the start or end
        if (e.progressNormalized == 0f || e.progressNormalized == 1f) {
            Hide(); // Hide the progress bar if progress is complete or not started
        } else {
            Show(); // Show the progress bar if progress is ongoing
        }
    }

    // Method to show the progress bar UI
    private void Show() {
        gameObject.SetActive(true); // Make the progress bar UI active and visible
    }

    // Method to hide the progress bar UI
    private void Hide() {
        gameObject.SetActive(false); // Make the progress bar UI inactive and invisible
    }
}
