using UnityEngine;

namespace JGM.Patterns.Strategy
{
    public class Drone : MonoBehaviour
    {
        public float Speed = 1f;
        public float MaxHeight = 5f;
        public float WeavingDistance = 1.5f;
        public float FallbackDistance = 20f;

        private Vector3 _rayDirection;
        private const float _rayAngle = -45f;
        private const float _rayDistance = 15f;

        private void Awake()
        {
            _rayDirection = transform.TransformDirection(Vector3.back) * _rayDistance;
            _rayDirection = Quaternion.Euler(_rayAngle, 0f, 0f) * _rayDirection;
        }


        private void Update()
        {
            Debug.DrawRay(transform.position, _rayDirection, Color.blue);

            if (Physics.Raycast(transform.position, _rayDirection, out var hitInfo, _rayDistance) && hitInfo.collider)
            {
                Debug.DrawRay(transform.position, _rayDirection, Color.green);
            }
        }

        public void ApplyStrategy(IManeuverBehaviour strategy)
        {
            strategy.Maneuver(this);
        }
    }
}