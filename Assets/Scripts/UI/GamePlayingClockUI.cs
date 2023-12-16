/*
This script is part of the UI system in a Unity game and is responsible for updating the visual representation of a game timer.
It uses an Image component (timerImage), likely set to use a radial fill method, to represent the timer.
The script continually updates the fillAmount of the timerImage during the game based on the normalized game playing timer retrieved from KitchenGameManager.
This normalized value indicates the progress of the timer relative to its total duration,
allowing the image to visually represent the passage of time in the game.
*/

// Import necessary namespaces for Unity functionality and UI handling
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Declare a public class 'GamePlayingClockUI' that inherits from 'MonoBehaviour'
public class GamePlayingClockUI : MonoBehaviour {

    // Serialized private field for an Image component
    [SerializeField] private Image timerImage; // Reference to the Image component used to display the game timer

    // Define the Update method which is called every frame
    private void Update() {
        // Set the fill amount of the timer image based on the normalized game playing timer
        // The normalized value represents the proportion of time passed in the game
        timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
    }    
}
