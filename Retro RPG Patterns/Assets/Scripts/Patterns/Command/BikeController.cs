using UnityEngine;

namespace JGM.Patterns.Command
{
    public class BikeController : MonoBehaviour
    {
        public enum Direction
        {
            Left = -1,
            Right = 1
        }

        private bool _isTurboOn;
        private const float _distance = 1f;

        public void ToggleTurbo()
        {
            _isTurboOn = !_isTurboOn;
        }

        public void Turn(Direction direction)
        {
            if (direction == Direction.Left)
            {
                transform.Translate(Vector3.left * _distance);
            }
            else if (direction == Direction.Right)
            {
                transform.Translate(Vector3.right * _distance);
            }
        }

        public void ResetPosition()
        {
            transform.position = Vector3.zero;
        }
    }
}