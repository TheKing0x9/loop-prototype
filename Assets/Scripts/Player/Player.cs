using UnityEngine;
using Loop.Utilities;

namespace Loop.Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 200f;
        [Range(0, 90)][SerializeField] private float _angle = 60f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private Rigidbody _theRigidbody;
        private float _input;
        private Vector2 _direction = new Vector3(1, 0);
        private void Awake() 
        {
            _direction = _direction.Rotate(_angle);   
            _theRigidbody = GetComponent<Rigidbody>();
        }

        private void Update() 
        {
            Vector2 force = new Vector3();
            force.x = _direction.x * _input;
            force.y = _direction.y * Mathf.Abs(_input);

            force *= _jumpForce;
            _theRigidbody.AddForce(force);

            float speed = _theRigidbody.velocity.magnitude;
            if (speed > _maxSpeed)
            {
                _theRigidbody.velocity = _theRigidbody.velocity.normalized * _maxSpeed;
            }

            _input = 0;
        }

        public void SetInput(float input)
        {
            Debug.Log("nhvfld");
            _input = input;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _theRigidbody.velocity = Vector3.zero;
                float angle = _theRigidbody.rotation.eulerAngles.z;
                angle = Mathf.Abs(angle);
                if (angle < 90f) 
                {
                    _theRigidbody.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    _theRigidbody.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
        }
    }   
}