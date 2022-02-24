using UnityEngine;

namespace JGM.Patterns.Observer
{
    public class ClientObserver : MonoBehaviour
    {
        private BikeController _bikeController;

        private void Start()
        {
            _bikeController = (BikeController)FindObjectOfType(typeof(BikeController));
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Damage Bike") && _bikeController)
            {
                _bikeController.TakeDamage(15.0f);
            }

            if (GUILayout.Button("Toggle Turbo") && _bikeController)
            {
                _bikeController.ToggleTurbo();
            }
        }
    }
}