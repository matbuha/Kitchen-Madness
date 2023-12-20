/*
This script manages the pause menu UI in a Unity game.
It sets up listeners for the resume, main menu, and options buttons to handle their respective actions.
The script interacts with KitchenGameManager to respond to pause and unpause events,
showing or hiding the pause menu accordingly.
The Show and Hide methods are used to control the visibility of the pause menu,
and the resumeButton is set to be selected by default when the menu is shown.
*/

// Import necessary namespaces for Unity functionality and UI handling

using UnityEngine;
using UnityEngine.UI;

// Declare a public class 'GamePauseUI' that inherits from 'MonoBehaviour'
public class GamePauseUI : MonoBehaviour {

    // Serialized private fields for UI buttons
    [SerializeField] private Button resumeButton; // A reference to the resume button in the UI
    [SerializeField] private Button mainMenuButton; // A reference to the main menu button in the UI
    [SerializeField] private Button optionsButton; // A reference to the options button in the UI

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        // Add a listener to the resume button to toggle the pause state of the game when clicked
        resumeButton.onClick.AddListener(() => {
            KitchenGameManager.Instance.TogglePauseGame();
        });

        // Add a listener to the main menu button to load the main menu scene when clicked
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        // Add a listener to the options button to hide the current UI and show the options UI when clicked
        optionsButton.onClick.AddListener(() => {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnGamePaused and OnGameUnpaused events of the KitchenGameManager
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        // Initially hide the pause UI
        Hide();
    }

    // Define the event handler method for when the game is unpaused
    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide(); // Hide the pause UI
    }

    // Define the event handler method for when the game is paused
    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e) {
        Show(); // Show the pause UI
    }

    // Method to show the pause UI
    private void Show() {
        gameObject.SetActive(true); // Make the pause UI active and visible

        resumeButton.Select(); // Select the resume button by default
    }

    // Method to hide the pause UI
    private void Hide() {
        gameObject.SetActive(false); // Make the pause UI inactive and invisible
    }
}
