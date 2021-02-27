using UnityEngine;
using Loop.Players;

namespace Loop.AI
{
    [RequireComponent(typeof(Player))]
    public abstract class BaseAI : MonoBehaviour
    {
        [SerializeField] protected float _actionCountDown = 3f;
        [SerializeField] protected LayerMask _groundMask;
        [SerializeField] protected float _raycastDistance = 2f;
        [SerializeField] protected float _sideRayDistance = 0.2f;

        private Player _player;
        protected float _currentTime;
        protected Transform _target;

        protected void SetPlayerInput(float input)
        {
            _player.SetInput(input);
        }

        protected abstract void Act();

        protected virtual void ResetTimer()
        {
            _currentTime = _actionCountDown;
        }

        protected void Awake() 
        {
            ResetTimer();    
            _player = GetComponent<Player>();
            _target = GameObject.FindGameObjectWithTag("Target").transform;
        }

        protected void Update()
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0)
            {
                ResetTimer();
                Act();
            }

            bool result = Physics.Raycast(transform.position, -Vector3.up, _raycastDistance, _groundMask.value);
            if (result)
                Act();

            result = Physics.Raycast(transform.position, -Vector3.right, _sideRayDistance, _groundMask);
            if (result)
                SetPlayerInput(1);

            result = Physics.Raycast(transform.position, Vector3.right, _sideRayDistance, _groundMask);
            if (result)
                SetPlayerInput(-1);
        }
    }
}