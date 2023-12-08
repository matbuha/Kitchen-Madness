using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;


    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // There is no KitchenObject here
            if (player.HasKitchenObject()) {
                // player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // Player not carrying anything
            }
        } else {
            // There is KitchenObject here
            if (player.HasKitchenObject()) {
                // Player is carrying something
            } else {
                // Player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // There is a KitchenObject here
            GetKitchenObject().DestroySelf();

            Transform kitchenObjectTransform = Instantiate(cutKitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
    }

}
