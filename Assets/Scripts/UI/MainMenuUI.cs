/*
This script manages the main menu UI in a Unity game.
It sets up listeners for the play and quit buttons to handle their respective actions.
When the play button is clicked, it triggers the loading of a specified game level (Level 1 in this case).
When the quit button is clicked, it triggers the application to quit.
The script also ensures that the time scale is set to normal (1f),
which is especially relevant if coming back to the main menu from a paused state in the game.
*/

// Import necessary namespaces for Unity functionality, scene management, and UI handling

using UnityEngine;
using UnityEngine.UI;

// Declare a public class 'MainMenuUI' that inherits from 'MonoBehaviour'
public class MainMenuUI : MonoBehaviour {

    // Serialized private fields for UI buttons
    [SerializeField] private Button playButton; // Reference to the play button in the UI
    [SerializeField] private Button quitButton; // Reference to the quit button in the UI

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        // Add a listener to the play button to load the Level 1 scene when clicked
        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Level1);
        });

        // Add a listener to the quit button to quit the application when clicked
        quitButton.onClick.AddListener(() => {
            Application.Quit(); // Quit the application
        });

        // Set the time scale to 1, ensuring that the game runs at normal speed
        Time.timeScale = 1f;
    }
}
