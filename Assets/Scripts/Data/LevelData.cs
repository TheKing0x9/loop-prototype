using UnityEngine;

namespace Loop.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Loop/LevelData", order = 0)]
    public class LevelData : ScriptableObject 
    {
        [SerializeField] private string _name;
        [SerializeField] private string _desc;
        [SerializeField] private Material _groundMaterial;

        public string Name { get => _name; }
        public string Desc { get => _desc; }
        public Material GroundMaterial { get => _groundMaterial; }
    }
}