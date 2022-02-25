using UnityEngine;

namespace JGM.Patterns.Visitor
{
    public class BikeEngineVisitable : MonoBehaviour, IBikeElementVisitable
    {
        public float TurboBoostInMph = 25f;
        public float MaxTurboBoost = 200f;

        private const float _defaultSpeedInMph = 300f;
        private bool _isTurboOn;

        public float CurrentSpeed
        {
            get
            {
                return (_isTurboOn) ? _defaultSpeedInMph + TurboBoostInMph : _defaultSpeedInMph;
            }
        }

        public void ToggleTurbo()
        {
            _isTurboOn = !_isTurboOn;
        }

        public void Accept(IBikeElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(125, 20, 200, 20), "Turbo Boost: " + TurboBoostInMph);
        }
    }
}