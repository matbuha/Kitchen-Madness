using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClearCounter : BaseCounter {


    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player) {

        if (!HasKitchenObject()) {

            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {

            }
        } else {
            // If there is a KitchenObject on this counter.
            if (player.HasKitchenObject()) {
                // If the player is carrying a KitchenObject.
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // Player is not carrying plate but somthing else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                // If the player is not carrying anything.
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}