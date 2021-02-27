using UnityEngine;

namespace Loop.AI
{
    public class DumbAI : BaseAI
    {
        protected override void Act()
        {
            Vector3 targetPosition = _target.position;
            Vector3 position = transform.position;
            Vector3 diff = targetPosition - position;

            if (diff.y < 0)
                return;

            float angle = Vector3.SignedAngle(position, targetPosition, Vector3.forward);

            Debug.Log(angle);

            var sign = Mathf.Sign(angle);
            SetPlayerInput(-sign);
        }
    }
}
