/*
This script defines a ScriptableObject in Unity called AudioClipRefsSO.
It's designed to hold references to arrays of AudioClip objects,
each array corresponding to a different type of sound effect (like chopping, footsteps,
or stove sizzling) in the game.
This allows for easy organization and access to various sound effects that might be used across different components of the game.
The CreateAssetMenu attribute allows this script to be used for creating new assets in the Unity Editor,
providing a convenient way to manage and update sound effects.
*/
 

// Import necessary namespaces for Unity functionality

using UnityEngine;

// Attribute to make this class appear in the Create Asset menu in Unity, allowing for the creation of scriptable objects from it
[CreateAssetMenu]
public class AudioClipRefsSO : ScriptableObject {

    // Public fields to hold arrays of AudioClips for different sound effects
    public AudioClip[] chop;           // Array to store AudioClips related to chopping sound effects
    public AudioClip[] deliveryFail;   // Array to store AudioClips related to delivery failure sound effects
    public AudioClip[] deliverySuccess;// Array to store AudioClips related to delivery success sound effects
    public AudioClip[] footstep;       // Array to store AudioClips related to footstep sound effects
    public AudioClip[] objectDrop;     // Array to store AudioClips related to object dropping sound effects
    public AudioClip[] objectPickup;   // Array to store AudioClips related to object pickup sound effects
    public AudioClip[] stoveSizzle;    // Array to store AudioClips related to stove sizzling sound effects
    public AudioClip[] trash;          // Array to store AudioClips related to trash sound effects
    public AudioClip[] warning;        // Array to store AudioClips related to warning sound effects
}
