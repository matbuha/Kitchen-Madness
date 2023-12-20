/*
This script sets up a DeliveryCounter class in a Unity game.
The class inherits from BaseCounter and overrides the Interact method to handle specific interactions related to delivery,
particularly focusing on player interactions with plates.
It utilizes a singleton pattern for the DeliveryCounter instance and interacts with a DeliveryManager to manage recipe deliveries.
*/

// Import necessary namespaces for Unity functionality

// Declare a public class 'DeliveryCounter' that inherits from 'BaseCounter'
public class DeliveryCounter : BaseCounter {

    // Declare a public static property 'Instance' with a getter and a private setter, of type DeliveryCounter
    public static DeliveryCounter Instance { get; private set; }

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        Instance = this; // Set the static Instance property to this instance of DeliveryCounter
    }

    // Override the 'Interact' method from the BaseCounter class with a parameter of type Player
    public override void Interact(Player player) {
        // Check if the player has a kitchen object
        if (player.HasKitchenObject()) {
            // Check if the player's kitchen object is a plate
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                // Only accepts plates

                // Call the DeliverRecipe method of the DeliveryManager's Instance, passing the plateKitchenObject
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                
                // Destroy the kitchen object that the player is holding
                player.GetKitchenObject().DestroySelf();
            }
        }
    }

}
