using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Text Mesh Pro namespace

public class LevelDisplay : MonoBehaviour {

    private TextMeshProUGUI levelText;

    private void Start() {
        levelText = GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component
        UpdateLevelText();
    }

    private void UpdateLevelText() {
        string sceneName = SceneManager.GetActiveScene().name;

        // Assuming your level scene names are "Level1", "Level2", etc.
        if (sceneName.StartsWith("Level")) {
            levelText.text = "Level " + sceneName.Substring(5);
        } else {
            levelText.text = sceneName; // Or some default text
        }
    }
}
