using UnityEngine;

namespace Loop.Gameplay
{
    public class LoopManager : MonoBehaviour
    {
        [SerializeField] private Transform _crown;
        [SerializeField] private bool _isPlayer = true;
        private bool _isLeading;
        private Vector3 _entryDir;
        private Rigidbody _theRigidbody;
        private int _score;

        public int Score { get => _score; }

        public void SetIsLeading(bool condition)
        {
            _isLeading = condition;
            //_crown.gameObject.SetActive(_isLeading);
        }

        private void Awake() 
        {
            _theRigidbody = GetComponent<Rigidbody>();
            SetIsLeading(false);
        }

        public void DisableControls()
        {
            _theRigidbody.isKinematic = true;
        }

        public void EnableControls()
        {
            _theRigidbody.isKinematic = false;
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag("Target"))
            {
                _entryDir = transform.up;
            }
        }

        private void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.CompareTag("Target"))
            {
                Vector3 exitDir = transform.up;
                float dot = Vector3.Dot(_entryDir, exitDir);

                if (dot > 0)
                {
                    Debug.Log("score");
                    GameManager.Instance.AddScore(_isPlayer);
                }
            }
        }
    }
}