/*
This script defines a StoveCounter class in a Unity game,
which inherits from BaseCounter and implements the IHasProgress interface.
It handles various states of cooking (Idle, Frying, Fried, Burned) and manages the frying and burning process of food items.
The script uses events to notify other parts of the game about the stove's state and progress changes,
and it includes logic to interact with players,
allowing them to add or remove kitchen objects from the stove.
The class also contains helper methods to work with recipes and determine the output based on the input kitchen objects.
*/
// Import necessary namespaces for Unity functionality and system functions
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter; // Import static members of the CuttingCounter class

// Declare a public class 'StoveCounter' that inherits from 'BaseCounter' and implements the 'IHasProgress' interface
public class StoveCounter : BaseCounter, IHasProgress {

    // Declare events for tracking progress and state changes of the stove counter
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    // Declare a nested class 'OnStateChangedEventArgs' that extends EventArgs to include stove state information
    public class OnStateChangedEventArgs : EventArgs {
        public State state; // Enum value representing the current state of the stove
    }

    // Enum to represent the different states of the stove
    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    // Serialized fields to reference arrays of frying and burning recipes
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    // Private fields to manage the state, timers, and current recipes of the stove
    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    // Initialize the stove counter state when the script starts
    private void Start() {
        state = State.Idle;
    }

    // Update the stove counter's logic each frame
    private void Update() {
        if (HasKitchenObject()) { // Check if there's a kitchen object on the stove
            switch (state) { // Handle logic based on the current state of the stove
                case State.Idle:
                    // Do nothing when idle
                    break;
                case State.Frying:
                    // Handle frying logic
                    fryingTimer += Time.deltaTime; // Increment the frying timer

                    // Notify of progress change
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.FryingTimerMax
                    });

                    // Check if frying is complete
                    if (fryingTimer > fryingRecipeSO.FryingTimerMax) {
                        GetKitchenObject().DestroySelf(); // Destroy the current kitchen object

                        // Spawn the output of the frying process
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        // Transition to the fried state
                        state = State.Fried;
                        burningTimer = 0f; // Reset burning timer
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO()); // Get the burning recipe

                        // Notify of state change
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    }
                    break;
                case State.Fried:
                    // Handle fried logic
                    burningTimer += Time.deltaTime; // Increment the burning timer

                    // Notify of progress change
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    // Check if burning is complete
                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        GetKitchenObject().DestroySelf(); // Destroy the current kitchen object

                        // Spawn the output of the burning process
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        // Transition to the burned state
                        state = State.Burned;

                        // Notify of state and progress change
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                    }
                    break;
                case State.Burned:
                    // Do nothing when burned
                    break;
            }
        }
    }

    // Override the interact method to handle player interaction with the stove counter
    public override void Interact(Player player) {
        if (!HasKitchenObject()) { // Check if the stove is empty
            if (player.HasKitchenObject()) { // Check if the player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) { // Check if the player's object can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this); // Place the player's object on the stove

                    // Prepare to start frying
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f; // Reset frying timer

                    // Notify of state and progress change
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.FryingTimerMax
                    });
                }
            } else {
                // Do nothing if the player is not carrying anything
            }
        } else { // Handle logic if there's a kitchen object on the stove
            if (player.HasKitchenObject()) { // Check if the player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) { // Check if the player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) { // Try to add the stove's object to the plate
                        GetKitchenObject().DestroySelf(); // Destroy the stove's object
                        state = State.Idle; // Reset to idle state

                        // Notify of state and progress change
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                    }
                }
            } else { // Handle if the player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player); // Give the stove's object to the player

                state = State.Idle; // Reset to idle state

                // Notify of state and progress change
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
            }
        }
    }

    // Helper methods to check for recipes and get outputs based on input objects
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        return GetFryingRecipeSOWithInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO recipe = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return recipe?.output;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO recipe in fryingRecipeSOArray) {
            if (recipe.input == inputKitchenObjectSO) {
                return recipe;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO recipe in burningRecipeSOArray) {
            if (recipe.input == inputKitchenObjectSO) {
                return recipe;
            }
        }
        return null;
    }

    // Method to check if the current state is Fried
    public bool IsFried() {
        return state == State.Fried;
    }
}
