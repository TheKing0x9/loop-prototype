using UnityEngine;

namespace Loop.Player
{
    [RequireComponent(typeof(Player))]
    public abstract class BaseInput : MonoBehaviour
    {
        protected float _input;

        private Player _player;

        private void Awake() 
        {
            _player = GetComponent <Player>();    
            _input = 0;
        }

        protected abstract void Update();

        protected void SetInput()
        {
            _player.SetInput(_input);
        }
    }

}

