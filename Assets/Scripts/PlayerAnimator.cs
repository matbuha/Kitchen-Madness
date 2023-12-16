/*
This script is attached to a player character in a Unity game and manages the character's animations.
It uses an Animator component to control animations based on the player's movement.
The script checks whether the player is walking using the IsWalking method from the Player class.
The result is then used to set the IS_WALKING parameter in the Animator, controlling the walk animation.
The Awake method is used to initialize the animator variable by getting the Animator component from the same GameObject.
*/

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'PlayerAnimator' that inherits from 'MonoBehaviour'
public class PlayerAnimator : MonoBehaviour {

    // Define a constant for the animator parameter
    private const string IS_WALKING = "IsWalking";

    // Serialized private field for a reference to the Player component
    [SerializeField] private Player player; // Reference to the Player component to access its state

    // Private field for the animator component
    private Animator animator;

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();
    }

    // Define the Update method which is called every frame
    private void Update() {
        // Set the animator's boolean parameter based on whether the player is walking
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
