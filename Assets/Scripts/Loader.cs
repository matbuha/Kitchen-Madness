using UnityEngine.SceneManagement;

public static class Loader {
    private static Scene targetScene;

    public enum Scene {
        MainMenuScene,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        LoadingScene,
        // Add other scenes here if needed
    }

    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            LoadLoadingScene(Scene.LoadingScene);
            targetScene = (Scene)nextSceneIndex; // Set the target scene
        } else {
            SceneManager.LoadScene(0); // Assuming the main menu is at index 0
        }
    }

    private static void LoadLoadingScene(Scene loadingScene) {
        SceneManager.LoadScene(loadingScene.ToString());
    }

    public static void LoaderCallback() {
        // Load the target scene
        SceneManager.LoadScene(targetScene.ToString());
    }
}
