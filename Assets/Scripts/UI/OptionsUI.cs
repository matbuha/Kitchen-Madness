/*
This script manages the options UI in a Unity game.
It sets up listeners for various buttons to handle their respective actions,
such as changing sound effects and music volumes, rebinding keys, and closing the options menu.
The script interacts with KitchenGameManager to respond to game unpause events and GameInput for key rebinding.
The Show and Hide methods control the visibility of the options UI,
and additional methods ShowPressToRebindKey and HidePressToRebindKey manage the visibility of the UI element for rebinding keys.
The UpdateVisual method updates the UI elements to reflect the current settings.
*/

// Import necessary namespaces for Unity functionality, UI handling, and text handling

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Declare a public class 'OptionsUI' that inherits from 'MonoBehaviour'
public class OptionsUI : MonoBehaviour {

    // Public static property to access the instance of this class
    public static OptionsUI Instance { get; private set; }

    // Serialized private fields for UI components
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    // Private field to store an action to be called when the close button is pressed
    private Action onCloseButtonAction;


    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        Instance = this; // Assign this instance to the static property Instance

        // Add listeners to the buttons to handle click events
        soundEffectsButton.onClick.AddListener (() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener (() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener (() => {
            Hide();
            onCloseButtonAction();
        });
        moveUpButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.Pause); });
        gamepadInteractButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.Gamepad_Interact); });
        gamepadInteractAlternateButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.Gamepad_InteractAlternate); });
        gamepadPauseButton.onClick.AddListener (() => { RebindBinding(GameInput.Binding.Gamepad_Pause); });
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the OnGameUnpaused event of the KitchenGameManager
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        // Initialize the UI
        UpdateVisual();

        HidePressToRebindKey();

        Hide();
    }

    // Define the event handler method for when the game is unpaused
    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e) {
        Hide(); // Hide the options UI
    }

    // Method to update the visual representation of the UI elements
    private void UpdateVisual() {
        // Update the text and other UI elements to reflect the current settings
        soundEffectsText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    // Public method to show the options UI
    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction; // Store the provided action
        gameObject.SetActive(true); // Activate the gameObject
        soundEffectsButton.Select(); // Select the sound effects button by default
    }

    // Public method to hide the options UI
    public void Hide() {
        gameObject.SetActive(false); // Deactivate the gameObject
    }

    // Method to show the press-to-rebind key UI element
    private void ShowPressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    // Method to hide the press-to-rebind key UI element
    private void HidePressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    // Method to handle rebinding of key bindings
    private void RebindBinding(GameInput.Binding binding) {
        ShowPressToRebindKey(); // Show the rebind UI
        // Call the RebindBinding method of GameInput and provide a callback for when the rebinding is complete
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}