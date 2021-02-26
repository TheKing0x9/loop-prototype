using UnityEngine;
using Loop.Utilities;

namespace Loop.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 200f;
        [Range(0, 90)][SerializeField] private float _angle = 60f;
        private float _input;
        private Vector2 _direction = new Vector3(1, 0).normalized;
        private Rigidbody2D _theRigidbody;

        private void Awake() 
        {
            _direction = _direction.Rotate(_angle);
            _theRigidbody = GetComponent <Rigidbody2D> ();            
        }

        private void FixedUpdate() 
        {
            Vector2 force = new Vector3();
            force.x = _direction.x * _input;
            force.y = _direction.y * Mathf.Abs(_input);

            force *= _jumpForce;

            _theRigidbody.AddForce(force);
        }

        public void SetInput(float input)
        {
            _input = input;
        }
    }   
}