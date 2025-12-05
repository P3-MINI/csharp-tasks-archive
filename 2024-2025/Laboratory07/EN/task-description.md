# Lab07:

You are the lead engineer responsible for managing server infrastructure in a large data center. Your task is to create a system that will allow for server management, track changes in their properties, and notify administrators of any significant updates.

## Stage01: Implement the `Server` class.

Create a `Server` class implementing the `IAddressable` and `INotifyPropertyChanged` interfaces, according to the following specification:

| Property | Type   |
| -------- | ------ |
| Address  | string |
| Name     | string |
| Status   | Status |
| Load     | double |

Requirements:

- `Status` can take the following values: `Failed`, `OverLoaded`, `Running`, and `Stopped`.
- The class has a constructor that sets all properties. The default values for `Status` and `Load` are `Stopped` and `0.0`, respectively.
- The value of `Address` can only be set within the constructor.
- Changing the values of the `Name`, `Load`, and `Status` properties triggers an event defined by the `INotifyPropertyChanged` interface.
- Calling `ToString` returns a string of the form: `Name [Address]`.

In `Program.cs`, add functionality to the server object so that any change to the object's fields prints a message to the console in the form: `[Address]: PropertyName => Value`.

### Points:

- `1.5 Pts` - implementing `Server` class and modifying `Program.cs`.

## Stage02: Implement a collection that notifies about changes.

Create a generic interface `INotifyingCollection<T>`, which implements `IEnumerable<T>` and with a constraint that `T` implements `IAddressable` and `INotifyPropertyChanged`. The interface provides:

- An `Add` method for inserting an item into the collection.
- A `Remove` method for removing an item with the given address from the collection.
- Each method returns information on whether the operation was successful.
- `ElementAdded` and `ElementRemoved` events of type `EventHandler<CollectionChangedEventArgs<T>>?`, where `CollectionChangedEventArgs<T>` inherits from `EventArgs` and contains a reference to the object involved in the collection operation.
- An `ElementPropertyChanged` event of type `EventHandler<ElementPropertyChangedEventArgs<T>>`, which captures any change in a property of an element in the collection. `ElementPropertyChangedEventArgs<T>` inherits from `EventArgs` and includes information about the name of the property that has changed, as well as a reference to the element itself.

Create a `ServerSystem` class implementing the `INotifyingCollection<Server>` interface, which stores `Server` objects in a private collection of type `Dictionary<string, Server>`. The server's address is a unique key for the dictionary.

Requirements:

- The class has an `OnServerPropertyChanged` method that subscribes to the `PropertyChanged` events of each server during the addition to the system and unsubscribes from these events during server removal from the system.

In `Program.cs`, subscribe to the following events for the system object:

- `ElementAdded` - prints to the console `Added [Element]`, sets the status of the added server to `Running` and randomly assigns a `Load` value from the range `[40, 50)` using a `random` object.
- `ElementRemoved` - prints to the console `Removed [Element]`.
- `ElementPropertyChanged` - prints to the console `[Address]: PropertyName => Value` for each property change of a server belonging to the system.

Then, add all objects from the `servers` collection to the system.

### Points:

- `1.0 Pts` - implementing `INotifyingCollection<T>` interface.
- `2.0 Pts` - implementing `ServerSystem` class.
- `0.5 Pts` - modifying `Program.cs`.

## Stage03: Extend the system's functionality.

In the `Extensions` class, implement the following extension methods:

- `GetByAddress`, which retrieves server with the corresponding address. If the server isn't found in the system, `null` is returned.
- `RedirectTraffic`, which accepts parameters: `threshold` of type `double` and `redirectionPolicy` which is a function invoked on the server to modify its state.
  - If any server in the system exceeds the threshold, the policy is executed on that server to balance the load.
- `MaintenanceOrder` which accepts `priority` function as a parameter (use `Compare` delegate) and returns all the servers in the order given by the `priority`.

In `Program.cs`, add the following traffic redirection policy:

- If the load of the server goes beyond 50, its load is set to 50 and the excessive traffic is redirected to the first found running server, which `Load` is less than 50. Appropriate message about the redirection's source and target should be displayed on the console (see the file with expected results).
- If the target server isn't found, the status of the overloaded server is set to `Failed` and the following message is displayed on the console: `{server}: Internal Server Error!`.

In `Program.cs` use `MaintenanceOrder` to display the system's servers in the following order (use lambda expression):

- Servers with status `Failed` have the highest priority.
- If both servers have status `Failed`, then the one with greater load has a higher priority.
- In all other cases greater load means higher priority.

### Points:

- `0.5 Pts` - implementing `GetByAddress`.
- `1.5 Pts` - implementing `RedirectTraffic` and modifying `Program.cs`.
- `1.0 Pts` - implementing `MaintenanceOrder` and modifying `Program.cs`.
