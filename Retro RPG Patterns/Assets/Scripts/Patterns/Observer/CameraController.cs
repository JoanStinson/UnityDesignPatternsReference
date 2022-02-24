using UnityEngine;

namespace JGM.Patterns.Observer
{
    public class CameraController : Observer
    {
        [SerializeField]
        private float _shakeMagnitude = 0.1f;
        private bool _isTurboOn;
        private Vector3 _initialPosition;
        private BikeController _bikeController;

        private void OnEnable()
        {
            _initialPosition = gameObject.transform.localPosition;
        }

        private void Update()
        {
            if (_isTurboOn)
            {
                Vector3 newRandomPosition = _initialPosition + (Random.insideUnitSphere * _shakeMagnitude);
                transform.localPosition = newRandomPosition;
            }
            else
            {
                transform.localPosition = _initialPosition;
            }
        }

        public override void Notify(Subject subject)
        {
            if (!_bikeController)
            {
                _bikeController = subject.GetComponent<BikeController>();
            }

            if (_bikeController)
            {
                _isTurboOn = _bikeController.IsTurboOn;
            }
        }
    }
}