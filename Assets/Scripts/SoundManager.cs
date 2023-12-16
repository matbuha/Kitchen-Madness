/*
This script defines a SoundManager class in Unity, which is responsible for managing and playing various sound effects throughout the game.
It uses a singleton pattern for easy access from other scripts.
The class listens to different events (e.g., recipe success, object placement, cutting) and plays corresponding sound effects.
It provides methods to play specific audio clips, change the volume, and get the current volume level.
The script also includes functionality to save and load volume settings using PlayerPrefs.
*/

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'SoundManager' that inherits from 'MonoBehaviour'
public class SoundManager : MonoBehaviour {

    // Define a constant for the player preference key of sound effects volume
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    // Singleton instance of SoundManager
    public static SoundManager Instance { get; private set; }


    // Serialized private field for a reference to AudioClipRefsSO, which contains references to audio clips
    [SerializeField] private AudioClipRefsSO audioClipRefsSO; // Reference to the scriptable object containing audio clip references


    // Private field to store the volume level
    private float volume = 1f; // Default volume level


    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        Instance = this; // Assign this instance to the singleton instance

        // Load the volume level from PlayerPrefs or use default if not set
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to various events to play corresponding sounds
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    // Event handler methods for different events that play corresponding sounds
    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    // Event handler methods for different events that play corresponding sounds
    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    // Event handler methods for different events that play corresponding sounds
    private void Player_OnPickedSomething(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    // Event handler methods for different events that play corresponding sounds
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    // Event handler methods for different events that play corresponding sounds
    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    // Event handler methods for different events that play corresponding sounds
    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    // Method to play a random sound from an array of audio clips at a specific position
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        // Randomly select and play a sound clip from the array
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    // Method to play a specific audio clip at a specific position
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        // Play the audio clip at the given position with adjusted volume
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    // Public methods to play specific types of sounds (e.g., footsteps, countdown, warning)
    public void PlayFootstepsSound(Vector3 position, float volume) {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    // Public methods to play specific types of sounds (e.g., footsteps, countdown, warning)
    public void PlayCountdownSound() {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }

    // Public methods to play specific types of sounds (e.g., footsteps, countdown, warning)
    public void PlayWarningSound(Vector3 position) {
        PlaySound(audioClipRefsSO.warning, position);
    }
    
    // Method to change the volume level and save it in PlayerPrefs
    public void ChangeVolume() {
        // Increment the volume and wrap around if it exceeds the maximum
        volume += .1f;
        if (volume > 1f) {
            volume = 0f;   
        }

        // Save the new volume level in PlayerPrefs and save the changes
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,volume);
        PlayerPrefs.Save();
    }

    // Method to get the current volume level
    public float GetVolume() {
        return volume; // Return the current volume level
    }

}