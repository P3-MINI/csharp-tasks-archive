# Programming 3 - Advanced

## Laboratory 5 - Yield, Interface, Generics, IEnumerable

### Stage01 (1 point)
Create a generic class `MyPair<TKey, TValue>` that will store a key and a value.
The class should include:
- A constructor that takes two parameters of type `TKey` and `TValue`.
- A `Key` property that returns the key and does not allow it to be changed.
- A `Value` property that allows getting and modifying the value.
The class should only be creatable for key types implementing the standard `IComparable<TKey>` interface.
For the MyPair class, also implement the `IComparable<MyPair<TKey, TValue>>` interface to allow comparing pairs based on the key.

### Stage02 (1 point)
Create an interface `IMyMap<TKey, TValue>` that defines some of the functionality of a map/dictionary.
As with the pair, `TKey` should implement the `IComparable<TKey>` interface.
The interface should include:
- A `Count` property that returns the number of elements in the map.
- An `Add(TKey, TValue)` method that adds a pair with the given key and value to the map.
  If the addition is successful, the method should return True; otherwise, False.
- A `Find(TKey)` method that searches the map for a pair with the given key.
  It should return `MyPair<TKey, TValue>` if a pair with the given key is found; otherwise, `null`.

### Stage03 (3 points)
Implement a `MyList` class that implements the `IEnumerable` interface. 
The values are stored in a private array, with an initial size of 4, and when the array is full, its size is doubled.

In addition to the properties and methods required by the interface, add the following:
- `Add` method, adds given item to list
- `Count` property, read-only of type `int`
- An indexer, should throw `ArgumentOutOfRangeException` for invalid index values
- `Remove` method, removes given item (only first occurrence) and returns `True`, if item is not in list `False` 
- `RemoveAt` method, should throw `ArgumentOutOfRangeException` for invalid index values
- `IndexOf` method, returns index of given item or `-1` if not found
- `Contains` method, return `True` if list contains given item, otherwise `False`
- `Clear` method, removes all items from list and changes its capacity to default value

### Stage04 (3 points)
Create a `MyBinaryTree<TKey, TValue>` class implementing `IMyMap<TKey, TValue>` that stores key-value pairs in a binary tree.  
Hint: Create an appropriate helper class for the tree node (nested within the `MyBinaryTree` class).

In addition to the properties and methods required by the interface, add the following:
- `Add` method, takes two parameters - key and value and adds new value to the tree.
  Returns `False` if given key is already in tree, otherwise `True`.
- `Find` method, returns `MyPair` with given key value or `null` if key is not in the tree.
- `Count` property