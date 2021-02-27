using UnityEngine;

namespace Loop.Data
{
    [CreateAssetMenu(fileName = "RoundData", menuName = "Loop/RoundData", order = 0)]
    public class RoundData : ScriptableObject
    {
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private int currentRound = 1;
        [SerializeField] private int roundsPerLevel = 3;
        [SerializeField] private int maxLevels = 3;

        public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
        public int CurrentRound { get => currentRound; set => currentRound = value; }
        public int RoundsPerLevel { get => roundsPerLevel; }
        public int MaxLevels { get => maxLevels; }
    }
}