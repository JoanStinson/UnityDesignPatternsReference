using UnityEngine;

namespace JGM.Game.State.States
{
    public class BikeStartState : MonoBehaviour, IBikeState
    {
        private BikeController _bikeController;

        public void Handle(BikeController bikeController)
        {
            if (!_bikeController)
            {
                _bikeController = bikeController;
            }

            _bikeController.CurrentSpeed = _bikeController.MaxSpeed;
        }

        private void Update()
        {
            if (_bikeController && _bikeController.CurrentSpeed > 0)
            {
                Vector3 bikeTranslation = Vector3.forward * (_bikeController.CurrentSpeed * Time.deltaTime);
                _bikeController.transform.Translate(bikeTranslation);
            }
        }
    }
}