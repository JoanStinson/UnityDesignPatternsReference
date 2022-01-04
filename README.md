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
* ### Chain of Responsibility
   Delegates commands to a chain of processing objects.
* ### Command
   Creates objects that encapsulate actions and parameters.
* ### Interpreter
   Implements a specialized language.
* ### Iterator
   Accesses the elements of an object sequentially without exposing its underlying representation.
* ### Mediator
   Allows loose coupling between classes by being the only class that has detailed knowledge of their methods.
* ### Memento
   Provides the ability to restore an object to its previous state (undo).
* ### Observer
   It's a publish/subscribe pattern, which allows  a number of observer objects to see an event.
* ### State
   Allows an object to alter its behavior when its internal state changes.

   > Unity has this pattern already built-in in its [Animation System](https://docs.unity3d.com/Manual/AnimationOverview.html). 
   ```csharp
    [RequiredByNativeCode]
    public abstract class StateMachineBehaviour : ScriptableObject
    {
        protected StateMachineBehaviour();

        public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash);
        public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller);
        
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

        public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash);
        public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller);
    }
   ```
   ```csharp
   public class NewStateMachineBehaviour : StateMachineBehaviour
   {
       // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
       override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
       {

       }

       // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
       override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
       {

       }

       // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
       override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
       {

       }

       // OnStateMove is called right after Animator.OnAnimatorMove()
       override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
       {
           // Implement code that processes and affects root motion
       }

       // OnStateIK is called right after Animator.OnAnimatorIK()
       override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
       {
           // Implement code that sets up animation IK (inverse kinematics)
       }
   }
   ```
   > Here is a C++ implementation I made in the past of a FSM using function pointer style.
   ```cpp
   template <typename T>
   class EnemyState
   {
      public:
         EnemyState() { }
         EnemyState(T* enemy) : enemy(enemy) { }
         virtual ~EnemyState() = default;

         virtual void OnStateEnter() { }
         virtual void OnStateUpdate() { }
         virtual void OnStateExit() { }

         void Exit(EnemyState* state)
         {
            enemy->current_state->OnStateExit();
            enemy->current_state = state;
            enemy->current_state->OnStateEnter();
         }

      protected:
         T* const enemy = nullptr;
   };
   ```
   ```cpp
   class Mushdoom
   {
      public:
         Mushdoom()
         {
            current_state = new MushdoomState();
            idle_state = new MushdoomStateIdle(this);
            attack_state = new MushdoomStateAttack(this);
         }
         ~Mushdoom();

         void Update()
         {
            _currentState->OnStateUpdate();
         }

      public:
         typedef EnemyState<Mushdoom> MushdoomState;
         MushdoomState* current_state = nullptr;
         MushdoomState* idle_state = nullptr;
         MushdoomState* attack_state = nullptr;
   };
   ```
   ```cpp
   class MushdoomStateIdle : public EnemyState<Mushdoom>
   {
      public:
         MushdoomStateIdle(Mushdoom* enemy);
         ~MushdoomStateIdle() = default;

         // Have its own implementation
         void OnStateEnter() override;
         void OnStateUpdate() override;
         void OnStateExit() override;
   };
   ```
   ```cpp
   class MushdoomStateAttack : public EnemyState<Mushdoom>
   {
      public:
         MushdoomStateAttack(Mushdoom* enemy);
         ~MushdoomStateAttack() = default;

         // Have its own implementation
         void OnStateEnter() override;
         void OnStateUpdate() override;
         void OnStateExit() override;
   };
   ```
   
* ### Strategy
   Allows one of a family of algorithms to be selected on-the-fly at runtime.
* ### Subclass Sandbox
   Defines the behavior in a subclass using a set of operations provided by its base class.
* ### Template Method
   Defines the skeleton of an algorithm as an abstract class, allowing its subclasses to provide concrete behavior.
* ### Type Object
   Allows a flexible creation of new “classes” by creating a single class, each instance of which represents a different type of object.
* ### Visitor
   Separates an algorithm from an object structure by moving the hierarchy of methods into one object.

## 🐣 Creational Patterns
Create objects, rather than instantiating them directly.
* ### Abstract Factory
   Groups object factories that have a common theme.
* ### Builder
   Constructs complex objects by separating construction and representation.
* ### Factory Method
   Creates objects without specifying the exact class to create.
* ### Prototype
   Creates objects by cloning an existing object.
   
   > Unity has this pattern already built-in in its [Prefabs](https://docs.unity3d.com/Manual/Prefabs.html) system. When using the [Object.Instantiate](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html) method it clones the original object (a prefab) and returns a clone (which is spawned in the current scene with the '(Clone)' suffix).
   ```csharp
   public class ExampleClass : MonoBehaviour
   {
       public Transform prefab;
       
       private void Start()
       {
           // Instantiates 10 copies of Prefab each 2 units apart from each other
           for (int i = 0; i < 10; ++i)
           {
               Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
           }
       }
   }
   ```
* ### Singleton
   Restricts object creation for a class to only one instance.

## ✂️ Decoupling Patterns
Split dependencies to ensure that changing a piece of code doesn't require changing another one.
* ### Component
   Allows a single entity to span multiple domains without coupling the domains to each other.
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

## ⏰ Sequencing Patterns
Invent time and craft the gears that drive the game's great clock.
* ### Double Buffer
   Causes a series of sequential operations to appear instantaneous or simultaneous.
* ### Game Loop
   Decouples the progression of game time from user input and processor speed.
   
   > Unity has this pattern already built-in in its own [internal system](https://docs.unity3d.com/Manual/ExecutionOrder.html).
   
   > Here are some C++ implementations I made in the past.
   ```cpp
   int main() 
   {
      cout << "WELCOME TO ZORK!" << endl;
      srand(static_cast<unsigned int>(time(NULL)));

      World world;
      string input;

      while (!world.IsGameOver()) 
      {
         // Get input
         getline(cin, input);

         // Split string to words
         vector<string> words = Globals::split(input);

         // Exit
         if (words.size() > 0 && (ACTION_EXIT == Globals::toLowercase(words.at(0)) || ACTION_QUIT == Globals::toLowercase(words.at(0))))
            break;

         // Parse command
         world.HandleInput(words);
      }

      cout << "Thanks for playing!" << endl;
      system("pause");
      return 0;
   }
   ```
   ```cpp
   enum class MainState
   {
      CREATION,
      START,
      UPDATE,
      FINISH,
      EXIT
   };

   Application* App = nullptr;

   int main(int argc, char** argv)
   {
      srand(static_cast<unsigned>(time(NULL)));

      int main_return = EXIT_FAILURE;
      MainState state = MainState::CREATION;

      while (state != MainState::EXIT)
      {
         switch (state)
         {
            case MainState::CREATION:
               LOG("Application Creation --------------");

               App = new Application();
               state = MainState::START;
               break;

            case MainState::START:
               LOG("Application Init --------------");

               if (!App->Init())
               {
                  LOG("Application Init exits with error -----");
                  state = MainState::EXIT;
               }
               else
               {
                  state = MainState::UPDATE;
                  LOG("Application Update --------------");
               }
               break;

            case MainState::UPDATE:
            {
               UpdateStatus update_return = App->Update();

               if (update_return == UpdateStatus::ERRORS)
               {
                  LOG("Application Update exits with error -----");
                  state = MainState::EXIT;
               }

               if (update_return == UpdateStatus::STOP)
                  state = MainState::FINISH;
            }
            break;

            case MainState::FINISH:
               LOG("Application CleanUp --------------");

               if (!App->CleanUp())
               {
                  LOG("Application CleanUp exits with error -----");
               }
               else
               {
                  main_return = EXIT_SUCCESS;
               }

               state = MainState::EXIT;
               break;
         }
      }

      delete App;
      _CrtDumpMemoryLeaks();
      return main_return;
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
   > Here is a C++ implementation I made in the past.
   ```cpp
   bool Application::Init()
   {
      bool ret = true;

      for (auto it = modules.begin(); it != modules.end() && ret; ++it)
         ret = (*it)->Init(); 

      for (auto it = modules.begin(); it != modules.end() && ret; ++it)
         if ((*it)->IsEnabled())
            ret = (*it)->Start();

      // Start the first scene 
      fade->FadeToBlack(scene_menu.get(), nullptr, 3.f);

      return ret;
   }

   UpdateStatus Application::Update()
   {
      UpdateStatus ret = UpdateStatus::CONTINUE;

      for (auto it = modules.begin(); it != modules.end() && ret == UpdateStatus::CONTINUE; ++it)
         if ((*it)->IsEnabled())
            ret = (*it)->PreUpdate();

      for (auto it = modules.begin(); it != modules.end() && ret == UpdateStatus::CONTINUE; ++it)
         if ((*it)->IsEnabled())
            ret = (*it)->Update();

      for (auto it = modules.begin(); it != modules.end() && ret == UpdateStatus::CONTINUE; ++it)
         if ((*it)->IsEnabled())
            ret = (*it)->PostUpdate();

      return ret;
   }

   bool Application::CleanUp()
   {
      bool ret = true;

      for (auto it = modules.rbegin(); it != modules.rend() && ret; ++it)
            ret = (*it)->CleanUp();

      return ret;
   }
   ```

## 🧬 Structural Patterns
Use inheritance to compose interfaces and define ways to compose objects to obtain new functionality.
* ### Adapter
   Allows classes with incompatible interfaces to work together by wrapping its own interface around that of an already existing class.
* ### Bridge
   Decouples an abstraction from its implementation so that the two can vary independently.
* ### Composite
   Composes zero-or-more similar objects so that they can be manipulated as one object.
* ### Decorator
   Dynamically adds/overrides behavior in an existing method of an object.
* ### Facade
   Provides a simplified interface to a large body of code.
* ### Flyweight
   Reduces the cost of creating and manipulating a large number of similar objects.
* ### Proxy
   Provides a placeholder for another object to control access, reduce cost, and reduce complexity.
