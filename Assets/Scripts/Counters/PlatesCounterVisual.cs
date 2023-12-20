/*
This script is responsible for the visual representation of plates on a plates counter in a Unity game.
It subscribes to events from PlatesCounter to manage the addition and removal of plate visuals.
When a plate is spawned or removed (as indicated by the PlatesCounter),
it updates the list of plate visual GameObjects and handles their instantiation and destruction,
respectively, to reflect these changes visually in the game.
*/
 

// Import necessary namespaces for Unity functionality

using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'PlatesCounterVisual' that inherits from 'MonoBehaviour'
public class PlatesCounterVisual : MonoBehaviour {

    // Declare private serialized fields for PlatesCounter, the top point of the counter, and the plate visual prefab
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    // Declare a private field 'plateVisualGameObjectList' to store a list of plate visual GameObjects
    private List<GameObject> plateVisualGameObjectList;

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        // Initialize the list of plate visual GameObjects
        plateVisualGameObjectList = new List<GameObject>();
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the 'OnPlateSpawned' and 'OnPlateRemoved' events of 'platesCounter'
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    // Define the event handler method for when a plate is removed
    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        // Get the last plate GameObject from the list
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        // Remove the plate GameObject from the list
        plateVisualGameObjectList.Remove(plateGameObject);
        // Destroy the plate GameObject
        Destroy(plateGameObject);
    }

    // Define the event handler method for when a plate is spawned
    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        // Instantiate a new plate visual at the counter top point
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        // Define the offset for the Y position of the plate
        float plateOffsetY = .1f;
        // Set the local position of the plate visual with the calculated offset based on the number of plates
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);

        // Add the new plate visual GameObject to the list
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
