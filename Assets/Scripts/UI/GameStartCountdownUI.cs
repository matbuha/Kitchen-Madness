/*
This script manages the game start countdown UI in a Unity game.
It listens to state changes from the KitchenGameManager to determine when to display a countdown to the game start.
The script updates the countdown number in real-time and triggers a popup animation and sound effect whenever the countdown number changes.
The Show and Hide methods control the visibility of the countdown UI.
*/

// Import necessary namespaces for Unity functionality and text handling
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Declare a public class 'GameStartCountdownUI' that inherits from 'MonoBehaviour'
public class GameStartCountdownUI : MonoBehaviour {

    // Define a constant for the animation trigger
    private const string NUMBER_POPUP = "NumberPopup";

    // Serialized private field for a TextMeshProUGUI component to display countdown numbers
    [SerializeField] private TextMeshProUGUI countdownText; // Reference to the UI text element showing the countdown numbers

    // Private fields for the animator component and to track the previous countdown number
    private Animator animator;
    private int previusCountdownNumber;

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        animator = GetComponent<Animator>(); // Assign the Animator component attached to this GameObject to the 'animator' field
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnStateChanged event of the KitchenGameManager
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        // Initially hide the countdown UI
        Hide();
    }

    // Define the event handler method for when the state of the KitchenGameManager changes
    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        // Check if the countdown to start the game is active
        if (KitchenGameManager.Instance.IsCountdownToStartActive()) {
            Show(); // Show the countdown UI
        } else {
            Hide(); // Hide the countdown UI
        }
    }

    // Define the Update method which is called every frame
    private void Update() {
        // Get the current countdown number from the KitchenGameManager
        int countdownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString(); // Update the UI text to show the current countdown number

        // Check if the countdown number has changed since the last frame
        if (previusCountdownNumber != countdownNumber) {
            previusCountdownNumber = countdownNumber; // Update the previous countdown number
            animator.SetTrigger(NUMBER_POPUP); // Trigger the number popup animation
            SoundManager.Instance.PlayCountdownSound(); // Play the countdown sound
        }
    }

    // Method to show the countdown UI
    private void Show() {
        gameObject.SetActive(true); // Make the countdown UI active and visible
    }

    // Method to hide the countdown UI
    private void Hide() {
        gameObject.SetActive(false); // Make the countdown UI inactive and invisible
    }
}
