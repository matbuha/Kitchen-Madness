/*
This script defines a KitchenGameManager class in a Unity game, which manages the game's state,
including the countdown to start, game playing duration, and game over states.
It uses a singleton pattern for global access. The class provides methods to check the game's state,
get the countdown timer, toggle the game's pause state, and get the normalized game playing timer.
It also raises events on state changes and pause/unpause actions, allowing other components to respond accordingly.
The script listens to pause input via the GameInput class and updates the game's state based on the timers and input.
*/

// Import necessary namespaces for Unity functionality
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'KitchenGameManager' that inherits from 'MonoBehaviour'
public class KitchenGameManager : MonoBehaviour {

    // Singleton instance of KitchenGameManager
    public static KitchenGameManager Instance { get; private set; }


    // Events for state change and pause/unpause actions
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler OnRecipeSuccess;

    // Enum to define different game states
    private enum State {
        WaitingToStart,      // State when the game is waiting to start
        CountdownToStart,    // State during the countdown to start the game
        GamePlaying,         // State when the game is actively being played
        GameOver,            // State when the game is over
        MissionSuccess,      // State when the mission is successfully completed
    }

    // Private fields to manage game state and timers
    private State state;
    private float waitingToStartTimer = 1f;    // Timer for waiting to start
    private float countdownToStartTimer = 3f;  // Timer for countdown to start
    private float gamePlayingTimer;            // Timer for game playing duration
    [SerializeField] private float gamePlayingTimerMax = 180f;  // Maximum duration for the game playing timer
    private bool isGamePaused = false;         // Flag to check if the game is paused
    public int successfulDeliveries = 0;
    [SerializeField] public int successfulDeliveriesGoal = 3;

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one KitchenGameManager instance");
        }
        Instance = this;
        state = State.WaitingToStart;
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        DeliveryManager.Instance.OnRecipeSuccess += (sender, e) => {
            successfulDeliveries++;
            OnRecipeSuccess?.Invoke(this, EventArgs.Empty); // Invoke the event
            CheckForLevelCompletion();
        };
    }

    // Event handler method for pause action
    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame(); // Toggle the pause state of the game
    }

    // Define the Update method which is called every frame
    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                // Update logic for waiting to start state
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty); // Invoke state change event
                }
                break;
            case State.CountdownToStart:
                // Update logic for countdown to start state
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty); // Invoke state change event
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    CheckForLevelCompletion();
                }
                break;
            case State.GameOver:
                // Game over logic here if needed
                break;
        }
    }

    private void CheckForLevelCompletion() {
        if (gamePlayingTimer < 0f) {
            if(successfulDeliveries >= successfulDeliveriesGoal) {
                Loader.LoadNextLevel(); // Load the next level
            } else {
                state = State.GameOver;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    // Method to check if the game is currently playing
    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    // Method to check if countdown to start is active
    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    // Method to get the remaining countdown timer
    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    // Method to check if the game is over
    public bool IsGameOver() {
        return state == State.GameOver;
    }

    // Method to get the normalized game playing timer
    public float GetGamePlayingTimerNormalized() {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    // Method to toggle the pause state of the game
    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f; // Pause the game
            OnGamePaused?.Invoke(this, EventArgs.Empty); // Invoke game paused event
        } else {
            Time.timeScale = 1f; // Resume the game
            OnGameUnpaused?.Invoke(this, EventArgs.Empty); // Invoke game unpaused event
        }
    }
}
