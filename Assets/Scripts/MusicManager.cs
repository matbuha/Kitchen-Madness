/*
This script defines a MusicManager class in Unity, responsible for managing background music within a game.
It uses an AudioSource component to play the music.
The class provides a method ChangeVolume to adjust the music volume and save the new setting using PlayerPrefs.
The GetVolume method allows other parts of the game to retrieve the current volume level.
The script initializes the volume from saved preferences (or a default value) in the Awake method and applies this setting to the AudioSource.
The MusicManager is implemented as a singleton to ensure only one instance manages the music settings across the game.
*/

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'MusicManager' that inherits from 'MonoBehaviour'
public class MusicManager : MonoBehaviour {

    // Define a constant for the player preference key of music volume
    private const string PLAYER_PREFS_MUSIC_EFFECTS_VOLUME = "MusicVolume";

    // Singleton instance of MusicManager
    public static MusicManager Instance { get; private set; }

    // Private field for the AudioSource component
    private AudioSource audioSource; // Component to play music
    private float volume = .1f; // Initial volume level

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        Instance = this; // Assign this instance to the singleton instance
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to the same GameObject

        // Load the music volume level from PlayerPrefs or use default if not set
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_EFFECTS_VOLUME, .3f);
        audioSource.volume = volume; // Set the volume of the audio source
    }

    // Method to change the music volume level and save it in PlayerPrefs
    public void ChangeVolume() {
        volume += .1f; // Increment the volume
        if (volume > 1f) {
            volume = 0f; // Reset volume to 0 if it exceeds the maximum
        }
        audioSource.volume = volume; // Update the volume of the audio source

        // Save the new volume level in PlayerPrefs and save the changes
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    // Method to get the current music volume level
    public float GetVolume() {
        return volume; // Return the current volume level
    }
}
