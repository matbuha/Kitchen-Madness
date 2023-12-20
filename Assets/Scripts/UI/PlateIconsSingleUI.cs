/*
This script is part of a UI system in a Unity game and is responsible for managing a single icon in a UI element that represents a plate.
It has a method SetKitchenObjectSO, which updates the sprite of the Image component (image) to reflect a specific KitchenObjectSO (a scriptable object representing a kitchen item).
This allows for dynamic updating of UI icons based on the specific kitchen items that are being interacted with or represented in the game.
*/

// Import necessary namespaces for Unity functionality and UI handling

using UnityEngine;
using UnityEngine.UI;

// Declare a public class 'PlateIconsSingleUI' that inherits from 'MonoBehaviour'
public class PlateIconsSingleUI : MonoBehaviour {

    // Serialized private field for the UI image component
    [SerializeField] private Image image; // Reference to the Image component used to display a kitchen object's sprite

    // Public method to set the kitchen object scriptable object and update the image sprite accordingly
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
        image.sprite = kitchenObjectSO.sprite; // Set the sprite of the image to the sprite of the provided KitchenObjectSO
    }
}
