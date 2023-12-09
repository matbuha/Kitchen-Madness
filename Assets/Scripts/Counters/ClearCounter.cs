// Importing necessary libraries and namespaces for the script.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declaring the ClearCounter class, which inherits from BaseCounter.
public class ClearCounter : BaseCounter {

    // A private serialized field of type KitchenObjectSO, likely representing a scriptable object related to a kitchen object.
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Overriding the 'Interact' method from the BaseCounter class. This method is called when interaction occurs.
    public override void Interact(Player player) {
        // Checking if there is no KitchenObject on this counter.
        if (!HasKitchenObject()) {
            // If there is no KitchenObject on this counter.
            if (player.HasKitchenObject()) {
                // If the player is carrying a KitchenObject.
                // Set this counter as the new parent of the KitchenObject the player is carrying.
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // If the player is not carrying anything.
                // This block is empty, so nothing happens if the player has nothing.
            }
        } else {
            // If there is a KitchenObject on this counter.
            if (player.HasKitchenObject()) {
                // If the player is carrying a KitchenObject.
                // This block is empty, so nothing happens if both the counter and the player have a KitchenObject.
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
            } else {
                // If the player is not carrying anything.
                // Set the player as the new parent of the KitchenObject on this counter.
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}