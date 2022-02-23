# Retro RPG Patterns
A tiny retro action RPG implementation made applying Software Design Patterns to serve as a guide of solutions that can be applied to specific problems.

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
* ### Bytecode
   Give a behavior the flexibility of data by encoding it as instructions for a virtual machine.
   
   > Unity has this pattern already built-in in its own [Visual Scripting System](https://docs.unity3d.com/2021.1/Documentation/Manual/com.unity.visualscripting.html) (previously named 'Bolt') and in its [Shader Graph System](https://docs.unity3d.com/Manual/shader-graph.html). Unreal has this pattern already built-in too in its [Blueprint Visual Scripting System](https://docs.unrealengine.com/4.27/en-US/ProgrammingAndScripting/Blueprints/). 
* ### Chain of Responsibility
   Delegates commands to a chain of processing objects.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Chain%20of%20Responsibility.png)
* ### Command
   Creates objects that encapsulate actions and parameters.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Command.png)
* ### Interpreter
   Implements a specialized language.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Interpreter.png)
   
   > Unity has this pattern already built-in in its own [Visual Scripting System](https://docs.unity3d.com/2021.1/Documentation/Manual/com.unity.visualscripting.html) (previously named 'Bolt') and in its [Shader Graph System](https://docs.unity3d.com/Manual/shader-graph.html). Unreal has this pattern already built-in too in its [Blueprint Visual Scripting System](https://docs.unrealengine.com/4.27/en-US/ProgrammingAndScripting/Blueprints/).
* ### Iterator
   Accesses the elements of an object sequentially without exposing its underlying representation.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Iterator.png)
* ### Mediator
   Allows loose coupling between classes by being the only class that has detailed knowledge of their methods.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Mediator.png)
* ### Memento
   Provides the ability to restore an object to its previous state (undo).
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Memento.png)
* ### Observer
   It's a publish/subscribe pattern, which allows  a number of observer objects to see an event.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Observer.png)
* ### State
   Allows an object to alter its behavior when its internal state changes.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/State.png)
   
   > Unity has this pattern already built-in in its own [Animation System](https://docs.unity3d.com/Manual/AnimationOverview.html) (also known as 'Mecanim'). 
   ```csharp
    [RequiredByNativeCode]
    public abstract class StateMachineBehaviour : ScriptableObject
    {
        protected StateMachineBehaviour();

        public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash);
        public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller);
        public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash);
        public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller);
        
        public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller);
        public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller);
        public virtual void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        public virtual void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller);
        
        public virtual void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        public virtual void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller);
        public virtual void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        public virtual void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller);
    }
   ```
   ```csharp
   public class EnemyIdle : StateMachineBehaviour
   {
       public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
       {
            animator.SetBool("canPursuePlayer", true);
       }

       override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
       {
            WanderRandomlyThroughMap();
            LookAtPlayerAtRandomIntervals();
       }
   }
   ```
* ### Strategy
   Allows one of a family of algorithms to be selected on-the-fly at runtime.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Strategy.png)
* ### Subclass Sandbox
   Defines the behavior in a subclass using a set of operations provided by its base class.
* ### Template Method
   Defines the skeleton of an algorithm as an abstract class, allowing its subclasses to provide concrete behavior.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Template%20Method.png)
* ### Type Object
   Allows a flexible creation of new “classes” by creating a single class, each instance of which represents a different type of object.
* ### Visitor
   Separates an algorithm from an object structure by moving the hierarchy of methods into one object.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Behavioral%20Patterns/Visitor.png)

## 🐣 Creational Patterns
Create objects, rather than instantiating them directly.
* ### Abstract Factory
   Groups object factories that have a common theme.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Creational%20Patterns/Abstract%20Factory.png)
* ### Builder
   Constructs complex objects by separating construction and representation.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Creational%20Patterns/Builder.png)
* ### Factory Method
   Creates objects without specifying the exact class to create.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Creational%20Patterns/Factory%20Method.png)
* ### Prototype
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
* ### Singleton
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

## ✂️ Decoupling Patterns
Split dependencies to ensure that changing a piece of code doesn't require changing another one.
* ### Component
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
* ### Event Queue
   Decouples when an event is sent and when it is executed.
* ### Service Locator
   Provides global access to services without being attached to the concrete class.

## 🛠️ Optimization Patterns
Speed up the game by pushing the hardware to the furthest.
* ### Data Locality
   Accelerates memory access by arranging data to take advantage of CPU caching.
* ### Dirty Flag
   Avoids unnecessary work by deferring it until the result is needed.
* ### Object Pool
   Allows the recycling of objects and optimizes performance and memory.
* ### Spatial Partition
   Locates objects efficiently by storing them in a data structure organized by their positions.
   
   > Unity has this pattern already built-in in its own [Frustum Culling System](https://forum.unity.com/threads/frustum-culling.2752/). It uses an octree for culling objects.

## ⏰ Sequencing Patterns
Invent time and craft the gears that drive the game's great clock.
* ### Double Buffer
   Causes a series of sequential operations to appear instantaneous or simultaneous.
   
   > Unity has this pattern already built-in in its own [Rendering System](https://answers.unity.com/questions/203931/double-buffering.html). It uses 2 or even more buffers by native implementation.
* ### Game Loop
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
* ### Update Method
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

## 🧬 Structural Patterns
Use inheritance to compose interfaces and define ways to compose objects to obtain new functionality.
* ### Adapter
   Allows classes with incompatible interfaces to work together by wrapping its own interface around that of an already existing class.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Adapter.png)
* ### Bridge
   Decouples an abstraction from its implementation so that the two can vary independently.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Bridge.png)
* ### Composite
   Composes zero-or-more similar objects so that they can be manipulated as one object.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Composite.png)
* ### Decorator
   Dynamically adds/overrides behavior in an existing method of an object.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Decorator.png)
* ### Facade
   Provides a simplified interface to a large body of code.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Facade.png)
* ### Flyweight
   Reduces the cost of creating and manipulating a large number of similar objects.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Flyweight.png)
      
   > Unity has this pattern already built-in in its [Prefabs System](https://docs.unity3d.com/Manual/Prefabs.html) by referencing the data of 1 prefab to instantiate multiple objects that are similar reducing memory usage and the same goes for the [Scriptable Objects System](https://docs.unity3d.com/Manual/class-ScriptableObject.html) as if multiple prefabs reference the same scriptable object, only 1 scriptable object reference will be used for all prefabs (less copies equals less memory).
* ### Proxy
   Provides a placeholder for another object to control access, reduce cost, and reduce complexity.
   
   ![Diagram](https://github.com/JoanStinson/RetroRPGPatterns/blob/main/Diagrams/Structural%20Patterns/Proxy.png)
