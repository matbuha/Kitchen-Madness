/*
This script is attached to a visual component in a Unity game that represents a complete plate,
particularly in a cooking or kitchen environment.
It manages visual representations (GameObjects) of different kitchen objects (specified as KitchenObjectSO) that can be added to a plate (PlateKitchenObject).
The script listens for an OnIngredientAdded event from the PlateKitchenObject and activates the corresponding GameObject for each ingredient added.
The KitchenObjectSO_GameObject struct is used to create a mapping between KitchenObjectSO items and their visual representations in the game.
This approach allows for a dynamic visual representation of a plate's composition based on the ingredients added to it.
*/

// Import necessary namespaces for Unity functionality
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'PlateCompleteVisual' that inherits from 'MonoBehaviour'
public class PlateCompleteVisual : MonoBehaviour {

    // Define a serializable struct to link KitchenObjectSOs with their corresponding GameObjects
    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO; // Reference to the KitchenObjectSO
        public GameObject gameObject; // The GameObject that visually represents the KitchenObjectSO
    }

    // Serialized private fields for a reference to PlateKitchenObject and a list of KitchenObjectSO to GameObject mappings
    [SerializeField] private PlateKitchenObject plateKitchenObject; // Reference to the PlateKitchenObject component
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList; // List of KitchenObjectSO and corresponding GameObjects

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnIngredientAdded event of the plateKitchenObject
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        // Initially deactivate all GameObjects in the kitchenObjectSOGameObjectList
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList) {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    // Event handler method for when an ingredient is added to the plate
    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        // Activate the corresponding GameObject for the added KitchenObjectSO
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList) {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO) {
                kitchenObjectSOGameObject.gameObject.SetActive(true); // Activate the GameObject
            }
        }
    }
}
