/*
This script defines a ScriptableObject named FryingRecipeSO in Unity.
It's designed to represent a frying recipe,
which specifies how an input item (represented by KitchenObjectSO),
is transformed into a fried output item (also represented by KitchenObjectSO) after a certain amount of time (specified by FryingTimerMax).
The CreateAssetMenu attribute allows this class to be used for creating new assets in the Unity Editor,
facilitating easy management and configuration of different frying recipes in the game.
*/
 

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attribute to make this class appear in the Create Asset menu in Unity, allowing for the creation of scriptable objects from it
[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject {

    // Public field to hold a reference to the input KitchenObjectSO, representing the item to be fried
    public KitchenObjectSO input; // The input object that is to be fried according to this recipe

    // Public field to hold a reference to the output KitchenObjectSO, representing the result of the frying process
    public KitchenObjectSO output; // The output object that is produced after frying the input object

    // Public field to store the maximum timer value for the frying process
    public float FryingTimerMax; // The maximum time in seconds for the frying process to complete
}
