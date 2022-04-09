# Retro RPG Patterns
A tiny retro action RPG implementation made applying Software Design Patterns to serve as a guide of reusable solutions that can be applied to common problems.

* **🔊 Behavioral Patterns**
    * Define a concrete communication scheme between objects.
* **🐣 Creational Patterns**
    * Create objects, rather than instantiating them directly.
* **✂️ Decoupling Patterns**
    * Split dependencies to ensure that changing a piece of code doesn't require changing another one.
* **🛠️ Optimization Patterns**
    * Speed up the game by pushing the hardware to the furthest.
* **⏰ Sequencing Patterns**
    * Invent time and craft the gears that drive the game's great clock.
* **🧬 Structural Patterns**
    * Use inheritance to compose interfaces and define ways to compose objects to obtain new functionality.

## 🔊 Behavioral Patterns
Define a concrete communication scheme between objects.

<details>
   <summary><b>🔊 Bytecode</b></summary>
  
   ### Bytecode
   Give a behavior the flexibility of data by encoding it as instructions for a virtual machine.

   > Unity has this pattern already built-in in its own [Visual Scripting System](https://docs.unity3d.com/2021.1/Documentation/Manual/com.unity.visualscripting.html) (previously named 'Bolt') and in its [Shader Graph System](https://docs.unity3d.com/Manual/shader-graph.html). Unreal has this pattern already built-in too in its [Blueprint Visual Scripting System](https://docs.unrealengine.com/4.27/en-US/ProgrammingAndScripting/Blueprints/). 
</details>

<details>
   <summary><b>🔊 Chain of Responsibility</b></summary>
   
   ### Chain of Responsibility
   Delegates commands to a chain of processing objects.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Chain%20of%20Responsibility.png)   
</details>

<details>
   <summary><b>🔊 Command</b></summary>
   
   ### Command
   Creates objects that encapsulate actions and parameters.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Command.png)
   
   ```csharp
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
   ```
   ```csharp
   public class BikeController : MonoBehaviour
   {
       public enum Direction
       {
           Left = -1,
           Right = 1
       }

       private bool _isTurboOn;
       private const float _distance = 1f;

       public void ToggleTurbo()
       {
           _isTurboOn = !_isTurboOn;
       }

       public void Turn(Direction direction)
       {
           if (direction == Direction.Left)
           {
               transform.Translate(Vector3.left * _distance);
           }
           else if (direction == Direction.Right)
           {
               transform.Translate(Vector3.right * _distance);
           }
       }

       public void ResetPosition()
       {
           transform.position = Vector3.zero;
       }
   }
   ```
   ```csharp
   public class Invoker : MonoBehaviour
   {
       private SortedList<float, Command> _recordedCommands = new SortedList<float, Command>();

       private bool _isRecording;
       private bool _isReplaying;
       private float _replayTime;
       private float _recordingTime;

       public void ExecuteCommand(Command command)
       {
           command.Execute();

           if (_isRecording)
           {
               _recordedCommands.Add(_recordingTime, command);
           }

           Debug.Log("Recorded Time: " + _recordingTime);
           Debug.Log("Recorded Command: " + command);
       }

       public void Record()
       {
           _recordingTime = 0.0f;
           _isRecording = true;
       }

       public void Replay()
       {
           _replayTime = 0.0f;
           _isReplaying = true;

           if (_recordedCommands.Count <= 0)
           {
               Debug.LogError("No commands to replay!");
           }

           _recordedCommands.Reverse();
       }

       private void FixedUpdate()
       {
           if (_isRecording)
           {
               _recordingTime += Time.fixedDeltaTime;
           }

           if (_isReplaying)
           {
               _replayTime += Time.fixedDeltaTime;

               if (_recordedCommands.Any())
               {
                   if (Mathf.Approximately(_replayTime, _recordedCommands.Keys[0]))
                   {
                       Debug.Log("Replay Time: " + _replayTime);
                       Debug.Log("Replay Command: " + _recordedCommands.Values[0]);

                       _recordedCommands.Values[0].Execute();
                       _recordedCommands.RemoveAt(0);
                   }
               }
               else
               {
                   _isReplaying = false;
               }
           }
       }
   }
   ```
   ```csharp
   public abstract class Command
   {
       public abstract void Execute();
   }
   ```                                           
   ```csharp
   public class TurnLeft : Command
   {
       private readonly BikeController _controller;

       public TurnLeft(BikeController controller)
       {
           _controller = controller;
       }

       public override void Execute()
       {
           _controller.Turn(BikeController.Direction.Left);
       }
   }
   ```
   ```csharp
   public class TurnRight : Command
   {
       private readonly BikeController _controller;

       public TurnRight(BikeController controller)
       {
           _controller = controller;
       }

       public override void Execute()
       {
           _controller.Turn(BikeController.Direction.Right);
       }
   }
   ```
   ```csharp
   public class ToggleTurbo : Command
   {
       private readonly BikeController _controller;

       public ToggleTurbo(BikeController controller)
       {
           _controller = controller;
       }

       public override void Execute()
       {
           _controller.ToggleTurbo();
       }
   }
   ```
</details>

<details>
   <summary><b>🔊 Interpreter</b></summary>
   
   ### Interpreter
   Implements a specialized language.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Interpreter.png)
   
   > Similar to the Bytecode pattern, Unity has this pattern already built-in in its own [Visual Scripting System](https://docs.unity3d.com/2021.1/Documentation/Manual/com.unity.visualscripting.html) (previously named 'Bolt') and in its [Shader Graph System](https://docs.unity3d.com/Manual/shader-graph.html). Unreal has this pattern already built-in too in its [Blueprint Visual Scripting System](https://docs.unrealengine.com/4.27/en-US/ProgrammingAndScripting/Blueprints/).
</details>

<details>
   <summary><b>🔊 Iterator</b></summary>
   
   ### Iterator
   Accesses the elements of an object sequentially without exposing its underlying representation.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Iterator.png) 
</details>

<details>
   <summary><b>🔊 Mediator</b></summary>
   
   ### Mediator
   Allows loose coupling between classes by being the only class that has detailed knowledge of their methods.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Mediator.png)
</details>

<details>
   <summary><b>🔊 Memento</b></summary>
   
   ### Memento
   Provides the ability to restore an object to its previous state (undo).
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Memento.png)
   
   > Similar to the State pattern, but with an extra feature that gives objects the ability to roll back to a previous state.
</details>

<details>
   <summary><b>🔊 Observer</b></summary>
   
   ### Observer
   It's a publish/subscribe pattern, which allows a number of observer objects to see an event.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Observer.png)
   
   > Any publish/subscribe structure forms part of this pattern. This way, C# [Delegates](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/), [Actions](https://docs.microsoft.com/en-us/dotnet/api/system.action-1?view=net-6.0), [Event Actions](https://www.reddit.com/r/csharp/comments/m7o16r/what_is_the_difference_between_action_and_event/) and [EventHandlers](https://docs.microsoft.com/en-us/dotnet/api/system.eventhandler?view=net-6.0) are its most basic implementation. [Click Here For A Summary Of All](https://medium.com/nerd-for-tech/c-delegates-actions-events-summary-please-8fab0244a40a). Unity's API has [UnityActions](https://docs.unity3d.com/ScriptReference/Events.UnityAction.html) and [UnityEvents](https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html) which are basically a wrapper of these C# events, but made available through the Inspector. From this point on, the pattern can be expanded to be more or less decoupled until reaching it's final form, which would be a Message or Event Bus System. Here is a basic implementation using Scriptable Objects: [Event Bus System with Scriptable Objects](https://github.com/JoanStinson/SlotsMachine).
   
   ```csharp
   public class ClientObserver : MonoBehaviour
   {
       private BikeController _bikeController;

       private void Start()
       {
           _bikeController = (BikeController)FindObjectOfType(typeof(BikeController));
       }

       private void OnGUI()
       {
           if (GUILayout.Button("Damage Bike") && _bikeController)
           {
               _bikeController.TakeDamage(15.0f);
           }

           if (GUILayout.Button("Toggle Turbo") && _bikeController)
           {
               _bikeController.ToggleTurbo();
           }
       }
   }
   ```
   ```csharp
   public abstract class Subject : MonoBehaviour
   {
       private readonly ArrayList _observers = new ArrayList();

       protected void Attach(Observer observer)
       {
           if (observer != null)
           {
               _observers.Add(observer);
           }
           else
           {
               Debug.LogWarning("Attached observer cannot be null!");
           }
       }

       protected void Detach(Observer observer)
       {
           if (observer != null)
           {
               _observers.Remove(observer);
           }
           else
           {
               Debug.LogWarning("Detached observer cannot be null!");
           }
       }

       protected void NotifyObservers()
       {
           foreach (Observer observer in _observers)
           {
               observer?.Notify(this);
           }
       }
   }
   ```
   ```csharp
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
   ```
   ```csharp
   public abstract class Observer : MonoBehaviour
   {
       public abstract void Notify(Subject subject);
   }
   ```
   ```csharp
   public class CameraController : Observer
   {
       [SerializeField]
       private float _shakeMagnitude = 0.1f;
       private bool _isTurboOn;
       private Vector3 _initialPosition;
       private BikeController _bikeController;

       private void OnEnable()
       {
           _initialPosition = gameObject.transform.localPosition;
       }

       private void Update()
       {
           if (_isTurboOn)
           {
               Vector3 newRandomPosition = _initialPosition + (Random.insideUnitSphere * _shakeMagnitude);
               transform.localPosition = newRandomPosition;
           }
           else
           {
               transform.localPosition = _initialPosition;
           }
       }

       public override void Notify(Subject subject)
       {
           if (!_bikeController)
           {
               _bikeController = subject.GetComponent<BikeController>();
           }

           if (_bikeController)
           {
               _isTurboOn = _bikeController.IsTurboOn;
           }
       }
   }
   ```
   ```csharp
   public class HUDController : Observer
   {
       private bool _isTurboOn;
       private float _currentHealth;
       private BikeController _bikeController;

       private void OnGUI()
       {
           GUILayout.BeginArea(new Rect(50, 50, 100, 200));
           {
               GUILayout.BeginHorizontal("box");
               GUILayout.Label("Health: " + _currentHealth);
               GUILayout.EndHorizontal();

               if (_isTurboOn)
               {
                   GUILayout.BeginHorizontal("box");
                   GUILayout.Label("Turbo Activated!");
                   GUILayout.EndHorizontal();
               }

               if (_currentHealth <= 50f)
               {
                   GUILayout.BeginHorizontal("box");
                   GUILayout.Label("WARNING: Low Health");
                   GUILayout.EndHorizontal();
               }
           }
           GUILayout.EndArea();
       }

       public override void Notify(Subject subject)
       {
           if (!_bikeController)
           {
               _bikeController = subject.GetComponent<BikeController>();
           }

           if (_bikeController)
           {
               _isTurboOn = _bikeController.IsTurboOn;
               _currentHealth = _bikeController.CurrentHealth;
           }
       }
   }
   ```

</details>

<details>
   <summary><b>🔊 State</b></summary>
   
   ### State
   Allows an object to alter its behavior when its internal state changes.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/State.png)
   
   > Unity has this pattern already built-in in its own [Animation System](https://docs.unity3d.com/Manual/AnimationOverview.html) (also known as 'Mecanim'). Actually, it uses an FSM (Finite State Machine), which uses the State pattern, but with blending and transitions.

   ```csharp
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
   ```
   ```csharp
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
   ```
   ```csharp
   public enum Direction
   {
       Left = -1,
       Right = 1
   }
   ```
   ```csharp
   public class BikeStateContext
   {
       public IBikeState CurrentState { get; set; }

       private readonly BikeController _bikeController;

       public BikeStateContext(BikeController bikeController)
       {
           _bikeController = bikeController;
       }

       public void Transition(IBikeState state)
       {
           CurrentState = state;
           CurrentState.Handle(_bikeController);
       }
   }
   ```
   ```csharp
   public interface IBikeState
   {
       void Handle(BikeController bikeController);
   }
   ```
   ```csharp
   public class BikeStartState : MonoBehaviour, IBikeState
   {
       private BikeController _bikeController;

       public void Handle(BikeController bikeController)
       {
           if (!_bikeController)
           {
               _bikeController = bikeController;
           }

           _bikeController.CurrentSpeed = _bikeController.MaxSpeed;
       }

       private void Update()
       {
           if (_bikeController && _bikeController.CurrentSpeed > 0)
           {
               Vector3 bikeTranslation = Vector3.forward * (_bikeController.CurrentSpeed * Time.deltaTime);
               _bikeController.transform.Translate(bikeTranslation);
           }
       }
   }
   ```
   ```csharp
   public class BikeTurnState : MonoBehaviour, IBikeState
   {
       private Vector3 _turnDirection;
       private BikeController _bikeController;

       public void Handle(BikeController bikeController)
       {
           if (!_bikeController)
           {
               _bikeController = bikeController;
           }

           _turnDirection.x = (float)_bikeController.CurrentTurnDirection;

           if (_bikeController.CurrentSpeed > 0)
           {
               transform.Translate(_turnDirection * _bikeController.TurnDistance);
           }
       }
   }
   ```
   ```csharp
   public class BikeStopState : MonoBehaviour, IBikeState
   {
       private BikeController _bikeController;

       public void Handle(BikeController bikeController)
       {
           if (!_bikeController)
           {
               _bikeController = bikeController;
           }

           _bikeController.CurrentSpeed = 0;
       }
   }
   ```
</details>

<details>
   <summary><b>🔊 Strategy</b></summary>
   
   ### Strategy
   Allows one of a family of algorithms to be selected on-the-fly at runtime.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Strategy.png)
   
   ```csharp
   public class ClientStrategy : MonoBehaviour
   {
       private GameObject _drone;
       private List<IManeuverBehaviour> _components = new List<IManeuverBehaviour>();

       private void OnGUI()
       {
           if (GUILayout.Button("Spawn Drone"))
           {
               SpawnDrone();
           }
       }

       private void SpawnDrone()
       {
           _drone = GameObject.CreatePrimitive(PrimitiveType.Cube);
           _drone.AddComponent<Drone>();
           _drone.transform.position = Random.insideUnitSphere * 10;
           ApplyRandomStrategies();
       }

       private void ApplyRandomStrategies()
       {
           _components.Add(_drone.AddComponent<BoppingManeuver>());
           _components.Add(_drone.AddComponent<FallbackManeuver>());
           _components.Add(_drone.AddComponent<WeavingManeuver>());

           int index = Random.Range(0, _components.Count);
           _drone.GetComponent<Drone>().ApplyStrategy(_components[index]);
       }
   }
   ```
   ```csharp
   public class Drone : MonoBehaviour
   {
       public float Speed = 1f;
       public float MaxHeight = 5f;
       public float WeavingDistance = 1.5f;
       public float FallbackDistance = 20f;

       private Vector3 _rayDirection;
       private const float _rayAngle = -45f;
       private const float _rayDistance = 15f;

       private void Awake()
       {
           _rayDirection = transform.TransformDirection(Vector3.back) * _rayDistance;
           _rayDirection = Quaternion.Euler(_rayAngle, 0f, 0f) * _rayDirection;
       }


       private void Update()
       {
           Debug.DrawRay(transform.position, _rayDirection, Color.blue);

           if (Physics.Raycast(transform.position, _rayDirection, out var hitInfo, _rayDistance) && hitInfo.collider)
           {
               Debug.DrawRay(transform.position, _rayDirection, Color.green);
           }
       }

       public void ApplyStrategy(IManeuverBehaviour strategy)
       {
           strategy.Maneuver(this);
       }
   }
   ```
   ```csharp
   public interface IManeuverBehaviour
   {
       void Maneuver(Drone drone);
   }
   ```
   ```csharp
   public class BoppingManeuver : MonoBehaviour, IManeuverBehaviour
   {
       public void Maneuver(Drone drone)
       {
           StartCoroutine(Bopple(drone));
       }

       private IEnumerator Bopple(Drone drone)
       {
           float time;
           bool isReverse = false;
           float speed = drone.Speed;
           Vector3 startPosition = drone.transform.position;
           Vector3 endPosition = startPosition;
           endPosition.y = drone.MaxHeight;

           while (true)
           {
               time = 0;
               Vector3 start = drone.transform.position;
               Vector3 end = (isReverse) ? startPosition : endPosition;

               while (time < speed)
               {
                   drone.transform.position = Vector3.Lerp(start, end, time / speed);
                   time += Time.deltaTime;
                   yield return null;
               }

               yield return new WaitForSeconds(1);
               isReverse = !isReverse;
           }
       }
   }
   ```
   ```csharp
   public class FallbackManeuver : MonoBehaviour, IManeuverBehaviour
   {
       public void Maneuver(Drone drone)
       {
           StartCoroutine(Fallback(drone));
       }

       private IEnumerator Fallback(Drone drone)
       {
           float time = 0;
           float speed = drone.Speed;
           Vector3 startPosition = drone.transform.position;
           Vector3 endPosition = startPosition;
           endPosition.z = drone.FallbackDistance;

           while (time < speed)
           {
               drone.transform.position = Vector3.Lerp(startPosition, endPosition, time / speed);
               time += Time.deltaTime;
               yield return null;
           }
       }
   }
   ```
   ```csharp
   public class WeavingManeuver : MonoBehaviour, IManeuverBehaviour
   {
       public void Maneuver(Drone drone)
       {
           StartCoroutine(Weave(drone));
       }

       private IEnumerator Weave(Drone drone)
       {
           float time;
           bool isReverse = false;
           float speed = drone.Speed;
           Vector3 startPosition = drone.transform.position;
           Vector3 endPosition = startPosition;
           endPosition.x = drone.WeavingDistance;

           while (true)
           {
               time = 0;
               Vector3 start = drone.transform.position;
               Vector3 end = (isReverse) ? startPosition : endPosition;

               while (time < speed)
               {
                   drone.transform.position = Vector3.Lerp(start, end, time / speed);
                   time += Time.deltaTime;
                   yield return null;
               }

               yield return new WaitForSeconds(1);
               isReverse = !isReverse;
           }
       }
   }
   ```
</details>

<details>
   <summary><b>🔊 Subclass Sandbox</b></summary>
   
   ### Subclass Sandbox
   Defines the behavior in a subclass using a set of operations provided by its base class.
</details>

<details>
   <summary><b>🔊 Template Method</b></summary>
   
### Template Method
   Defines the skeleton of an algorithm as an abstract class, allowing its subclasses to provide concrete behavior.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Template%20Method.png)
   
   > This is basically the definition of polymorphism.
</details>

<details>
   <summary><b>🔊 Type Object</b></summary>
   
   ### Type Object
   Allows a flexible creation of new “classes” by creating a single class, each instance of which represents a different type of object.
</details>

<details>
   <summary><b>🔊 Visitor</b></summary>
   
   ### Visitor
   Separates an algorithm from an object structure by moving the hierarchy of methods into one object.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Visitor.png)
   
   ```csharp
   public class ClientVisitor : MonoBehaviour
   {
       [SerializeField] private PowerUpVisitor _enginePowerUp;
       [SerializeField] private PowerUpVisitor _shieldPowerUp;
       [SerializeField] private PowerUpVisitor _weaponPowerUp;

       private BikeController _bikeController;

       private void Awake()
       {
           _bikeController = gameObject.AddComponent<BikeController>();
       }

       private void OnGUI()
       {
           if (GUILayout.Button("PowerUp Shield"))
           {
               _bikeController.Accept(_shieldPowerUp);
           }

           if (GUILayout.Button("PowerUp Engine"))
           {
               _bikeController.Accept(_enginePowerUp);
           }

           if (GUILayout.Button("PowerUp Weapon"))
           {
               _bikeController.Accept(_weaponPowerUp);
           }
       }
   }
   ```
   ```csharp
   public interface IBikeElementVisitor
   {
       void Visit(BikeShieldVisitable bikeShield);
       void Visit(BikeEngineVisitable bikeEngine);
       void Visit(BikeWeaponVisitable bikeWeapon);
   }
   ```
   ```csharp
   [CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]
   public class PowerUpVisitor : ScriptableObject, IBikeElementVisitor
   {
       public string PowerupName;
       public GameObject PowerupPrefab;
       public string PowerupDescription;

       [Tooltip("Fully heal shield")]
       public bool HealShield;

       [Range(0f, 50f)]
       [Tooltip("Boost turbo settings up to increments of 50/mph")]
       public float TurboBoost;

       [Range(0f, 25)]
       [Tooltip("Boost weapon range in increments of up to 25 units")]
       public int WeaponRange;

       [Range(0.0f, 50f)]
       [Tooltip("Boost weapon strength in increments of up to 50%")]
       public float WeaponStrength;

       public void Visit(BikeShieldVisitable bikeShield)
       {
           if (HealShield)
           {
               bikeShield.HealtPercentage = 100f;
           }
       }

       public void Visit(BikeWeaponVisitable bikeWeapon)
       {
           int range = bikeWeapon.Range += WeaponRange;
           bikeWeapon.Range = (range >= bikeWeapon.MaxRange) ? bikeWeapon.MaxRange : range;

           float strength = bikeWeapon.Strength += Mathf.Round(bikeWeapon.Strength * WeaponStrength / 100);
           bikeWeapon.Strength = (strength >= bikeWeapon.MaxStrength) ? bikeWeapon.MaxStrength : strength;
       }

       public void Visit(BikeEngineVisitable bikeEngine)
       {
           float boost = bikeEngine.TurboBoostInMph += TurboBoost;

           if (boost < 0.0f)
           {
               bikeEngine.TurboBoostInMph = 0.0f;
           }
           else if (boost >= bikeEngine.MaxTurboBoost)
           {
               bikeEngine.TurboBoostInMph = bikeEngine.MaxTurboBoost;
           }
       }
   }
   ```
   ```csharp
   public class BikeController : MonoBehaviour, IBikeElementVisitable
   {
       private List<IBikeElementVisitable> _bikeElements = new List<IBikeElementVisitable>();

       private void Awake()
       {
           _bikeElements.Add(gameObject.AddComponent<BikeShieldVisitable>());
           _bikeElements.Add(gameObject.AddComponent<BikeWeaponVisitable>());
           _bikeElements.Add(gameObject.AddComponent<BikeEngineVisitable>());
       }

       public void Accept(IBikeElementVisitor visitor)
       {
           foreach (IBikeElementVisitable element in _bikeElements)
           {
               element.Accept(visitor);
           }
       }
   }
   ```
   ```csharp
   public interface IBikeElementVisitable
   {
       void Accept(IBikeElementVisitor visitor);
   }
   ```
   ```csharp
   public class BikeShieldVisitable : MonoBehaviour, IBikeElementVisitable
   {
       public float HealtPercentage = 50f;

       public float Damage(float damage)
       {
           return HealtPercentage -= damage;
       }

       public void Accept(IBikeElementVisitor visitor)
       {
           visitor.Visit(this);
       }

       private void OnGUI()
       {
           GUI.color = Color.green;
           GUI.Label(new Rect(125, 0, 200, 20), "Shield Health: " + HealtPercentage);
       }
   }
   ```
   ```csharp
   public class BikeWeaponVisitable : MonoBehaviour, IBikeElementVisitable
   {
       [Header("Range")]
       public int Range = 5;
       public int MaxRange = 25;

       [Header("Strength")]
       public float Strength = 25f;
       public float MaxStrength = 50f;

       public void Fire()
       {
           Debug.Log("Weapon fired!");
       }

       public void Accept(IBikeElementVisitor visitor)
       {
           visitor.Visit(this);
       }

       private void OnGUI()
       {
           GUI.color = Color.green;
           GUI.Label(new Rect(125, 40, 200, 20), "Weapon Range: " + Range);
           GUI.Label(new Rect(125, 60, 200, 20), "Weapon Strength: " + Strength);
       }
   }
   ```
   ```csharp
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
   ```
</details>

## 🐣 Creational Patterns
Create objects, rather than instantiating them directly.

<details>
   <summary><b>🐣 Abstract Factory</b></summary>
   
   ### Abstract Factory
   Groups object factories that have a common theme.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Creational%20Patterns/Abstract%20Factory.png)
</details>

<details>
   <summary><b>🐣 Builder</b></summary>
   
   ### Builder
   Constructs complex objects by separating construction and representation.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Creational%20Patterns/Builder.png)
</details>

<details>
   <summary><b>🐣 Factory Method</b></summary>
   
   ### Factory Method
   Creates objects without specifying the exact class to create.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Creational%20Patterns/Factory%20Method.png)
</details>

<details>
   <summary><b>🐣 Prototype</b></summary>
   
   ### Prototype
   Creates objects by cloning an existing object.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Creational%20Patterns/Prototype.png)
   
   > Unity has this pattern already built-in in its [Prefabs System](https://docs.unity3d.com/Manual/Prefabs.html). When using the [GameObject.Instantiate](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html) method it clones the original object (a prefab) and returns a clone (which is spawned in the current scene with the '(Clone)' suffix).
   ```csharp
   public class PrefabInstantiater : MonoBehaviour
   {
       [SerializeField]
       private Transform _prefab;
       
       private void Start()
       {
           for (int i = 0; i < 10; ++i)
           {
               Instantiate(_prefab, new Vector3(i * 2f, 0, 0), Quaternion.identity);
           }
       }
   }
   ```
</details>

<details>
   <summary><b>🐣 Singleton</b></summary>
   
   ### Singleton
   Restricts object creation for a class to only one instance.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Creational%20Patterns/Singleton.png)
      
   > This is a [project killer pattern](https://cocoacasts.com/are-singletons-bad)! It's the prohibited pattern which shall never be named (except in game jams). Instead of using singletons, program to an interface (not to an implementation) and if you use a DI framework to fill these dependencies even better. I highly recommend using [Zenject](https://assetstore.unity.com/packages/tools/utilities/extenject-dependency-injection-ioc-157735). Dependency Inversion Principle > Singleton.
   ```csharp
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool _shuttingDown = false;
        private static readonly object _lock = new object();
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_shuttingDown)
                {
                    Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed. Returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (_instance == null)
                        {
                            var singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<T>();
                            singletonObject.name = $"{typeof(T)} (Singleton)";
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return _instance;
                }
            }
        }

        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        private void OnDestroy()
        {
            _shuttingDown = true;
        }
    }
   ```
   ```csharp
    public sealed class UIManager : MonoBehaviourSingleton<UIManager>
    {
        public void ShowPanel<T>() where T : BasePanel
        {
            // show panel if it exists
        }

        public void HidePanel<T>() where T : BasePanel
        {
            // hide panel if it exists
        }
    }
   ```
   ```csharp
    public class ControlsMenuPanel : BasePanel
    {
        private void ShowOptionsMenu()
        {
             UIManager.Instance.HidePanel<MainMenuPanel>();
             UIManager.Instance.ShowPanel<OptionsMenuPanel>();
        }
    }
   ```
</details>

## ✂️ Decoupling Patterns
Split dependencies to ensure that changing a piece of code doesn't require changing another one.
   
<details>
   <summary><b>✂️ Component</b></summary>
   
   ### Component
   Allows a single entity to span multiple domains without coupling the domains to each other.
   
   > Unity has this pattern already built-in in its own [Component System](https://docs.unity3d.com/ScriptReference/Component.html).
   ```csharp
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Creature : MonoBehaviour, IEntity
    {
        protected Animator _animator;
        protected AudioSource _audioSource;
        protected Rigidbody2D _rigidbody2D;

        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
   ```
</details>

<details>
   <summary><b>✂️ Event Queue</b></summary>
   
   ### Event Queue
   Decouples when an event is sent and when it is executed.
</details>

<details>
   <summary><b>✂️ Service Locator</b></summary>
   
   ### Service Locator
   Provides global access to services without being attached to the concrete class.
</details>

## 🛠️ Optimization Patterns
Speed up the game by pushing the hardware to the furthest.

<details>
   <summary><b>🛠️ Data Locality</b></summary>
   
   ### Data Locality
   Accelerates memory access by arranging data to take advantage of CPU caching.
</details>

<details>
   <summary><b>🛠️ Dirty Flag</b></summary>
   
   ### Dirty Flag
   Avoids unnecessary work by deferring it until the result is needed.
</details>

<details>
   <summary><b>🛠️ Object Pool</b></summary>
   
   ### Object Pool
   Allows the recycling of objects and optimizes performance and memory.
</details>

<details>
   <summary><b>🛠️ Spatial Partition</b></summary>
   
   ### Spatial Partition
   Locates objects efficiently by storing them in a data structure organized by their positions.
   
   > Unity has this pattern already built-in in its own [Frustum Culling System](https://forum.unity.com/threads/frustum-culling.2752/). It uses an octree for culling objects.
</details>

## ⏰ Sequencing Patterns
Invent time and craft the gears that drive the game's great clock.
   
<details>
   <summary><b>⏰ Double Buffer</b></summary>
   
   ### Double Buffer
   Causes a series of sequential operations to appear instantaneous or simultaneous.
   
   > Unity has this pattern already built-in in its own [Rendering System](https://answers.unity.com/questions/203931/double-buffering.html). It uses 2 or even more buffers by native implementation.
</details>

<details>
   <summary><b>⏰ Game Loop</b></summary>
   
   ### Game Loop
   Decouples the progression of game time from user input and processor speed.
   
   > Unity has this pattern already built-in in its own [Execution System](https://docs.unity3d.com/Manual/ExecutionOrder.html).
   
   > Here is a C++ implementation I made in the past.
   ```cpp
   int main() 
   {
      while (!world.IsGameOver()) 
      {
         getline(cin, input);
         vector<string> words = Globals::split(input);

         if (ShouldExit())
         {
            break;
         }

         world.HandleInput(words);
      }
   }
   ```
</details>
   
<details>
   <summary><b>⏰ Update Method</b></summary>
   
   ### Update Method
   Simulates a collection of independent objects by telling each to process one frame of behavior at a time.
   
   > Unity has this pattern already built-in in its [MonoBehaviour](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) base class, from which every Unity script derives.
   ```csharp
   public class NewBehaviourScript : MonoBehaviour
   {
       // Start is called before the first frame update
       private void Start()
       {

       }

       // Update is called once per frame
       private void Update()
       {

       }
   }
   ```
</details>

## 🧬 Structural Patterns
Use inheritance to compose interfaces and define ways to compose objects to obtain new functionality.
   
<details>
   <summary><b>🧬 Adapter</b></summary>
   
   ### Adapter
   Allows classes with incompatible interfaces to work together by wrapping its own interface around that of an already existing class.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Adapter.png)
   
   ```csharp
   public class ClientAdapter : MonoBehaviour
   {
       [SerializeField]
       private InventoryItem _item;
       private InventorySystem _inventorySystem;
       private IInventorySystem _inventorySystemAdapter;

       private void Awake()
       {
           _inventorySystem = new InventorySystem();
           _inventorySystemAdapter = new InventorySystemAdapter();
       }

       private void OnGUI()
       {
           if (GUILayout.Button("Add item (no adapter)"))
           {
               _inventorySystem.AddItem(_item);
           }

           if (GUILayout.Button("Add item (with adapter)"))
           {
               _inventorySystemAdapter.AddItem(_item, SaveLocation.Both);
           }
       }
   }
   ```
   ```csharp
   public interface IInventorySystem
   {
       void SyncInventories();
       void AddItem(InventoryItem item, SaveLocation location);
       void RemoveItem(InventoryItem item, SaveLocation location);
       List<InventoryItem> GetInventory(SaveLocation location);
   }
   ```
   ```csharp
   [CreateAssetMenu(fileName = "New Item", menuName = "Inventory")]
   public class InventoryItem : ScriptableObject
   {
       // Placeholder class
   }
   ```
   ```csharp
   public class InventorySystem
   {
       public void AddItem(InventoryItem item)
       {
           Debug.Log("Adding item to the cloud");
       }

       public void RemoveItem(InventoryItem item)
       {
           Debug.Log("Removing item from the cloud");
       }

       public List<InventoryItem> GetInventory()
       {
           Debug.Log("Returning an inventory list stored in the cloud");
           return new List<InventoryItem>();
       }
   }
   ```
   ```csharp
   public class InventorySystemAdapter : InventorySystem, IInventorySystem
   {
       private List<InventoryItem> _cloudInventory;

       public void SyncInventories()
       {
           var _cloudInventory = GetInventory();
           Debug.Log("Synchronizing local drive and cloud inventories");
       }

       public void AddItem(InventoryItem item, SaveLocation location)
       {
           if (location == SaveLocation.Cloud)
           {
               AddItem(item);
           }
           else if (location == SaveLocation.Local)
           {
               Debug.Log("Adding item to local drive");
           }
           else if (location == SaveLocation.Both)
           {
               Debug.Log("Adding item to local drive and on the cloud");
           }
       }

       public void RemoveItem(InventoryItem item, SaveLocation location)
       {
           Debug.Log("Remove item from local/cloud/both");
       }

       public List<InventoryItem> GetInventory(SaveLocation location)
       {
           Debug.Log("Get inventory from local/cloud/both");
           return new List<InventoryItem>();
       }
   }
   ```
   ```csharp
   public enum SaveLocation
   {
       Local,
       Cloud,
       Both
   }
   ```
</details>

<details>
   <summary><b>🧬 Bridge</b></summary>
   
   ### Bridge
   Decouples an abstraction from its implementation so that the two can vary independently.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Bridge.png)
</details>

<details>
   <summary><b>🧬 Composite</b></summary>
   
   ### Composite
   Composes zero-or-more similar objects so that they can be manipulated as one object.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Composite.png)
</details>

<details>
   <summary><b>🧬 Decorator</b></summary>
   
   ### Decorator
   Dynamically adds/overrides behavior in an existing method of an object.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Decorator.png)
</details>

<details>
   <summary><b>🧬 Facade</b></summary>
   
   ### Facade
   Provides a simplified interface to a large body of code.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Facade.png)
   
   > The Facade pattern establishes a new interface, whereas the Adapter pattern adapts an old interface.
</details>

<details>
   <summary><b>🧬 Flyweight</b></summary>
   
   ### Flyweight
   Reduces the cost of creating and manipulating a large number of similar objects.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Flyweight.png)
      
   > Unity has this pattern already built-in in its [Prefabs System](https://docs.unity3d.com/Manual/Prefabs.html) by referencing the data of 1 prefab to instantiate multiple objects that are similar reducing memory usage and the same goes for the [Scriptable Objects System](https://docs.unity3d.com/Manual/class-ScriptableObject.html) as if multiple prefabs reference the same scriptable object, only 1 scriptable object reference will be used for all prefabs (less copies equals less memory).
</details>

<details>
   <summary><b>🧬 Proxy</b></summary>
   
   ### Proxy
   Provides a placeholder for another object to control access, reduce cost, and reduce complexity.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Proxy.png)
</details>
