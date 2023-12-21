/*
This script defines an interface IHasProgress in Unity.
It's designed to provide a standard way for objects to communicate their progress (such as loading or task completion) to other parts of the game.
The interface includes an event OnProgressChanged, which can be triggered when the progress changes.
The OnProgressChangedEventArgs class is a custom event argument class that includes a progressNormalized field,
representing the progress as a normalized value (usually between 0 and 1).
This interface allows objects implementing it to notify listeners about their progress in a standardized format.
*/

// Import necessary namespaces for Unity functionality and system functions
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public interface 'IHasProgress'
public interface IHasProgress {

    // Declare an event 'OnProgressChanged' with a custom event argument class 'OnProgressChangedEventArgs'
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    // Define a nested class 'OnProgressChangedEventArgs' that extends EventArgs
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized; // A public field to hold the normalized progress value
    }
}
