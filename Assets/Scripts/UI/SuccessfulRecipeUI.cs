using UnityEngine;
using TMPro;  // Make sure to use this namespace for Text Mesh Pro

public class SuccessfulRecipeUI : MonoBehaviour {

    private TextMeshProUGUI successfulRecipeText;

    private void Start() {
        successfulRecipeText = GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component
        UpdateSuccessfulRecipeText();

        // Subscribe to the OnRecipeSuccess event to update the text whenever a recipe is successfully delivered
        KitchenGameManager.Instance.OnRecipeSuccess += (sender, e) => {
            UpdateSuccessfulRecipeText();
        };
    }

    private void UpdateSuccessfulRecipeText() {
        if (KitchenGameManager.Instance != null) {
            successfulRecipeText.text = $"RECIPES MADE = {KitchenGameManager.Instance.successfulDeliveries}";
        }
    }
}
