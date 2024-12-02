using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XpSystem : MonoBehaviour
{
    
    [Header("UI Elements")]
    public Slider ProgressBar; // ProgressBar to display XP
    public TextMeshProUGUI XPText; // Text to display XP info

    [Header("Settings")]
    public XpSettings xpSettings; // ScriptableObject with XP thresholds

    private int currentXP = 0; // Current XP value
    private int currentLevel = 1; // Current level

    // Public properties for other components to access
    public int CurrentLevel => currentLevel;
    public int CurrentXP => currentXP;

    // Events for notifying level and XP changes
    public event System.Action<int> OnXPUpdated;
    public event System.Action<int> OnLevelUp;

    void Start()
    {
        UpdateUI();
    }

    public void AddXP(int amount)
    {

        amount = 50;
        if (xpSettings == null)
        {
            Debug.LogError("XPSettings is not assigned! Please link the ScriptableObject.");
            return;
        }

        currentXP += amount;

        // Check for level-up
        while (currentXP >= xpSettings.GetXPThreshold(currentLevel))
        {
            currentXP -= xpSettings.GetXPThreshold(currentLevel);
            currentLevel++;
            OnLevelUp?.Invoke(currentLevel); // Trigger the level-up event
        }

        OnXPUpdated?.Invoke(currentXP); // Notify listeners of XP update
        UpdateUI();
    }

    public void ResetProgress()
    {
        currentXP = 0;
        currentLevel = 1;

        // Trigger events for listeners
        OnXPUpdated?.Invoke(currentXP);
        OnLevelUp?.Invoke(currentLevel);

        UpdateUI();
        Debug.Log("Progress has been reset.");
    }

    public int GetCurrentLevelXPThreshold()
    {
        return xpSettings.GetXPThreshold(currentLevel);
    }

    private void UpdateUI()
    {
        if (ProgressBar != null)
        {
            int xpThreshold = xpSettings.GetXPThreshold(currentLevel);
            ProgressBar.value = (float)currentXP / xpThreshold;
        }

        if (XPText != null)
        {
            XPText.text = $"XP: {currentXP}/{xpSettings.GetXPThreshold(currentLevel)}";
        }
    }
}
