using UnityEngine;
using TMPro;  // Make sure to use this namespace for Text Mesh Pro

public class MissionGoalUI : MonoBehaviour {

    private TextMeshProUGUI missionGoalText;

    private void Start() {
        missionGoalText = GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component
        UpdateMissionGoalText();
    }

    private void UpdateMissionGoalText() {
        if (KitchenGameManager.Instance != null) {
            // Format the text with the successfulDeliveriesGoal value
            missionGoalText.text = $"DELIVER {KitchenGameManager.Instance.successfulDeliveriesGoal} SUCCESSFUL RECIPES";
        }
    }
}
