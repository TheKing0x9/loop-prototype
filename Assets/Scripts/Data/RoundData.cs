using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Loop/RoundData", order = 0)]
public class RoundData : ScriptableObject 
{
    private int currentLevel = 1;
    private int currentRound = 1;
    private int roundsPerLevel = 3;
    private int maxLevels = 3;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public int CurrentRound { get => currentRound; set => currentRound = value; }
    public int RoundsPerLevel { get => roundsPerLevel; }
    public int MaxLevels { get => maxLevels; }
}