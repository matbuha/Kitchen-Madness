/*
This script is dealing with kitchen objects and counters.
Each method and property has a specific role in the gameplay mechanics related to kitchen objects.
*/
// Import necessary libraries and namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'BaseCounter' that inherits from 'MonoBehaviour' and implements the 'IKitchenObjectParent' interface
public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    // Declare a public static event 'OnAnyObjectPlacedHere' of type EventHandler
    public static event EventHandler OnAnyObjectPlacedHere;

    // Declare a public static method 'ResetStaticData' to reset the static event
    public static void ResetStaticData() {
        OnAnyObjectPlacedHere = null; // Set the static event to null
    }

    // Declare a private serialized field 'counterTopPoint' of type Transform
    [SerializeField] private Transform counterTopPoint;

    // Declare a private field 'kitchenObject' of type KitchenObject
    private KitchenObject kitchenObject;

    // Declare a public virtual method 'Interact' with a parameter of type Player
    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();"); // Log an error message for debugging purposes
    }

    // Declare a public virtual method 'InteractAlternate' with a parameter of type Player, currently empty
    public virtual void InteractAlternate(Player player) {
    }

    // Declare a public method 'GetKitchenObjectFollowTransform' returning a Transform object
    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint; // Return the Transform 'counterTopPoint'
    }

    // Declare a public method 'SetKitchenObject' with a parameter of type KitchenObject
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject; // Set the private field 'kitchenObject' to the passed parameter

        // Check if 'kitchenObject' is not null and invoke the event 'OnAnyObjectPlacedHere'
        if (kitchenObject != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    // Declare a public method 'GetKitchenObject' returning a KitchenObject
    public KitchenObject GetKitchenObject() {
        return kitchenObject; // Return the private field 'kitchenObject'
    }

    // Declare a public method 'ClearKitchenObject' to clear the kitchen object
    public void ClearKitchenObject() {
        kitchenObject = null; // Set the 'kitchenObject' to null
    }

    // Declare a public method 'HasKitchenObject' returning a boolean
    public bool HasKitchenObject() {
        return kitchenObject != null; // Return true if 'kitchenObject' is not null, otherwise false
    }

}
