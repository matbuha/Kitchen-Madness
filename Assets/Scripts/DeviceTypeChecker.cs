using System.Runtime.InteropServices;
using UnityEngine;

public class DeviceTypeChecker : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern int GetDeviceType();
    public GameObject interact;
    public GameObject interactAlternate;
    public GameObject movementAndroidButtons;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            int deviceType = GetDeviceType(); // Call the function from the JS Plugin
            if (deviceType == 1)
            {
                // Enable or disable UI elements accordingly for Mobile
                interact.SetActive(true);
                interactAlternate.SetActive(true);
                movementAndroidButtons.SetActive(true);
            }
            else
            {
                // Enable or disable UI elements accordingly for Desktop
                interact.SetActive(false);
                interactAlternate.SetActive(false);
                movementAndroidButtons.SetActive(false);
            }
        }
    }
}
