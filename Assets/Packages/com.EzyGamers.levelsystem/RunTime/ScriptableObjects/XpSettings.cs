using UnityEngine;

[CreateAssetMenu(fileName = "XPSettings", menuName = "LevelSystem/XPSettings")]
public class XpSettings : ScriptableObject
{
    public int[] xpThresholds;

    public int GetXPThreshold(int level)
    {
        return level - 1 < xpThresholds.Length ? xpThresholds[level - 1] : xpThresholds[xpThresholds.Length - 1];
    }
}
