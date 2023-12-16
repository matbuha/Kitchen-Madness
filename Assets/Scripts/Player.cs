/*
This script defines a Player class in Unity, which manages player interactions and movements in a kitchen-themed game.
The player can interact with counters and hold kitchen objects.
The script includes methods for handling movement, interacting with counters, and managing a kitchen object.
It implements the IKitchenObjectParent interface, providing methods to set and get a kitchen object,
and to determine if the player is holding a kitchen object.
The class uses events to notify other parts of the game about certain actions,
such as picking up an object or selecting a counter.
It also subscribes to input actions for interacting and alternate interacting.
The movement and interaction logic is updated in the Update method,
and the script maintains the state related to player movement and selected counters.
*/

// Import necessary namespaces for Unity functionality
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'Player' that inherits from 'MonoBehaviour' and implements 'IKitchenObjectParent'
public class Player : MonoBehaviour, IKitchenObjectParent {

    // Singleton instance of Player
    public static Player Instance { get; private set; }


    // Events for player actions and interactions
    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter; // The counter currently selected by the player
    }

    // Serialized private fields for player properties and references
    [SerializeField] private float moveSpeed = 7f; // Movement speed of the player
    [SerializeField] private GameInput gameInput; // Reference to the GameInput component
    [SerializeField] private LayerMask countersLayerMask; // Layer mask to detect counters
    [SerializeField] private Transform kitchenObjectHoldPoint; // Point where kitchen objects are held


    // Private fields for internal state management
    private bool isWalking; // Flag to check if the player is walking
    private Vector3 lastInteractDir; // The last direction of interaction
    private BaseCounter selectedCounter; // The currently selected counter
    private KitchenObject kitchenObject; // The kitchen object currently held by the player


    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        // Ensure there is only one instance of the Player
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to input action events
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    // Event handler methods for input actions
    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    // Event handler methods for input actions
    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    // Define the Update method which is called every frame
    private void Update() {
        // Handle player movement and interactions
        HandleMovement();
        HandleInteractions();
    }

    // Method to check if the player is walking
    public bool IsWalking() {
        return isWalking;
    }

    // Private methods to handle interactions and movement
    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                // Has ClearCounter
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);

            }
        } else {
            SetSelectedCounter(null);
        }
    }

    // Private methods to handle interactions and movement
    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can move only on the X
                moveDir = moveDirX;
            } else {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                } else {
                    // Cannot move in any direction
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    // Method to set the selected counter and invoke the corresponding event
    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        // Invoke the OnSelectedCounterChanged event
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    // IKitchenObjectParent interface implementation
    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    // IKitchenObjectParent interface implementation
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    // IKitchenObjectParent interface implementation
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    // IKitchenObjectParent interface implementation
    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    // IKitchenObjectParent interface implementation
    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

}