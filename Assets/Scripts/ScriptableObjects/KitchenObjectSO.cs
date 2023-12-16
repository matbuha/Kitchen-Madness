/*
This script defines a ScriptableObject named KitchenObjectSO in Unity.
It is designed to represent a kitchen object in the game, encompassing its 3D prefab (prefab),
its 2D sprite representation (sprite), and its name (objectName).
The CreateAssetMenu attribute allows this class to be easily used for creating new asset instances within the Unity Editor,
which aids in managing and organizing different kitchen objects in the game.
The prefab and sprite allow for versatile use of the kitchen object in both 3D and 2D contexts within the game.
*/
 

// Import necessary namespaces for Unity functionality
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attribute to make this class appear in the Create Asset menu in Unity, allowing for the creation of scriptable objects from it
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject {

    // Public field to hold a reference to a Transform, which is typically used to store a prefab of the kitchen object
    public Transform prefab; // Prefab representing the physical appearance and behavior of the kitchen object in the game

    // Public field to hold a reference to a Sprite, which is typically used for UI representation or 2D visuals of the kitchen object
    public Sprite sprite; // Sprite image for the kitchen object, often used in UI or 2D representations

    // Public field to store the name of the kitchen object
    public string objectName; // The name of the kitchen object
}
