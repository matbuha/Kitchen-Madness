/*
This script manages the visual representation of a selected counter in a Unity game,
particularly in a kitchen-themed game setting.
It controls an array of GameObjects that visually indicate when a particular BaseCounter is selected by the player.
The script listens to an event from the Player class (OnSelectedCounterChanged),
and shows or hides its visual elements based on whether its associated BaseCounter is the one currently selected by the player.
The Show and Hide methods activate and deactivate these visual elements, respectively.
*/

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Declare a public class 'SelectedCounterVisual' that inherits from 'MonoBehaviour'
public class SelectedCounterVisual : MonoBehaviour {

    // Serialized private fields for a reference to BaseCounter and an array of visual GameObjects
    [SerializeField] private BaseCounter baseCounter; // Reference to the BaseCounter this script is related to
    [SerializeField] private GameObject[] visualGameObjectArray; // Array of GameObjects that represent the visual elements

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnSelectedCounterChanged event of the Player
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    // Define the event handler method for when the selected counter changes in the Player class
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        // Check if the selected counter is the same as the baseCounter referenced in this script
        if (e.selectedCounter == baseCounter) {
            Show(); // Show the visual elements if the selected counter matches
        } else {
            Hide(); // Hide the visual elements if the selected counter does not match
        }
    }

    // Method to show all visual GameObjects in the array
    private void Show() {
        foreach (GameObject visualGameObject in visualGameObjectArray) {
            visualGameObject.SetActive(true); // Activate each visual GameObject
        }
    }

    // Method to hide all visual GameObjects in the array
    private void Hide() {
        foreach (GameObject visualGameObject in visualGameObjectArray) {
            visualGameObject.SetActive(false); // Deactivate each visual GameObject
        }
    }
}
