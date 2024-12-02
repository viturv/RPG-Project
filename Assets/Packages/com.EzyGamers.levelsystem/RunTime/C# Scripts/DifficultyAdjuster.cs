using UnityEngine;
using TMPro;

public class DifficultyAdjuster : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty currentDifficulty;

    [Header("Settings")]
    public float minCompletionTime = 10f; // Minimum time threshold to complete the level
    public float difficultyIncrementThreshold = 1.2f; // The multiplier to increase enemy speed and spawn rate

    [Header("UI Elements")]
    public TextMeshProUGUI difficultyText; // Reference to Difficulty UI Text

    public DifficultySettings difficultySettings; // Reference to DifficultySettings (ScriptableObject)

    private float levelStartTime; // Time when the level starts
    private float levelEndTime; // Time when the level ends

    void Start()
    {
        UpdateDifficultyText(); // Initialize the UI
    }

    public void StartLevel()
    {
        levelStartTime = Time.time; // Log the start time
        Debug.Log($"Level started at: {levelStartTime}");
    }

    public void EndLevel()
    {
        levelEndTime = Time.time; // Log the end time
        Debug.Log($"Level ended at: {levelEndTime}");

        float completionTime = levelEndTime - levelStartTime; // Calculate the level completion time
        Debug.Log($"Level completed in: {completionTime} seconds");

        UpdateDifficulty(completionTime); // Adjust difficulty based on completion time
    }

    // Adjust difficulty based on completion time
    public void UpdateDifficulty(float levelCompletionTime)
    {
        //Debug.Log($"Level completed in {levelCompletionTime} seconds.");

        // If the player completes the level faster than the required minimum time
        if (levelCompletionTime < minCompletionTime)
        {
            // Increase difficulty if not already at max difficulty
            if (currentDifficulty == Difficulty.Easy)
            {
                currentDifficulty = Difficulty.Medium;
            }
            else if (currentDifficulty == Difficulty.Medium)
            {
                currentDifficulty = Difficulty.Hard;
            }
            else if (currentDifficulty == Difficulty.Hard)
            {
                // Custom logic to increase difficulty beyond Hard
                IncreaseBeyondHard();
            }

            // Decrease the threshold for the next level completion time
            minCompletionTime += (difficultyIncrementThreshold/2f);
            minCompletionTime = Mathf.Max(minCompletionTime, 1f); // Ensure the minimum threshold doesn't go below 1 second

            Debug.Log($"New difficulty: {currentDifficulty}, New completion time threshold: {minCompletionTime}");
        }
        else
        {
            Debug.Log($"Difficulty remains at: {currentDifficulty} because completion time was {levelCompletionTime} seconds.");
        }

        // Update the UI only if the difficulty has changed
        UpdateDifficultyText();
    }

    // Increase difficulty beyond Hard
    private void IncreaseBeyondHard()
    {
        Debug.Log("Player is performing exceptionally well! Increasing additional challenges.");

        // Fetch the current settings based on the current difficulty
        DifficultyLevelSettings currentLevelSettings = GetDifficultySettings(currentDifficulty);

        // Multiply spawnRate and enemySpeed by the difficultyIncrementThreshold
        currentLevelSettings.spawnRate *= difficultyIncrementThreshold;
        currentLevelSettings.enemySpeed *= difficultyIncrementThreshold;

        Debug.Log($"New Spawn Rate: {currentLevelSettings.spawnRate}, New Enemy Speed: {currentLevelSettings.enemySpeed}");

        // You can also add logic to further customize gameplay, such as:
        // - Adding more enemy types
        // - Increasing spawn density
        // - Modifying power-ups or rewards

        // If you want to persist these changes or reflect them in the gameplay, you can apply these changes to relevant gameplay systems
    }

    // Get the difficulty level settings based on the current difficulty
    private DifficultyLevelSettings GetDifficultySettings(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return difficultySettings.easySettings;
            case Difficulty.Medium:
                return difficultySettings.mediumSettings;
            case Difficulty.Hard:
                return difficultySettings.hardSettings;
            default:
                return difficultySettings.easySettings; // Default to Easy if something goes wrong
        }
    }

    // Update the difficulty text in the UI
    public void UpdateDifficultyText()
    {
        if (difficultyText != null)
        {
            difficultyText.text = $"Current Difficulty: {currentDifficulty}";
        }
        else
        {
            Debug.LogWarning("DifficultyText is not assigned in the Inspector.");
        }
    }
}
