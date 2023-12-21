/*
This script extends the functionality of BaseCounter to add specific interactions for a ClearCounter in a Unity game,
likely involving gameplay mechanics around kitchen objects, plates, and ingredients.
The Interact method handles various scenarios based on whether the player or the counter has a kitchen object and what type of object it is (like a plate).
*/
// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'ClearCounter' that inherits from 'BaseCounter'
public class ClearCounter : BaseCounter {

    // Declare a private serialized field 'kitchenObjectSO' of type KitchenObjectSO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Override the 'Interact' method from the BaseCounter class with a parameter of type Player
    public override void Interact(Player player) {

        // Check if the counter does not have a kitchen object
        if (!HasKitchenObject()) {

            // Check if the player has a kitchen object
            if (player.HasKitchenObject()) {
                // Set the player's kitchen object's parent to this counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // Empty else clause for future implementation or logic
            }
        } else {
            // If there is a KitchenObject on this counter
            if (player.HasKitchenObject()) {
                // If the player is carrying a KitchenObject
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        // Try adding ingredient to the plate; if successful, destroy the KitchenObject on the counter
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // Player is not carrying a plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // The counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            // Try adding the player's kitchen object as an ingredient to the plate; if successful, destroy the player's kitchen object
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                // If the player is not carrying anything
                // Set the counter's kitchen object's parent to the player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
