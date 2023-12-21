/*
This script defines a ScriptableObject named RecipeSO in Unity.
It's designed to represent a general recipe in the game,
consisting of a list of kitchen objects (kitchenObjectSOList) that are ingredients or items used in the recipe.
The recipeName field is used to store the name of the recipe.
The CreateAssetMenu attribute allows this class to be used for creating new assets directly within the Unity Editor,
which is useful for easily managing and organizing various recipes in the game.
*/
 

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attribute to make this class appear in the Create Asset menu in Unity, allowing for the creation of scriptable objects from it
[CreateAssetMenu()]
public class RecipeSO : ScriptableObject {

    // Public field to hold a list of KitchenObjectSOs, representing the ingredients or items involved in the recipe
    public List<KitchenObjectSO> kitchenObjectSOList;

    // Public field to store the name of the recipe
    public string recipeName; // The name of the recipe
}
