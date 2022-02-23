using JGM.Game.State.States;
using UnityEngine;

namespace JGM.Game.State
{
    public class BikeController : MonoBehaviour
    {
        [field: SerializeField] public float MaxSpeed { get; private set; } = 2.0f;
        [field: SerializeField] public float TurnDistance { get; private set; } = 2.0f;
        public float CurrentSpeed { get; set; }
        public Direction CurrentTurnDirection { get; private set; }

        private IBikeState _startState;
        private IBikeState _stopState;
        private IBikeState _turnState;

        private BikeStateContext _bikeStateContext;

        private void Awake()
        {
            _bikeStateContext = new BikeStateContext(this);
            _startState = gameObject.AddComponent<BikeStartState>();
            _stopState = gameObject.AddComponent<BikeStopState>();
            _turnState = gameObject.AddComponent<BikeTurnState>();
            _bikeStateContext.Transition(_stopState);
        }

        public void StartBike()
        {
            _bikeStateContext.Transition(_startState);
        }

        public void StopBike()
        {
            _bikeStateContext.Transition(_stopState);
        }

        public void Turn(Direction direction)
        {
            CurrentTurnDirection = direction;
            _bikeStateContext.Transition(_turnState);
        }
    }
}