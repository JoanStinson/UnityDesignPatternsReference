using UnityEngine;

namespace JGM.Patterns.EventBus
{
    public class HUDController : MonoBehaviour, IRaceEventSubscriber
    {
        private bool _isDisplayOn;

        public void OnEnable()
        {
            RaceEventBus.Subscribe(RaceEventType.Start, DisplayHUD);
        }

        public void OnDisable()
        {
            RaceEventBus.Unsubscribe(RaceEventType.Start, DisplayHUD);
        }

        private void DisplayHUD()
        {
            _isDisplayOn = true;
        }

        private void OnGUI()
        {
            if (_isDisplayOn && GUILayout.Button("Stop Race"))
            {
                _isDisplayOn = false;
                RaceEventBus.Publish(RaceEventType.Stop);
            }
        }
    }
}