/*
This script defines a ScriptableObject named CuttingRecipeSO in Unity,
which is designed to represent a cutting recipe.
This type of recipe specifies how an input item (represented by KitchenObjectSO),
is transformed into an output item (also represented by KitchenObjectSO) through a cutting process.
The cuttingProgressMax field indicates the amount of progress needed to complete the cutting.
The CreateAssetMenu attribute enables this class to be used for creating new assets in the Unity Editor,
making it easier to manage and configure different cutting recipes in the game.
*/
 

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attribute to make this class appear in the Create Asset menu in Unity, allowing for the creation of scriptable objects from it
[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject {

    // Public field to hold a reference to the input KitchenObjectSO, representing the item to be cut
    public KitchenObjectSO input;

    // Public field to hold a reference to the output KitchenObjectSO, representing the result of the cutting process
    public KitchenObjectSO output;

    // Public field to store the required progress amount to complete the cutting
    public int cuttingProgressMax; // The maximum value of cutting progress required to transform the input into the output
}
