/*
This script manages the delivery result UI in a Unity game.
It displays different visual and textual feedback based on whether a recipe delivery is successful or failed.
The script subscribes to the OnRecipeSuccess and OnRecipeFailed events from the DeliveryManager to determine the result of a delivery.
When a delivery result is received, it activates the UI,
sets the appropriate colors and sprites for success or failure,
triggers a popup animation, and updates the message text to reflect the outcome of the delivery.
*/

// Import necessary namespaces for Unity functionality, UI handling, and text handling

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Declare a public class 'DeliveryResultUI' that inherits from 'MonoBehaviour'
public class DeliveryResultUI : MonoBehaviour {

    // Define a constant for the animation trigger
    private const string POPUP = "Popup";

    // Serialized private fields for UI components
    [SerializeField] private Image backgroundImage; // Reference to the UI Image for the background
    [SerializeField] private Image iconImage; // Reference to the UI Image for the icon
    [SerializeField] private TextMeshProUGUI messageText; // Reference to the TextMeshProUGUI for displaying messages
    [SerializeField] private Color successColor; // Color to be used for successful delivery
    [SerializeField] private Color failedColor; // Color to be used for failed delivery
    [SerializeField] private Sprite successSprite; // Sprite to be used for successful delivery
    [SerializeField] private Sprite failedSprite; // Sprite to be used for failed delivery

    // Private field for the animator component
    private Animator animator;

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        animator = GetComponent<Animator>(); // Assign the Animator component attached to this GameObject to the 'animator' field
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnRecipeSuccess and OnRecipeFailed events of the DeliveryManager
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        // Initially deactivate the gameObject
        gameObject.SetActive(false);
    }

    // Define the event handler method for when a recipe delivery fails
    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e) {
        gameObject.SetActive(true); // Activate the gameObject
        animator.SetTrigger(POPUP); // Trigger the popup animation
        backgroundImage.color = failedColor; // Set the background color to indicate failure
        iconImage.sprite = failedSprite; // Set the icon to the failed sprite
        messageText.text = "DELIVERY\nFAILED"; // Update the message text to indicate delivery failure
    }

    // Define the event handler method for when a recipe delivery succeeds
    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
        gameObject.SetActive(true); // Activate the gameObject
        animator.SetTrigger(POPUP); // Trigger the popup animation
        backgroundImage.color = successColor; // Set the background color to indicate success
        iconImage.sprite = successSprite; // Set the icon to the success sprite
        messageText.text = "DELIVERY\nSUCCESS"; // Update the message text to indicate delivery success
    }
}
