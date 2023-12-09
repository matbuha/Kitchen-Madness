// Importing necessary libraries and namespaces for the script.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declaring the BaseCounter class which inherits from MonoBehaviour and implements the IKitchenObjectParent interface.
public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    // A private serialized field of type Transform, likely representing a point on the counter.
    [SerializeField] private Transform counterTopPoint;

    // Private field to hold a reference to a KitchenObject.
    private KitchenObject kitchenObject;

    // A public virtual method named 'Interact', taking a Player object as a parameter. 
    // This can be overridden in derived classes. Currently, it logs an error message when called.
    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();");
    }

    // A public virtual method named 'InteractAlternate'. It is currently commented out and does nothing.
    public virtual void InteractAlternate(Player player) {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }

    // Public method returning a Transform, likely the Transform of the point where the kitchen object should follow.
    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    // Public method for setting the kitchenObject. It takes a KitchenObject as a parameter and sets the class's kitchenObject field to it.
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    // Public method that returns the current kitchenObject.
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    // Public method that clears the kitchenObject by setting it to null.
    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    // Public method that returns a boolean indicating whether a kitchenObject is present or not.
    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

}
