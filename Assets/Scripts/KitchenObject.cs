/*
This script defines a KitchenObject class in a Unity game.
It's used to represent an interactive object in a kitchen environment, like a utensil or food item.
The script includes functionalities to set and get the object's parent (IKitchenObjectParent),
handle the object's destruction, and check if it's a specific type (like a plate).
It also includes a static method to spawn new KitchenObject instances,
using a KitchenObjectSO (a scriptable object representing kitchen items) and set their parent.
*/

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Declare a public class 'KitchenObject' that inherits from 'MonoBehaviour'
public class KitchenObject : MonoBehaviour {

    // Serialized private field for a reference to KitchenObjectSO
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Reference to the scriptable object representing this kitchen object

    // Private field to store the parent object that interacts with this kitchen object
    private IKitchenObjectParent kitchenObjectParent;

    // Public method to get the associated KitchenObjectSO
    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO; // Return the scriptable object associated with this kitchen object
    }

    // Public method to set the parent object for this kitchen object
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        // Clear the current kitchen object from its parent if it exists
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        // Set the new parent object
        this.kitchenObjectParent = kitchenObjectParent;

        // Check if the new parent already has a kitchen object and log an error if it does
        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject!");
        }

        // Set this kitchen object in the new parent
        kitchenObjectParent.SetKitchenObject(this);

        // Set this object's transform to follow the parent object's transform
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero; // Reset local position to zero
    }

    // Public method to get the current parent of this kitchen object
    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent; // Return the parent object
    }

    // Public method to destroy this kitchen object
    public void DestroySelf() {
        // Clear this kitchen object from its parent
        kitchenObjectParent.ClearKitchenObject();

        // Destroy this game object
        Destroy(gameObject);
    }

    // Public method to check if this kitchen object is a plate and output it
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        // Check if this kitchen object is a plate
        if (this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject; // Cast and output it as a plate
            return true;
        } else {
            plateKitchenObject = null; // Set output to null if it's not a plate
            return false;
        }
    }

    // Static method to spawn a new kitchen object from a KitchenObjectSO
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        // Instantiate the prefab from the KitchenObjectSO
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        // Get the KitchenObject component from the instantiated object
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        
        // Set the parent of the newly spawned kitchen object
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        // Return the newly spawned kitchen object
        return kitchenObject;
    }
}
