# Programming 3 - Advanced
## Laboratory 15 - Retake

### Stage #1 - Points: 2.0
Implement a method `BreadthFirstSearch` (BFS https://en.wikipedia.org/wiki/Breadth-first_search), that takes a function of built-in type Action (called onVisit) with argument of type Node, as a parameter.
The method traverses the tree in a level order and calls `onVisit` action when it encounters a `Node`.  \
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

### Stage #2 - Points: 2.0
Create a generic ```Polynomial``` class that will implement ```+``` and ```-``` operators (use specific interfaces from the previous stage).  \
Use ```INumber<T>``` type to store the coefficients as a public read-only property.  \
Implement read-only property ```Degree``` that will return a degree of the polynomial (do not store the value).  \
Constructor that takes a degree and initializes coefficients with default values.  \
Constructor that takes **variable** amount of ```T``` type values representing coefficients.  \
Indexer that returns/assigns value at the given index get should return a default value if out of bounds while set should throw argument exception.  \
Hint: Use ```IAdditionOperators``` and ```ISubtractionOperators``` interfaces for ```Polynomial``` class.

### Stage #3 - Points: 1.0
Create a generic extension method ```Evaluate``` for ```Polynomial``` that calculates, **using Horner method**, the value of a given polynomial at a given point.

### Stage #4 - Points: 1.0
Create ```RandomSequence``` class that implements ```IEnumerable<int>``` interface that will return random values from a given range.  \
Create a constructor that will get the instance of the random class you should use for generating numbers and min/max values.

### Stage #5 - Points: 2.0 
Create an extension method for ```FileStream``` called ```WriteMangled``` that gives a string array and random sequence that will mangle the text before outputting it to the file.  \
**This part needs to be performed using ```Tasks```**. A single thread should perform an action on a single string from an array. Once all tasks are completed, write the file.  \
Write some characters of the original text, and after that, write some random characters from a random sequence.  \
Repeat until the text runs out of characters.
In order to know how many characters to write (from both text and sequence), create a random type variable with seed 1234.  