using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider xpSlider;      // Reference to the UI Slider
    public XpSystem xpSystem;    // Reference to the XP System

    void Start()
    {
        if (xpSlider == null || xpSystem == null)
        {
            Debug.LogError("XPSlider or XPSystem is not assigned in the inspector.");
            return;
        }

        // Initialize the slider value at the start
        xpSlider.value = 0;  // Start with a clean state
    }

    void Update()
    {
        if (xpSystem != null && xpSlider != null)
        {
            // Calculate the progress (0 to 1 scale)
            float progress = (float)xpSystem.CurrentXP / xpSystem.GetCurrentLevelXPThreshold();
            progress = Mathf.Clamp01(progress); // Ensure value is between 0 and 1

            // Log what we're trying to set the slider to
            Debug.Log($"Setting Slider value to: {progress}");

            // Set the slider value to progress
            xpSlider.value = progress;
        }
    }
}
