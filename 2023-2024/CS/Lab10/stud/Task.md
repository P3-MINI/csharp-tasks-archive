## Lambdas and Delegates
The task consists of 5 stages.  
The stages can be completed in any order.  
Each stage is worth 1 point.  
The stages are defined in the files:
- BinaryTree.cs
- Benchmarking.cs
- DictionaryExtensions.cs
- EnumerableExtensions.cs
- Numerics.cs

### Binary Tree
Implement a method `BreadthFirstSearch` (BFS https://en.wikipedia.org/wiki/Breadth-first_search), that takes a function of built-in type Action (called onVisit) with argument of type Node, as a parameter.
The method traverses the tree in a level order and calls `onVisit` action when it encounters a `Node`.  
Pseudocode of the generic BFS on a binary tree:
```
Queue Q = Queue.Empty
Q.Enqueue(Root)
while(Q not empty)
  Node N = Q.Dequeue()
  if(N.Left) Q.Enqueue(N.Left)
  if(N.Right) Q.Enqueue(N.Right)
```
Then implement a `ValueCount` method that takes a value of type T as a parameter.
It traverses the tree using a previously implemented `BreadthFirstSearch` method and counts how many times the value appears in a tree.

### Benchmarking
Implement a method `CreateBenchmark`, that creates a benchmarking wrapper around a given Action to measure and report the average execution time over a specified number of iterations.
This method takes the following parameters:
- an Action delegate `action`
- a string representing the name of the benchmark `name`
- and an optional parameter `times` (default set to 1000)

It returns an anonymous function of an `Action` delegate type. The returned `Action` delegate, when invoked, measures the average execution time of the provided `action` by running it in a loop for the specified number of `times`. It uses the `Stopwatch` class to accurately measure the elapsed time.
After the loop, the method calculates and prints the average time taken for a single execution of the `action` in milliseconds. The output includes the name of the function `name` and the average time per execution in a format:
```
"Function `name`: `elapsed` ms on average"
```
Then implement method `CreateBenchmarks` using previously implemented method, that create 3 benchmarks comparing performance of the 3 methods of concatenating a string (defined in the same file): `StringAdd`, `StringBuilder`, and `StringJoin`.  
Relevant `Stopwatch` functions and properties:
- `new Stopwatch()`
- `stopwatch.Start()`
- `stopwatch.Stop()`
- `stopwatch.Elapsed.TotalMilliseconds`

### Dictionary
Implement a method `Merge<TKey, TValue>` - an extension method for the `IDictionary` interface, that merges an existing value associated with a given key in the dictionary with a given value.  
If the specified key is not already associated with a value or is associated with `null`, the method associates it with the given as parameter non-`null` value. 
Otherwise, it replaces the associated value with the results of the given remapping function, or removes if the result of remapping is `null`.  

The method takes the following parameters:
- `TKey` `key` - The key of the entry to be merged or added,
- `TValue` `value` - the non-`null` value to be merged with the existing value associated with the `key` or, if no existing value or a `null` value is associated with the key, to be associated with the key,
- `Func` `remappingFunction` - the function to recompute a value if present, based on old value and `value`  

It returns the new value associated with the specified `key`, or `null` if no value is associated with the `key`.  

Then implement the `MergeLogs` method using the previously defined Merge method, which merges two dictionaries. In case of a conflicting key in both dictionaries, update the value in the source dictionary by concatenating values from both dictionaries (use `string.Concat`).

### Enumerable
Implement method `Collect<TSource, TAccumulate>` - an extension method for the `IEnumerable` interface, that provides a generic mechanism for accumulating elements from an enumerable source into a result, allowing flexibility in how the accumulation is performed by providing a custom accumulator action.  

The method takes the following parameters:
- `TAccumulate` `collection` - a result container.
- `Action` `accumulator` - an associative, non-interfering, stateless function for incorporating an additional element into a result, that takes an argument of a `TAccumulate` type and an argument of a `TSource` type with no return value.   

It returns the result of the reduction.  

Implement a method `Filter<T>` - an extension method for the `IEnumerable` interface, that takes a `Predicate` `predicate` parameter. The method iterates through each element in the source collection and yields only those elements for which the specified `predicate` evaluates to `true`. It returns an `IEnumerable<T>` containing the filtered elements.  
Then implement a method `ConcatStrings`, that filters the elements of enumeration, keeping only those that are of type `string`, using the `Filter` method. The filtered strings are then concatenated into a single `string` using previously implemented `Collect` method and `StringBuilder`. Finally, the method returns the resulting concatenated string.

### Numerics
Declare a delegate `Function` that takes one double parameter `x` and returns one double number.
Implement a method `HalleyRootFinding`, that performs the Halley method for root finding. It takes a `Function f` and optional first and second derivatives (`Function df` and `ddf` respectively with `null` as a default value), an initial guess (`x0`) with a default value `0`, and a tolerance (`eps`) with a default value `1e-6`.  
If the first or the second derivative was not provided (`null` value) initialize them with the following estimation: `df = (f(x + eps) - f(x - eps)) / (2 * eps)`.
Pseudocode of the Halley root finding procedure (https://en.wikipedia.org/wiki/Halley%27s_method):
```
Halley(f, df, ddf, x0, eps):
  xn = x0
  do
    x = xn
    xn = x - 2 * f(x) * df(x) / (2 * df(x) * df(x) - f(x) * ddf(x))
  while |x - xn| > eps
  return xn
```
Then implement `FindLinearRoot`, `FindQuadraticRoot`, and `FindLogRoot` functions that find a root of the following functions using Halley's method:
- `FindLinearRoot`: `f(x) = 2x - 1`, also specify the exact first and second derivatives. Keep the default starting point and epsilon. 
- `FindQuadraticRoot`: `f(x) = 2x^2 + 3x + 1`, keep all parameters of Halley's method at default value. 
- `FindLinearRoot`: `f(x) = log(x)`, specify the exact first derivative, but not the second. Use `2` as a starting point (`x0`). Keep the default epsilon value. Pass the functions as a references to `double.Log` and `double.ReciprocalEstimate` methods.
