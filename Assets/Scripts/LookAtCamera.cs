/*
This script provides functionality to control the orientation of a game object relative to the camera in Unity.
It uses an enum Mode to define different types of orientations: directly looking at the camera,
looking in the opposite direction, aligning with the camera's forward direction,
and aligning in the opposite direction of the camera's forward.
The LateUpdate method updates the orientation of the game object based on the selected mode.
This is useful for scenarios like making a UI element always face the camera or having an object always face away from the camera.
*/

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Declare a public class 'LookAtCamera' that inherits from 'MonoBehaviour'
public class LookAtCamera : MonoBehaviour {

    // Define an enum 'Mode' to specify different look at modes
    private enum Mode {
        LookAt,               // Mode where the object looks at the camera
        LookAtInverted,       // Mode where the object looks away from the camera
        CameraForward,        // Mode where the object aligns with the camera's forward direction
        CameraForwardInverted // Mode where the object aligns with the opposite of the camera's forward direction
    }

    // Serialized private field to choose the mode in the Unity Inspector
    [SerializeField] private Mode mode;

    // Define the LateUpdate method which is called after all Update methods have been called
    private void LateUpdate() {
        switch (mode) {
            case Mode.LookAt:
                // Make the object look directly at the camera
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                // Calculate direction from the camera to the object and make the object look in the opposite direction
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                // Align the object's forward direction with the camera's forward direction
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                // Align the object's forward direction with the opposite of the camera's forward direction
                transform.forward = -Camera.main.transform.forward;
                break;
        }        
    }
}
