using UnityEngine;

namespace JGM.Patterns.Command
{
    public class InputHandler : MonoBehaviour
    {
        private Invoker _invoker;
        private BikeController _bikeController;
        private Command _turnLeftCommand;
        private Command _turnRightCommand;
        private Command _toggleTurboCommand;
        private bool _isReplaying;
        private bool _isRecording;

        private void Awake()
        {
            _invoker = gameObject.AddComponent<Invoker>();
            _bikeController = FindObjectOfType<BikeController>();
            _turnLeftCommand = new TurnLeft(_bikeController);
            _turnRightCommand = new TurnRight(_bikeController);
            _toggleTurboCommand = new ToggleTurbo(_bikeController);
        }

        private void Update()
        {
            if (!_isReplaying && _isRecording)
            {
                if (Input.GetKeyUp(KeyCode.A))
                {
                    _invoker.ExecuteCommand(_turnLeftCommand);
                }

                if (Input.GetKeyUp(KeyCode.D))
                {
                    _invoker.ExecuteCommand(_turnRightCommand);
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    _invoker.ExecuteCommand(_toggleTurboCommand);
                }
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Start Recording"))
            {
                _bikeController.ResetPosition();
                _isReplaying = false;
                _isRecording = true;
                _invoker.Record();
            }

            if (GUILayout.Button("Stop Recording"))
            {
                _bikeController.ResetPosition();
                _isRecording = false;
            }

            if (!_isRecording && GUILayout.Button("Start Replay"))
            {
                _bikeController.ResetPosition();
                _isRecording = false;
                _isReplaying = true;
                _invoker.Replay();
            }
        }
    }
}