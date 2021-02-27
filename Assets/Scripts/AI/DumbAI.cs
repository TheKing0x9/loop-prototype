using UnityEngine;
using Random = UnityEngine.Random;

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

            int random = Random.Range(1, 2);

            if (random == 1)
                SetPlayerInput(1);
            else
                SetPlayerInput(2);
        }
    }
}
