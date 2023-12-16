/*
In this script, the ContainerCounter class extends the functionality of BaseCounter.
The Interact method is overridden to implement specific behavior when a player interacts with the counter.
If the player doesn't currently have a kitchen object, the script spawns a new KitchenObject and then raises an event (OnPlayerGrabbedObject).
This event can be used to trigger other game mechanics or visual effects in response to the interaction.
*/
 

// Import necessary namespaces for Unity functionality and system functions
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'ContainerCounter' that inherits from 'BaseCounter'
public class ContainerCounter : BaseCounter {

    // Declare a public event 'OnPlayerGrabbedObject' of type EventHandler
    public event EventHandler OnPlayerGrabbedObject;

    // Declare a private serialized field 'kitchenObjectSO' of type KitchenObjectSO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Override the 'Interact' method from the BaseCounter class with a parameter of type Player
    public override void Interact(Player player) {
        // Check if the player does not have a kitchen object
        if (!player.HasKitchenObject()) {
            // Player is not carrying anything
            // Call the static method 'SpawnKitchenObject' on KitchenObject, passing 'kitchenObjectSO' and 'player'
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            // Invoke the 'OnPlayerGrabbedObject' event, if it's not null, with 'this' as the sender and an empty EventArgs
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
