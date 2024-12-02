using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public XpSystem xpSystem; // Reference to the XP system
    public DifficultyAdjuster difficultyAdjuster; // Reference to the DifficultyAdjuster

    [Header("UI Elements")]
    public TextMeshProUGUI levelUpdateText; // UI Text element to display level-up message
    public TextMeshProUGUI currentLevelText; // UI Text element to display the current level

    private float levelStartTime; // Track when the level starts

    void Start()
    {
        if (xpSystem != null)
        {
            xpSystem.OnLevelUp += HandleLevelUp; // Subscribe to level-up events
        }

        // Initialize current level display
        UpdateCurrentLevelUI();

        // Start the first level
        if (difficultyAdjuster != null)
        {
            difficultyAdjuster.StartLevel();
        }
    }

    void OnDestroy()
    {
        if (xpSystem != null)
        {
            xpSystem.OnLevelUp -= HandleLevelUp; // Unsubscribe to prevent memory leaks
        }
    }

    public void StartLevel()
    {
        levelStartTime = Time.time; // Record the start time of the level
        Debug.Log($"Level started at: {levelStartTime}");
    }

    public void CompleteLevel()
    {
        float levelEndTime = Time.time; // Record the end time of the level
        float levelCompletionTime = levelEndTime - levelStartTime; // Calculate time spent on the level

        Debug.Log($"Level completed! Completion time: {levelCompletionTime} seconds");

        // End the current level and adjust difficulty based on completion time
        if (difficultyAdjuster != null)
        {
            difficultyAdjuster.UpdateDifficulty(levelCompletionTime);
        }

        // Prepare for the next level
        if (difficultyAdjuster != null)
        {
            difficultyAdjuster.StartLevel();
        }
    }

    private void HandleLevelUp(int newLevel)
    {
        Debug.Log($"Player leveled up! New Level: {newLevel}");

        // Update level-up message
        if (levelUpdateText != null)
        {
            levelUpdateText.text = $"Level Up! You are now Level {newLevel}";
        }

        // Update the current level display
        UpdateCurrentLevelUI();

        // Explicitly update difficulty in case the level up affects it
        if (difficultyAdjuster != null)
        {
            difficultyAdjuster.UpdateDifficulty(0f); // Pass 0 as we are only focusing on completion time for now
        }

        StartCoroutine(HideLevelUpdateText());
    }

    private void UpdateCurrentLevelUI()
    {
        if (currentLevelText != null)
        {
            currentLevelText.text = $"Level: {xpSystem.CurrentLevel}";
        }
    }

    private System.Collections.IEnumerator HideLevelUpdateText()
    {
        yield return new WaitForSeconds(2f);
        if (levelUpdateText != null)
        {
            levelUpdateText.text = ""; // Clear the message
        }
    }
}
