/*
This script is for a CuttingCounterVisual in a Unity game,
likely designed to handle the visual representation of a cutting counter.
It uses an Animator component to trigger a cut animation when a certain event (likely related to cutting actions) occurs on the CuttingCounter object.
The script listens to an event (OnCut) from the cuttingCounter and responds by triggering an animation.
*/

// Import necessary namespaces for Unity functionality

using System;
using UnityEngine;

// Declare a public class 'CuttingCounter' that inherits from 'BaseCounter' and implements the 'IHasProgress' interface
public class CuttingCounter : BaseCounter, IHasProgress {

    // Declare a public static event 'OnAnyCut' of type EventHandler
    public static event EventHandler OnAnyCut;

    // Declare a new public static method 'ResetStaticData' that hides the inherited method
    public static void RestStaticData() {
        OnAnyCut = null;
    }

    // Declare events 'OnProgressChanged' and 'OnCut' with specific event handler types
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    // Declare a private serialized array 'cuttingRecipeSOArray' of type CuttingRecipeSO
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;


    // Declare a private integer 'cuttingProgress' to track the progress of cutting
    private int cuttingProgress;


    // Override the 'Interact' method from BaseCounter with a parameter of type Player
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // There is no KitchenObject on the counter
            if (player.HasKitchenObject()) {
                // Player is carrying a KitchenObject
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // Player is carrying an item that can be cut according to the recipes
                    player.GetKitchenObject().SetKitchenObjectParent(this); // Set the counter as the parent of the player's KitchenObject
                    cuttingProgress = 0; // Reset cutting progress

                    // Find the recipe for the KitchenObject on the counter
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    // Invoke the OnProgressChanged event with normalized progress
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            } else {
                // Player is not carrying anything
            }
        } else {
            // There is KitchenObject on the counter
            if (player.HasKitchenObject()) {
                // Player is carrying a KitchenObject
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        // Try to add the counter's KitchenObject as an ingredient to the plate; if successful, destroy the counter's KitchenObject
                        GetKitchenObject().DestroySelf();
                    }
                }
            } else {
                // Player is not carrying something
                // Transfer the KitchenObject from the counter to the player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    // Override the 'InteractAlternate' method for alternate interactions with the player
    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // There is a KitchenObject on the counter and it can be cut
            cuttingProgress++; // Increment the cutting progress


            // Invoke the OnCut and OnAnyCut events
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            
            // Get the recipe for the KitchenObject on the counter
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            // Invoke the OnProgressChanged event with updated normalized progress
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            // Check if cutting is complete
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                // Find the output KitchenObject for the input
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                // Destroy the current KitchenObject on the counter and spawn the output KitchenObject
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);                    
            }
        }
    }

    // Define a private method 'HasRecipeWithInput' that checks if there is a recipe for the given input KitchenObjectSO
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        // Get the recipe for the input KitchenObjectSO
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null; // Return true if a recipe is found, otherwise false
    }

    // Define a private method 'GetOutputForInput' to get the output KitchenObjectSO for a given input KitchenObjectSO
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        // Get the recipe for the input KitchenObjectSO
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output; // Return the output KitchenObjectSO from the recipe
        } else {
            return null; // Return null if no recipe is found
        }
    }

    // Define a private method 'GetCuttingRecipeSOWithInput' to find a CuttingRecipeSO for a given input KitchenObjectSO
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        // Iterate through the cuttingRecipeSOArray to find a matching recipe
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO; // Return the found recipe
            }
        }
        return null; // Return null if no matching recipe is found
    }

}