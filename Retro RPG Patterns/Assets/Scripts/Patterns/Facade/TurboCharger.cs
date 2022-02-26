using UnityEngine;
using System.Collections;

namespace JGM.Patterns.Facade
{
    public class TurboCharger : MonoBehaviour
    {
        public BikeEngine engine;

        private bool _isTurboOn;
        private CoolingSystem _coolingSystem;

        public void ToggleTurbo(CoolingSystem coolingSystem)
        {
            _coolingSystem = coolingSystem;
            if (!_isTurboOn)
            {
                StartCoroutine(TurboCharge());
            }
        }

        private IEnumerator TurboCharge()
        {
            _isTurboOn = true;
            _coolingSystem.PauseCooling();

            yield return new WaitForSeconds(engine.turboDuration);

            _isTurboOn = false;
            _coolingSystem.PauseCooling();
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(100, 60, 500, 20), "Turbo Activated: " + _isTurboOn);
        }
    }
}