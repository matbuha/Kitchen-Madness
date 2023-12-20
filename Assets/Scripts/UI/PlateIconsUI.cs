/*
This script is part of a UI system in a Unity game and manages the visual representation of ingredients on a plate (PlateKitchenObject).
It uses a template (iconTemplate) for creating icon elements dynamically and places them within its own transform.
The script listens for the OnIngredientAdded event from the PlateKitchenObject to update the UI whenever a new ingredient is added.
The UpdateVisual method is called to clear existing icons and create new ones based on the current ingredients on the plate,
displaying them as UI icons.
Each icon is activated and configured with the corresponding KitchenObjectSO (a scriptable object representing a kitchen item).
*/

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Declare a public class 'PlateIconsUI' that inherits from 'MonoBehaviour'
public class PlateIconsUI : MonoBehaviour {

    // Serialized private fields for a reference to PlateKitchenObject and the icon template
    [SerializeField] private PlateKitchenObject plateKitchenObject; // Reference to the PlateKitchenObject to track ingredients
    [SerializeField] private Transform iconTemplate; // Reference to the transform of the icon template

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        iconTemplate.gameObject.SetActive(false); // Initially deactivate the icon template gameObject
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnIngredientAdded event of the plateKitchenObject
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    // Define the event handler method for when an ingredient is added to the plateKitchenObject
    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        UpdateVisual(); // Update the UI to reflect the change
    }

    // Method to update the visual representation of ingredients on the plate
    private void UpdateVisual() {
        // Iterate over each child of this transform and destroy them, except the icon template
        foreach (Transform child in transform) {
            if (child == iconTemplate) continue; // Skip the icon template itself
            Destroy(child.gameObject); // Destroy the child gameObject
        }

        // Instantiate and set up new icons for each kitchen object in the plate
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
            Transform iconTransform = Instantiate(iconTemplate, transform); // Instantiate a new icon
            iconTransform.gameObject.SetActive(true); // Activate the new icon gameObject
            // Set the KitchenObjectSO for the instantiated icon
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
