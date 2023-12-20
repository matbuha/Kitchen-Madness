/*
This script manages the UI representation of recipes for the DeliveryManager in a Unity game.
It creates and updates visual elements for each recipe that is waiting to be completed.
The script uses a template (recipeTemplate) for creating UI elements dynamically and places them inside a container (container).
When recipes are spawned or completed (OnRecipeSpawned and OnRecipeCompleted events),
the script updates the UI to reflect these changes by destroying old elements and creating new ones based on the current state of the DeliveryManager.
*/

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Declare a public class 'DeliveryManagerUI' that inherits from 'MonoBehaviour'
public class DeliveryManagerUI : MonoBehaviour {

    // Serialized private fields for UI components
    [SerializeField] private Transform container; // A reference to the container transform where recipe UIs will be placed
    [SerializeField] private Transform recipeTemplate; // A reference to the transform of the recipe UI template

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        recipeTemplate.gameObject.SetActive(false); // Initially deactivate the recipe template gameObject
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnRecipeSpawned and OnRecipeCompleted events of the DeliveryManager
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

        // Update the UI to reflect the current state of the DeliveryManager
        UpdateVisual();
    }

    // Define the event handler method for when a recipe is completed in the DeliveryManager
    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e) {
        UpdateVisual(); // Update the UI to reflect the change
    }

    // Define the event handler method for when a recipe is spawned in the DeliveryManager
    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e) {
        UpdateVisual(); // Update the UI to reflect the change
    }

    // Method to update the visual representation of recipes
    private void UpdateVisual() {
        // Iterate over each child of the container and destroy them, except the recipe template
        foreach (Transform child in container) {
            if (child == recipeTemplate) continue; // Skip the recipe template itself
            Destroy(child.gameObject); // Destroy the child gameObject
        }

        // Instantiate and set up new recipe UIs for each recipe in the waiting list
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()) {
            Transform recipeTransform = Instantiate(recipeTemplate, container); // Instantiate a new recipe UI
            recipeTransform.gameObject.SetActive(true); // Activate the new recipe UI gameObject
            // Set the RecipeSO for the instantiated UI
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
