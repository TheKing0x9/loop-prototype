using UnityEngine;

namespace Loop.UI
{
    [System.Serializable]
    struct Animatable
    {
        public RectTransform transform;
        public Vector3 initPosition;
        public Vector3 finalPosition;
    }
}