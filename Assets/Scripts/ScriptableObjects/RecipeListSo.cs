/*
This script defines a ScriptableObject named RecipeListSO in Unity.
Its primary purpose is to store a list of RecipeSO objects,
effectively creating a collection or database of recipes.
Each RecipeSO in the recipeSOList represents an individual recipe in the game.
The CreateAssetMenu attribute enables this class to be easily used for creating new asset instances within the Unity Editor,
simplifying the management and organization of multiple recipes in the game.
*/
 

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attribute to make this class appear in the Create Asset menu in Unity, allowing for the creation of scriptable objects from it
[CreateAssetMenu]
public class RecipeListSO : ScriptableObject {

    // Public field to hold a list of RecipeSO objects, representing a collection of recipes
    public List<RecipeSO> recipeSOList; // This list stores various recipes, each defined as a RecipeSO object
}
