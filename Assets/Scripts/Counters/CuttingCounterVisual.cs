// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class 'CuttingCounterVisual' that inherits from 'MonoBehaviour'
public class CuttingCounterVisual : MonoBehaviour {

    // Declare a private constant string 'CUT' and initialize it with the value "Cut"
    private const string CUT = "Cut";

    // Declare a private serialized field 'cuttingCounter' of type CuttingCounter
    [SerializeField] private CuttingCounter cuttingCounter;

    // Declare a private field 'animator' of type Animator
    private Animator animator;

    // Define the Awake method which is called when the script instance is being loaded
    private void Awake() {
        animator = GetComponent<Animator>(); // Assign the Animator component attached to this GameObject to the 'animator' field
    }

    // Define the Start method which is called just before any of the Update methods is called the first time
    private void Start() {
        // Subscribe to the 'OnCut' event of 'cuttingCounter' with the 'CuttingCounter_OnCut' method
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    // Define the event handler method for when 'OnCut' event is triggered
    private void CuttingCounter_OnCut(object sender, System.EventArgs e) {
        // Trigger the 'Cut' animation on the animator
        animator.SetTrigger(CUT);
    }

}
