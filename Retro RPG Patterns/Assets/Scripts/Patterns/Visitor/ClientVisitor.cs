using UnityEngine;

namespace JGM.Patterns.Visitor
{
    public class ClientVisitor : MonoBehaviour
    {
        [SerializeField] private PowerUpVisitor _enginePowerUp;
        [SerializeField] private PowerUpVisitor _shieldPowerUp;
        [SerializeField] private PowerUpVisitor _weaponPowerUp;

        private BikeController _bikeController;

        private void Awake()
        {
            _bikeController = gameObject.AddComponent<BikeController>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("PowerUp Shield"))
            {
                _bikeController.Accept(_shieldPowerUp);
            }

            if (GUILayout.Button("PowerUp Engine"))
            {
                _bikeController.Accept(_enginePowerUp);
            }

            if (GUILayout.Button("PowerUp Weapon"))
            {
                _bikeController.Accept(_weaponPowerUp);
            }
        }
    }
}