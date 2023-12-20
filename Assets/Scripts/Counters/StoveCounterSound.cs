/*
This script is responsible for managing the sound effects related to a stove counter in a Unity game.
It plays sound when the stove counter is in specific states (Frying or Fried) and triggers a warning sound based on the progress of the cooking,
indicating a potential burn risk.
The script listens to state and progress changes in a StoveCounter object,
and uses an AudioSource for playing or pausing sounds,
as well as a SoundManager for additional sound effects.
*/
 

// Import necessary namespaces for Unity functionality and system functions

using UnityEngine;

// Declare a public class 'StoveCounterSound' that inherits from 'MonoBehaviour'
public class StoveCounterSound : MonoBehaviour {

    // Declare a private serialized field 'stoveCounter' of type StoveCounter
    [SerializeField] private StoveCounter stoveCounter;

    // Declare private fields for the audio source, a timer for warning sounds, and a flag to play warning sounds
    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        audioSource = GetComponent<AudioSource>(); // Assign the AudioSource component attached to this GameObject to the 'audioSource' field
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the 'OnStateChanged' and 'OnProgressChanged' events of 'stoveCounter'
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += stoveCounter_OnProgressChanged;
    }

    // Define the event handler method for when the progress of the stove counter changes
    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        // Define a threshold for showing the burn progress
        float burnShowProgressAmount = .5f;
        // Set the playWarningSound flag based on whether the stove is fried and the progress exceeds the threshold
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    // Define the event handler method for when the state of the stove counter changes
    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        // Determine whether to play the sound based on the stove state being Frying or Fried
        bool PlaySound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (PlaySound) {
            audioSource.Play(); // Play the sound
        } else {
            audioSource.Pause(); // Pause the sound
        }
    }

    // Define the Update method which is called every frame
    private void Update() {
        // Check if the warning sound should be played
        if (playWarningSound) {
            warningSoundTimer -= Time.deltaTime; // Decrement the timer by the time elapsed since the last frame
            // Check if the timer has elapsed
            if (warningSoundTimer <= 0f) {
                float warningSoundTimerMax = .5f; // Reset the timer
                warningSoundTimer = warningSoundTimerMax;

                // Play the warning sound using the SoundManager at the stove counter's position
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
