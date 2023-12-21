/*
This script defines a ScriptableObject named BurningRecipeSO in Unity.
It's designed to represent a burning recipe,
which is a type of process in the game where an input item (represented by KitchenObjectSO),
is transformed into a burnt output item (also represented by KitchenObjectSO),
after a certain amount of time (specified by burningTimerMax).
The CreateAssetMenu attribute allows this class to be used to create new assets in the Unity Editor,
facilitating easy management and configuration of different burning recipes in the game.
*/
 

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attribute to make this class appear in the Create Asset menu in Unity, allowing for the creation of scriptable objects from it
[CreateAssetMenu]
public class BurningRecipeSO : ScriptableObject {

    // Public field to hold a reference to the input KitchenObjectSO, representing the input item for the burning recipe
    public KitchenObjectSO input; 

    // Public field to hold a reference to the output KitchenObjectSO, representing the result of the burning process
    public KitchenObjectSO output;

    // Public field to store the maximum timer value for the burning process
    public float burningTimerMax; // The maximum time in seconds before the input item is considered fully burnt
}
