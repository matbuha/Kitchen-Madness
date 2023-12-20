/*
This script extends the KitchenObject class to specifically represent a plate in a kitchen-themed game.
The PlateKitchenObject class manages a list of ingredients (represented by KitchenObjectSO objects) that can be placed on the plate.
It includes a method TryAddIngredient to add ingredients to the plate,
checking if they are valid and not already present.
An event OnIngredientAdded is invoked when a new ingredient is successfully added.
The class also provides a method GetKitchenObjectSOList to retrieve the current list of ingredients on the plate.
The script is designed to manage the composition of a plate in a cooking or kitchen simulation context.
*/

// Import necessary namespaces for Unity functionality
using System;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'PlateKitchenObject' that inherits from 'KitchenObject'
public class PlateKitchenObject : KitchenObject {

    // Event to notify when an ingredient is added to the plate
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO; // The kitchen object scriptable object that has been added
    }

    // Serialized private field for a list of valid ingredients that can be added to the plate
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList; // List of valid KitchenObjectSOs that can be added to the plate

    // Private field to store the list of ingredients currently on the plate
    private List<KitchenObjectSO> kitchenObjectSOList; // List of KitchenObjectSOs currently on the plate

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        // Initialize the kitchenObjectSOList
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    // Method to try adding an ingredient to the plate
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        // Check if the ingredient is valid and not already on the plate
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            // Not a valid ingredient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            // Already has this type of ingredient
            return false;
        } else {
            // Add the ingredient to the plate
            kitchenObjectSOList.Add(kitchenObjectSO);

            // Invoke the OnIngredientAdded event
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });

            // Ingredient successfully added
            return true;
        }
    }

    // Method to get the list of ingredients currently on the plate
    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList; // Return the list of KitchenObjectSOs on the plate
    }
}
