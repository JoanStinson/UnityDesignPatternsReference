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
   Is a publish/subscribe pattern, which allows  a number of observer objects to see an event.
* ### State
   Allows an object to alter its behavior when its internal state changes.
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
* ### Update Method
   Simulates a collection of independent objects by telling each to process one frame of behavior at a time.

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
