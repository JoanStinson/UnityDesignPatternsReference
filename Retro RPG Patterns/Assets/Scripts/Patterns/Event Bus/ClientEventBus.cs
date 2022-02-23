using UnityEngine;

namespace JGM.Patterns.EventBus
{
    public class ClientEventBus : MonoBehaviour, IRaceEventSubscriber
    {
        private bool _isButtonEnabled;

        private void Awake()
        {
            _isButtonEnabled = true;
            gameObject.AddComponent<HUDController>();
            gameObject.AddComponent<CountdownTimer>();
            gameObject.AddComponent<BikeController>();
        }

        public void OnEnable()
        {
            RaceEventBus.Subscribe(RaceEventType.Stop, Restart);
        }

        public void OnDisable()
        {
            RaceEventBus.Unsubscribe(RaceEventType.Stop, Restart);
        }

        private void Restart()
        {
            _isButtonEnabled = true;
        }

        private void OnGUI()
        {
            if (_isButtonEnabled && GUILayout.Button("Start Countdown"))
            {
                _isButtonEnabled = false;
                RaceEventBus.Publish(RaceEventType.Countdown);
            }
        }
    }
}