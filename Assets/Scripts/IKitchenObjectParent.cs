/*
This script defines an interface IKitchenObjectParent in Unity.
It's designed to provide a set of method signatures for objects that can act as parents to KitchenObject instances,
such as kitchen counters or storage areas in a cooking-themed game.
The interface includes methods for setting and retrieving a kitchen object, clearing it,
checking if a kitchen object is present, and getting the transform that kitchen objects should follow.
This interface allows for a standardized way of interacting with different game objects that can hold or interact with kitchen objects,
providing flexibility and modularity in the game's design.
*/

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public interface 'IKitchenObjectParent'
public interface IKitchenObjectParent {

    // Define a method signature for getting the transform that a kitchen object should follow
    public Transform GetKitchenObjectFollowTransform(); // Returns the Transform that a KitchenObject should position itself relative to

    // Define a method signature for setting a kitchen object
    public void SetKitchenObject(KitchenObject kitchenObject); // Assigns a KitchenObject to this parent

    // Define a method signature for getting the currently set kitchen object
    public KitchenObject GetKitchenObject(); // Retrieves the currently assigned KitchenObject

    // Define a method signature for clearing any set kitchen object
    public void ClearKitchenObject(); // Clears the currently assigned KitchenObject, if any

    // Define a method signature for checking if a kitchen object is currently set
    public bool HasKitchenObject(); // Checks if there is a KitchenObject currently assigned
}
