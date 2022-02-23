using UnityEngine;
using System.Collections;

namespace JGM.Game.EventBus
{
    public class CountdownTimer : MonoBehaviour, IRaceEventSubscriber
    {
        private float _currentTime;
        private float _durationInSeconds = 3.0f;

        public void OnEnable()
        {
            RaceEventBus.Subscribe(RaceEventType.Countdown, StartTimer);
        }

        public void OnDisable()
        {
            RaceEventBus.Unsubscribe(RaceEventType.Countdown, StartTimer);
        }

        private void StartTimer()
        {
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            _currentTime = _durationInSeconds;

            while (_currentTime > 0)
            {
                yield return new WaitForSeconds(1f);
                _currentTime--;
            }

            RaceEventBus.Publish(RaceEventType.Start);
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(125, 0, 100, 20), "COUNTDOWN: " + _currentTime);
        }
    }
}