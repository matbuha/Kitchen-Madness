/*
This script is a static utility class for managing scene loading in a Unity game.
It defines an enumeration Scene for easy reference to different scenes.
The class provides a method Load to begin loading a new scene, which first transitions to a loading scene.
Once the loading scene is ready, it is expected to call LoaderCallback, which then loads the target scene.
This approach is commonly used to create smooth transitions between scenes,
often displaying a loading screen or progress bar while the game loads the next scene.
*/

// Import necessary namespaces for Unity functionality and scene management
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Declare a public static class 'Loader'
public static class Loader {

    // Define an enum 'Scene' to specify different scenes that can be loaded
    public enum Scene {
        MainMenuScene,  // Represents the main menu scene
        Level1,         // Represents the first level scene
        LoadingScene,   // Represents the loading scene
    }

    // Private static field to store the target scene to load
    private static Scene targetScene;

    // Public static method to initiate the loading of a target scene
    public static void Load(Scene targetScene) {
        Loader.targetScene = targetScene; // Set the target scene

        // Load the loading scene
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    // Public static method to be called as a callback to load the target scene
    public static void LoaderCallback() {
        // Load the target scene
        SceneManager.LoadScene(targetScene.ToString());
    }
}
