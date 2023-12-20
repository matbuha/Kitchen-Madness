/*
This script is attached to a player character in a Unity game and is responsible for playing footstep sounds as the player moves.
It uses a timer (footstepTimer) to control the frequency of the sound playback.
When the player is walking (as indicated by player.IsWalking()),
the script plays a footstep sound through the SoundManager.
The timer ensures that the sound is played at regular intervals,
creating a more realistic auditory effect as the player moves.
The Awake method is used to initialize the player variable by getting the Player component from the same GameObject.
*/

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Declare a public class 'PlayerSounds' that inherits from 'MonoBehaviour'
public class PlayerSounds : MonoBehaviour {

    // Private field for a reference to the Player component
    private Player player; // Reference to the Player component attached to the same GameObject

    // Private fields to manage the footstep sound timer
    private float footstepTimer; // Timer to track when to play the footstep sound
    private float footstepTimerMax = 0.1f; // Maximum time interval between playing footstep sounds

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        // Get the Player component attached to the same GameObject
        player = GetComponent<Player>();
    }

    // Define the Update method which is called every frame
    private void Update() {
        // Decrement the footstep timer by the time elapsed since the last frame
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f) { // Check if the footstep timer has elapsed
            footstepTimer = footstepTimerMax; // Reset the timer

            // Check if the player is walking
            if (player.IsWalking()) {
                float volume = 1f; // Set the volume for the footstep sound
                // Play the footstep sound at the player's position
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
        }
    }
}
