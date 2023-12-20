/*
This script sets up a ContainerCounterVisual in a Unity game,
designed to manage visual aspects of a container counter, like animations.
The class subscribes to an event (OnPlayerGrabbedObject) from the containerCounter.
When this event is triggered, it responds by triggering an 'OpenClose' animation using the Animator component.
This behavior is typically used to visually represent the interaction of a player with a container in the game.
*/

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Declare a public class 'ContainerCounterVisual' that inherits from 'MonoBehaviour'
public class ContainerCounterVisual : MonoBehaviour {

    // Declare a private constant string 'OPEN_CLOSE' and initialize it with the value "OpenClose"
    private const string OPEN_CLOSE = "OpenClose";

    // Declare a private serialized field 'containerCounter' of type ContainerCounter
    [SerializeField] private ContainerCounter containerCounter;

    // Declare a private field 'animator' of type Animator
    private Animator animator;

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        animator = GetComponent<Animator>(); // Assign the Animator component attached to this GameObject to the 'animator' field
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the 'OnPlayerGrabbedObject' event of 'containerCounter' with the 'ContainerCounter_OnPlayerGrabbedObject' method
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    // Define the event handler method for when 'OnPlayerGrabbedObject' event is triggered
    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e) {
        // Trigger the 'OpenClose' animation on the animator
        animator.SetTrigger(OPEN_CLOSE);
    }

}
