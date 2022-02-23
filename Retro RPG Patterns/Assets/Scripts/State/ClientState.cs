using UnityEngine;

namespace JGM.Game.State
{
    [RequireComponent(typeof(BikeController))]
    public class ClientState : MonoBehaviour
    {
        private BikeController _bikeController;

        private void Awake()
        {
            _bikeController = GetComponent<BikeController>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Start Bike"))
            {
                _bikeController.StartBike();
            }

            if (GUILayout.Button("Turn Left"))
            {
                _bikeController.Turn(Direction.Left);
            }

            if (GUILayout.Button("Turn Right"))
            {
                _bikeController.Turn(Direction.Right);
            }

            if (GUILayout.Button("Stop Bike"))
            {
                _bikeController.StopBike();
            }
        }
    }
}