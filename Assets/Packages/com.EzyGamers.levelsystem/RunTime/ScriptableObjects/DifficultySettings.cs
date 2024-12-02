using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "LevelSystem/DifficultySettings")]
public class DifficultySettings : ScriptableObject
{
    public int mediumThreshold = 5;
    public int hardThreshold = 10;

    public DifficultyLevelSettings easySettings;
    public DifficultyLevelSettings mediumSettings;
    public DifficultyLevelSettings hardSettings;
}
