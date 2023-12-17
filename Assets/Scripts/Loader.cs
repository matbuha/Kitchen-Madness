using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {
    public static void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            // Handle last level completion here, like loading the main menu
            SceneManager.LoadScene(0); // Assuming the main menu is at index 0
        }
    }

    // Method to load a specific scene
    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    // LoaderCallback remains the same
    public static void LoaderCallback() {
        // Load the target scene
        SceneManager.LoadScene(targetScene.ToString());
    }

    // Enum Scene remains the same, if you want to keep specific scene names
    public enum Scene {
        MainMenuScene,
        Level1,
        Level2,
        LoadingScene,
        // Add other scenes here if needed
    }

    private static Scene targetScene;
}
