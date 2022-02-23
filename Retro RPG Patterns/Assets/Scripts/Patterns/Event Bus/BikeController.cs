using UnityEngine;

namespace JGM.Patterns.EventBus
{
    public class BikeController : MonoBehaviour, IRaceEventSubscriber
    {
        private string _status;

        public void OnEnable()
        {
            RaceEventBus.Subscribe(RaceEventType.Start, StartBike);
            RaceEventBus.Subscribe(RaceEventType.Stop, StopBike);
        }

        public void OnDisable()
        {
            RaceEventBus.Unsubscribe(RaceEventType.Start, StartBike);
            RaceEventBus.Unsubscribe(RaceEventType.Stop, StopBike);
        }

        private void StartBike()
        {
            _status = "Started";
        }

        private void StopBike()
        {
            _status = "Stopped";
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(10, 60, 200, 20), "BIKE STATUS: " + _status);
        }
    }
}