### Introduction to Graphs

A graph is a data structure that consists of a set of **vertices** and a set of **edges** that connect pairs of vertices. Graphs are used to model relationships between objects and are widely applied in various domains, such as social networks, mapping and navigation systems, computer networks, and more.

There are several ways to represent a graph in memory. The two most common representations are:

1.  **Adjacency Matrix**: A 2D array where the entry at `matrix[i][j]` is `1` or `true` if there is an edge from vertex `i` to vertex `j`, and `0` or `false` otherwise. This representation is efficient for checking if an edge exists between two vertices but can consume a lot of memory for sparse graphs (graphs with few edges).

2.  **Adjacency List**: An array of lists, where the list at index `i` contains all the vertices adjacent to vertex `i`. This representation is more memory-efficient for sparse graphs, as it only stores the existing edges.

In this laboratory, you will implement both of these representations.

### **General Requirements**

The goal of this laboratory is to implement a directed graph data structure and depth-first search algorithm associated with it. All classes should be in the `Graphs` namespace.

### **Stage 1: Graph Representation (6 points)**

This stage focuses on creating the basic graph structure and two different implementations for representing the graph's edges.

* **1.1. Abstract `Graph` Class (2 points)**
  * Define an abstract class `Graph`.
  * It should have a protected constructor `Graph(int vertexCount)` that initializes a public, read-only `int VertexCount` property.
  * Declare the following abstract methods:
    * public `List<int> GetOutVertices(int vertex);` - returns neighbors list of `vertex`
    * public `void AddEdge(int from, int to);` - add directed edge to the graph
  * Implement a public `void Display()` method that prints the graph's adjacency information to the console. The output for each vertex should be in the format `vertex -> [neighbor1,neighbor2,...]`, for example:
    ```
    0 -> [1,2]
    1 -> [3]
    2 -> []
    3 -> [1]
    ```

* **1.2. Adjacency List Implementation (2 points)**
  * Create a public class `AdjacencyListGraph`, which inherits from `Graph`.
  * It should have a public `AdjacencyListGraph(int vertexCount)` constructor.
  * Implement the `GetOutVertices` and `AddEdge` methods using a `List<int>[]` to store the graph structure.

* **1.3. Adjacency Matrix Implementation (2 points)**
  * Create a public class `AdjacencyMatrixGraph`, which inherits from `Graph`.
  * It should have a public `AdjacencyMatrixGraph(int vertexCount)` constructor.
  * Implement the `GetOutVertices` and `AddEdge` methods using a `bool[,]` to store the graph structure.

### **Stage 2: Graph Traversal and Pathfinding (6 points)**

This stage involves adding graph traversal logic using the Depth-First Search (DFS) algorithm and implementing the visitor pattern to process vertices during traversal.

Depth-First Search (DFS) is an algorithm for traversing or searching tree or graph data structures. The algorithm starts at the root node (or an arbitrary node in a graph) and explores as far as possible along each branch before backtracking. It is usually implemented using recursion.

* **2.1. Depth-First Search (DFS) Algorithm (3 points)**
  * Add a public `void DepthFirstSearch(int start, IVertexVisitor visitor)` method to the abstract `Graph` class.
  * This method should implement the DFS traversal algorithm recursively.
  * The traversal should start from the `start` vertex.
  * Before the recursive call for each neighbor, the method should call `PreVisit` from the `IVertexVisitor` interface.
  * After the recursive call for each neighbor, the method should call `PostVisit` from the `IVertexVisitor` interface.
  * The method might use a helper method, for example `DepthFirstSearchRecursive(int vertex, bool[] visited, IVertexVisitor visitor)`.
    * Use a `bool[] visited` array to mark and skip already visited vertices.

* **2.2. `IVertexVisitor` Interface**
  * Define a public interface `IVertexVisitor`.
  * This interface should declare the following methods:
    * `void PreVisit(int vertex);`
    * `void PostVisit(int vertex);`

* **2.3. `PathFinderVisitor` (3 points)**
  * Create a public class `PathFinderVisitor` that implements the `IVertexVisitor` interface.
  * It should have a public constructor: `PathFinderVisitor(int target)`.
  * It should have the following properties:
    * `public List<int> Path { get; }` - A list of vertices forming the found path.
    * `public bool Found { get; private set; }` - A flag indicating whether the path has been found.
  * Implement the `PreVisit` and `PostVisit` methods to find a path to the `target` vertex.
  * Override the `ToString()` method to return the found path in the format `v1 --> v2 --> ...` or a "not found" message.
