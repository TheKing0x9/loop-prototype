using UnityEngine;

namespace Loop.Player
{
    public class KeyboardInput : BaseInput 
    {
        private KeyCode _left = KeyCode.LeftArrow;
        private KeyCode _right = KeyCode.RightArrow;

        protected override void Update()
        {
            if (Input.GetKeyDown(_left))
                _input = -1;
            else if (Input.GetKeyDown(_right))
                _input = 1;
            else
                _input = 0;

            SetInput();
        } 
    }   
}