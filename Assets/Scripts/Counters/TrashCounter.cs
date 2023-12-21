/*
In this script, the TrashCounter class extends the functionality of BaseCounter.
The Interact method is overridden to implement specific behavior when a player interacts with the trash counter.
If the player has a kitchen object, the script destroys it and then raises an event (OnAnyObjectTrashed).
This event can be used to trigger other game mechanics or visual effects in response to the trashing action.
The ResetStaticData method is also overridden to ensure that the static event is reset properly,
maintaining the integrity of the event management in the game.
*/
 

// Import necessary namespaces for Unity functionality and system functions
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'TrashCounter' that inherits from 'BaseCounter'
public class TrashCounter : BaseCounter {

    // Declare a public static event 'OnAnyObjectTrashed' of type EventHandler
    public static event EventHandler OnAnyObjectTrashed;

    // Declare a new public static method 'ResetStaticData' to override the base class method
    new public static void ResetStaticData() {
        OnAnyObjectTrashed = null; // Set the static event to null
    }

    // Override the 'Interact' method from the BaseCounter class with a parameter of type Player
    public override void Interact(Player player) {
        // Check if the player has a kitchen object
        if (player.HasKitchenObject()) {
            // Destroy the kitchen object that the player is holding
            player.GetKitchenObject().DestroySelf();

            // Invoke the 'OnAnyObjectTrashed' event, if it's not null, with 'this' as the sender and an empty EventArgs
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
