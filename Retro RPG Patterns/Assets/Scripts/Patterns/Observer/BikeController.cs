using UnityEngine;

namespace JGM.Patterns.Observer
{
    public class BikeController : Subject
    {
        public bool IsTurboOn { get; private set; }
        public float CurrentHealth => health;

        [SerializeField]
        private float health = 100f;
        private CameraController _cameraController;
        private HUDController _hudController;
        private bool _isEngineOn;

        private void Awake()
        {
            _hudController = gameObject.AddComponent<HUDController>();
            _cameraController = (CameraController)FindObjectOfType(typeof(CameraController));
        }

        private void Start()
        {
            StartEngine();
        }

        private void OnEnable()
        {
            Attach(_hudController);
            Attach(_cameraController);
        }

        private void OnDisable()
        {
            Detach(_hudController);
            Detach(_cameraController);
        }

        private void StartEngine()
        {
            _isEngineOn = true;
            NotifyObservers();
        }

        public void ToggleTurbo()
        {
            if (_isEngineOn)
            {
                IsTurboOn = !IsTurboOn;
            }

            NotifyObservers();
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
            IsTurboOn = false;
            NotifyObservers();

            if (health < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}