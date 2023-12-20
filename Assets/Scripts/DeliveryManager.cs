/*
This script manages the delivery system in a Unity game.
It handles the spawning of recipes, tracks waiting recipes, and processes recipe deliveries made by the player.
The DeliveryManager uses a singleton pattern to ensure only one instance is active.
It subscribes to recipe-related events, such as spawning new recipes, and processing recipe deliveries,
including checking for successful matches between the ingredients on a plate and the required recipe ingredients.
The script also keeps track of successfully delivered recipes
*/

// Import necessary namespaces for Unity functionality

using System;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'DeliveryManager' that inherits from 'MonoBehaviour'
public class DeliveryManager : MonoBehaviour {


    // Events to notify when recipes are spawned, completed, succeeded, or failed
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;


    // Singleton pattern implementation
    public static DeliveryManager Instance { get; private set; }


    // Serialized private field for a reference to a RecipeListSO which contains a list of recipes
    [SerializeField] private RecipeListSO recipeListSO;


    // Private fields for managing recipes
    private List<RecipeSO> waitingRecipeSOList; // List of recipes waiting to be completed
    private float spawnRecipeTimer; // Timer for spawning new recipes
    private float spawnRecipeTimerMax = 4f; // Maximum time to spawn new recipes
    private int waitingRecipesMax = 4; // Maximum number of waiting recipes
    private int successfulRecipesAmount; // Counter for the number of successfully completed recipes
    

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        Instance = this; // Assign this instance to the static Instance property
        waitingRecipeSOList = new List<RecipeSO>(); // Initialize the list of waiting recipes
    }

    // Define the Update method which is called every frame
    private void Update() {
        // Decrement the recipe spawn timer
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax; // Reset the timer

            // Check if more recipes can be spawned
            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                // Randomly select a recipe to add to the waiting list
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);// Add the selected recipe

                // Invoke the OnRecipeSpawned event
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    // Public method to handle recipe delivery
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // Check if the plate contents match the recipe
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // Cycling through all ingredients in the Plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            // Ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // This Recipe ingredient was not found on the Plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe) {
                    // If Player delivered the correct recipe!
                    successfulRecipesAmount++;
                    waitingRecipeSOList.RemoveAt(i); // Remove the recipe from waiting list

                    // Invoke the OnRecipeCompleted and OnRecipeSuccess events
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        // If no match found, invoke the OnRecipeFailed event
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    // Public method to get the list of waiting recipes
    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

    // Public method to get the number of successfully completed recipes
    public int GetSuccessfulRecipesAmount() {
        return successfulRecipesAmount;
    }

}
