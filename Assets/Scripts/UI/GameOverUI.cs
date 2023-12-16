/*
This script manages the game over UI in a Unity game.
It sets up a listener for the OnStateChanged event from the KitchenGameManager to determine when the game is over.
When the game ends, the script shows the game over UI and updates a text element (recipesDeliveredText) to display the number of successfully delivered recipes,
using information from the DeliveryManager.
The Show and Hide methods control the visibility of the game over UI.
*/

// Import necessary namespaces for Unity functionality and text handling
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Declare a public class 'GameOverUI' that inherits from 'MonoBehaviour'
public class GameOverUI : MonoBehaviour {

    // Serialized private field for a TextMeshProUGUI component to display recipes delivered information
    [SerializeField] private TextMeshProUGUI recipesDeliveredText; // A reference to the UI text element showing the number of successfully delivered recipes

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnStateChanged event of the KitchenGameManager
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        // Initially hide the game over UI
        Hide();
    }

    // Define the event handler method for when the state of the KitchenGameManager changes
    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        // Check if the game is over
        if (KitchenGameManager.Instance.IsGameOver()) {
            Show(); // Show the game over UI

            // Update the text to display the number of successfully delivered recipes
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        } else {
            Hide(); // Hide the game over UI if the game is not over
        }
    }

    // Method to show the game over UI
    private void Show() {
        gameObject.SetActive(true); // Make the game over UI active and visible
    }

    // Method to hide the game over UI
    private void Hide() {
        gameObject.SetActive(false); // Make the game over UI inactive and invisible
    }
}
