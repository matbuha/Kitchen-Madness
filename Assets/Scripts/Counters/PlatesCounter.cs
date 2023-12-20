/*
This script sets up a PlatesCounter class in a Unity game, extending the functionality of BaseCounter.
It manages the spawning and interaction with plates.
The Update method handles the automatic spawning of plates at specific intervals,
while the Interact method handles player interactions,
allowing players to pick up plates if they don't already have a kitchen object and if plates are available.
Events are raised for both spawning and removing plates,
which can be used for other game mechanics or visual feedback.
*/
 

// Import necessary namespaces for Unity functionality and system functions

using System;
using UnityEngine;

// Declare a public class 'PlatesCounter' that inherits from 'BaseCounter'
public class PlatesCounter : BaseCounter {

    // Declare two public events of type EventHandler, 'OnPlateSpawned' and 'OnPlateRemoved'
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    // Declare a private serialized field 'plateKitchenObjectSO' of type KitchenObjectSO
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    // Declare a private field 'spawnPlateTimer' to keep track of the timer for spawning plates
    private float spawnPlateTimer;
    // Initialize 'spawnPlateTimerMax' to 4 seconds, setting the maximum time between plate spawns
    private float spawnPlateTimerMax = 4f;

    // Declare a private field 'platesSpawnedAmount' to keep track of the number of plates spawned
    private int platesSpawnedAmount;
    // Initialize 'platesSpawnedAmountMax' to 4, setting the maximum number of plates that can be spawned
    private int platesSpawnedAmountMax = 4;

    // Define the Update method which is called every frame
    private void Update() {
        spawnPlateTimer += Time.deltaTime; // Increment the timer by the time elapsed since the last frame
        // Check if the timer exceeds the maximum time allowed for spawning a plate
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f; // Reset the timer

            // Check if the number of plates spawned is less than the maximum allowed
            if (platesSpawnedAmount < platesSpawnedAmountMax) {
                platesSpawnedAmount++; // Increment the number of plates spawned

                // Invoke the 'OnPlateSpawned' event, if it's not null, with 'this' as the sender and an empty EventArgs
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    // Override the 'Interact' method from the BaseCounter class with a parameter of type Player
    public override void Interact(Player player) {
        // Check if the player does not have a kitchen object
        if (!player.HasKitchenObject()) {
            // Player is empty handed
            // Check if there is at least one plate available
            if (platesSpawnedAmount > 0) {
                // Decrement the number of plates available
                platesSpawnedAmount--;

                // Call the static method 'SpawnKitchenObject' on KitchenObject, passing 'plateKitchenObjectSO' and 'player'
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                // Invoke the 'OnPlateRemoved' event, if it's not null, with 'this' as the sender and an empty EventArgs
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
