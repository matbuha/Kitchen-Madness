/*
This script is part of a UI system in a Unity game and manages the visual representation of a single recipe in the DeliveryManager.
It uses a template (iconTemplate) for creating icon elements dynamically and places them inside a container (iconContainer).
The SetRecipeSO method is called to update the UI with the details of a given recipe,
setting the name of the recipe and creating icons for each kitchen object involved in that recipe.
The icons are set to be visible and their sprites are updated to reflect the corresponding kitchen objects.
*/

// Import necessary namespaces for Unity functionality, UI handling, and text handling

using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Declare a public class 'DeliveryManagerSingleUI' that inherits from 'MonoBehaviour'
public class DeliveryManagerSingleUI : MonoBehaviour {

    // Serialized private fields for UI components
    [SerializeField] private TextMeshProUGUI recipeNameText; // Reference to the TextMeshProUGUI for displaying the recipe name
    [SerializeField] private Transform iconContainer; // Reference to the container transform where icons will be placed
    [SerializeField] private Transform iconTemplate; // Reference to the transform of the icon template

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        iconTemplate.gameObject.SetActive(false); // Initially deactivate the icon template gameObject
    }

    // Public method to set the recipe scriptable object and update the UI accordingly
    public void SetRecipeSO(RecipeSO recipeSO) {
        recipeNameText.text = recipeSO.recipeName; // Set the recipe name in the UI

        // Iterate over each child of the icon container and destroy them, except the icon template
        foreach (Transform child in iconContainer) {
            if (child == iconTemplate) continue; // Skip the icon template itself
            Destroy(child.gameObject); // Destroy the child gameObject
        }

        // Instantiate and set up new icons for each kitchen object in the recipe
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList) {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer); // Instantiate a new icon
            iconTransform.gameObject.SetActive(true); // Activate the new icon gameObject
            // Set the sprite of the instantiated icon to the sprite of the kitchen object
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
