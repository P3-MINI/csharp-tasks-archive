# **Lab14: Synchronization**

## **Introduction**

> _Who doesn't love pizza? This Italian specialty has won hearts worldwide, and its popularity poses significant challenges for pizzerias. To satisfy customer appetites, pizzerias must efficiently handle multiple orders simultaneously. Proper synchronization is key – after all, nobody likes waiting too long for their favorite slice! In today's task, we'll create a simulation of a pizzeria's operation, based on the classic producer-consumer problem._

<p align="center">
  <img src="../Common/img/pizzeria.jpg" alt="pizzeria.jpg" style="width: 80%;"/>
</p>

## **Stage 1: (2 points)**

> _Imagine a pizzeria with a special queue where ready-to-deliver pizzas wait for drivers. We'll create such a data structure to ensure it works efficiently and safely in a multithreaded environment._

Your task is to create a `ParallelQueue<T>` class and implement the `IParallelQueue<T>` interface provided in the `IParallelQueue.cs` file.

The data structure will act as a queue with a maximum capacity defined in the constructor. The queue should allow elements to be enqueued and dequeued safely in a multithreaded environment.

**Note:** Do not use prebuilt classes like `ConcurrentQueue<T>` from `System.Collections.Concurrent` or `Queue<T>` from `System.Collections.Generic` to store the queue's elements internally.

### **Hints**:

- Use two [semaphores](https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=net-9.0&devlangs=csharp) to synchronize enqueue and dequeue operations.
- Ensure proper [resource disposal](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable?view=net-9.0) for synchronization primitives.

## **Stage 2: (2 points)**

> _Even the best pizzerias occasionally experience delays – sometimes there’s a dough shortage, or drivers encounter logistical issues. Our task is to implement functionality to temporarily pause and resume queue operations._

Extend the `ParallelQueue<T>` class to include functionalities defined in the `IPausableQueue<T>` interface (found in the `IPausableQueue.cs` file).

Methods starting with `Pause` should prevent all threads from enqueuing or dequeuing elements. Methods starting with `Resume` should re-enable operations previously paused. A newly created queue should allow all operations by default.

### **Hints**:

- Use [ManualResetEventSlim](https://learn.microsoft.com/en-us/dotnet/api/system.threading.manualreseteventslim?view=net-9.0&devlangs=csharp) to implement pause and resume functionalities.

## **Stage 3: (2 points)**

> _The smell of freshly baked pizza fills the air. It’s time for the chefs and delivery drivers to get to work!_

Your task is to expand the simulation in the `Pizzeria` class to reflect the operation of a pizzeria. `StartSimulationAsync` method has already been implemented and needs no changes. In `StartChefs` and `StartDeliverers` create two groups of `Task`s to represent chefs preparing pizzas and delivery drivers delivering them.

#### **Chef threads**:

- Number of chefs: `ChefsCount`.
- Each chef generates a pizza using the `GeneratePizzaOrder()` method in a loop.
- After preparing a pizza, the chef waits a random number of milliseconds between `[1000, 2000]` (baking takes time!).
- A ready pizza is added to the queue using `TryEnqueueAsync`. If this fails within one second, the chef discards the order (nobody wants cold pizza).

#### **Deliverer threads**:

- Number of deliverers: `DeliverersCount`.
- Deliverers attempt to dequeue a pizza from the queue using `TryDequeueAsync`. Like the chefs, they wait up to one second.
- If a pizza is successfully dequeued, the deliverer "delivers" it by waiting a random number of milliseconds between `[1000, 2000]`.

## **Stage 4: (2 points)**

> _The pizzeria is buzzing with activity, but every shift has its end. Let’s add support for a shift-based work system and daily income reporting._

#### **Shift-based work**:

- Each pizzeria worker completes `_dailyPizzaOrders` orders during their shift.
- After finishing their shift, workers wait for the others to wrap up to summarize the daily income.
- Use (and modify in the constructor the creation of) the `barrier` object (which is a private field of the `Pizzeria` class) for synchronization.
- Both the chefs and the deliverers, before starting the next workday, wait for the `b` key to be pressed by the user controlling the simulation (see _Simulation Controls_).
- Store income information in the `_dailyIncomeDictionary`. The dictionary keys can be pizza names, and the values are the respective revenues.
- Once all workers finish their shifts, print the income summary to the console.

#### **Ending the simulation**:

- Add logic to stop all pizzeria workers using the `cancellationToken` (which is a parameter of both `StartChefs` and `StartDeliverers`).

### **Hints**:

- [ConcurrentDictionary<TKey,TValue> Class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=net-9.0)
- [How to: Synchronize Concurrent Operations with a Barrier](https://learn.microsoft.com/en-us/dotnet/standard/threading/how-to-synchronize-concurrent-operations-with-a-barrier)

## **Simulation Controls**

You can control the simulation using the following keys (control logic is already implemented):

```
Control:
  p - Pause pizza preparation
  r - Resume pizza preparation
  k - Pause delivery
  l - Resume delivery
  b - Sum up daily income
  q - Exit
```
