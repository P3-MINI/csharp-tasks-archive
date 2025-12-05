# Programming 3 - Advanced
## Laboratory 3 - Classes and Structs in C#

Agenda:
- Classes vs structs in C#
- Access modifiers
- Constructor and methods
- Properties
    - Basic properties
    - Auto-implemented properties
    - Logic in  properties
- `static` keyword
- `const` keyword
    - `const` vs `readonly` in C#
- Inheritance
    - Basic inheritance
    - `base()` keyword
    - `new` keyword
    - `virtual` and `override`
    - `abstract` classes and methods
    - Polymorphism

### Part #1 - Classes vs Structs in C#

In C#, both classes and structs are used to define custom types that can store a state, properties and define their own behavior by specifying the methods inside, but they differ in several fundamental ways. A class is a reference type, meaning that objects of a class are stored on the heap, and variables hold a reference to the memory location of the object. When an object of a class is passed to a method or assigned to another variable, the reference is copied, so changes made to the object through one reference will be reflected in all references to that object. In contrast, a struct is a value type, meaning it is stored directly in memory, usually on the stack. When a struct is passed to a method or assigned to another variable, the actual data is copied, so changes made to one copy do not affect others. Structs are often used for small, simple data structures, while classes are more suited for complex objects that require shared behavior and relationships through inheritance.

The class can have the following components:
* fields
* methods
* constructors
* properties
* constants
* destructors
* operators
* indexers
* events 

Today, we will cover the first five components.

To begin with, let's write our first class and struct.

1. Open Visual Studio and create Console App. Remember to use .NET 8.0 (not .NET Framework).
If you don't check the 'Do not use top-level statements' checkbox in the 'Additional information' window, your application should look like this:

```csharp
// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
```

Program.cs (the file with top-level statements - with the code above) can also contain namespaces and type definitions, but they must come after the top-level statements.

If you check the aforementioned checkbox, you should be able to see the classic class, which defines the entry-point Main function:

```csharp
namespace YourNamespaceName
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
```
Here you can write your class outside the namespace, inside the namespace or inside the existing class, but it's better to understand where are you writing your code. In this case, we will write the classes inside the existing namespace or in a different file.

2. Write first class and struct. For the struct, we will create a `WorldPosition` definition storing its longitude and latitude, and for the class - `Car` storing its make and model.

```csharp
public struct WorldPosition 
{
    public float Longitude;
    public float Latitude;
}

public class Car 
{
    public string Make;
    public string Model;
}
```

Note: You can add these classes in a separate file, or in Program.cs in the proper place. The placement of the class definitions in Program.cs is depending on whether you are using top-level statements.

3. Play around with your code. Create one instance of `WorldPosition` and `Car` and assign values to the fields. 

**TEACHERS**: Explain the differences between struct and class: default access modifiers and value/reference type differences. Ask students a quiz: how do I change Model of the `car1` without using strictly `car1` variable's fields? Ask students to create another instance from the existing one by copying/referencing the first instance.


```csharp
// Value Type (struct) behavior
WorldPosition p1 = new WorldPosition { Longitude = 21.02, Latitude = 52.24 };
WorldPosition p2 = p1;
p2.Longitude = 140.5; // Does NOT affect p1.Longitude
Console.WriteLine("p1 Longitude: {0}, p2 Longitude: {1}", p1.Longitude, p2.Longitude);

// Reference Type (class) behavior
Car car1 = new Car { Make = "Toyota", Model = "Camry" };
Car car2 = car1;
car2.Model = "Corolla"; // Changes car1.Model as well!
Console.WriteLine("Car1 model: {0}, Car2 model: {1}", car1.Model, car2.Model);
```
### Part #2 - Access modifiers

Access modifiers in C# are keywords used to define the accessibility of classes, methods, fields, properties, and other members. They play a critical role in encapsulation, one of the core principles of object-oriented programming, by controlling how and where the members of a class can be accessed. Understanding and correctly applying access modifiers is essential for writing secure and maintainable code.

C# provides six main access modifiers: public, private, protected, internal, protected internal, and private protected. Each one defines different levels of access, from full accessibility across all classes and assemblies to restricting access within a specific class or derived classes.

We distinguish several types of access modifiers:

- `public`: Accessible from any other class, anywhere in the application or other assemblies.
- `private`: Accessible only within the containing class; not visible outside of it.
- `protected`: Accessible within the containing class and any class that inherits from it.
- `internal`: Accessible only within the same assembly (project); inaccessible from other assemblies.
- `protected internal`: Accessible within the same assembly or by derived classes in other assemblies.
- `private protected`: Accessible only within the containing class or by derived classes, but only within the same assembly.

In the `WorldPosition`/`Car` example, the access modifiers were defined as `public`, so they are accessible outside the class definition.


### Part #3 - Constructor and methods

In this part, we will focus on the 3 basic components in the class: constructor, fields and methods in the classes.

#### Constructor
To initialize the class, we can pass values to the fields as we've done before, but in general, it is recommended to use constructors to fill in the instance's data. Constructors are convenient when the values of specific fields are dependent on other values.

**TEACHERS**: Show how to generate constructors filling the fields with the Visual Studio Code Actions (Screwdriver or Lightbulb) icon.

**TEACHERS**: Show code snippets like `ctor`, `propfull`, `propg` (https://learn.microsoft.com/en-us/visualstudio/ide/visual-csharp-code-snippets).

Let's get back to our `Car` class definition. Our class already has two fields defined, with the `public` access modifier, as we still need other mechanisms to properly encapsulate the data. 

Write constructor to fill the `Make` and `Model` fields:

```csharp
public class Car 
{
    public string Make;
    public string Model;
    public Car(string make, string model)
    {
        Make = make;
        Model = model;
    }
}
```

With such implementation, we have to change `car1` object creation:

```csharp
Car car1 = new Car("Toyota", "Camry");
```

#### Fields

Fields are variables that hold data within a class or struct. They represent the state or attributes of an object and are typically used to store values that can be accessed or modified by the methods of that class. Fields can have different access modifiers (like private, public, or protected), which determine their visibility outside the class.
Fields should generally be kept private or protected and accessed via properties to maintain encapsulation and ensure that proper validation or logic can be applied.

Add a new `private` field to your class `_currentFuelAmount` of float type that will hold the information about the current level of the fuel in the tank.

#### Methods
Methods are blocks of code that perform specific tasks. They consist of an access specifier, return type, function name and parameters.

Create a method in the `Car` class that refuels the tank. Add the fuel value passed as the parameter to the `_currentFuelAmount`.

```csharp
public class Car 
{
    public string Make;
    public string Model;
    private float _currentFuelAmount = 0;

    public Car(string make, string model)
    {
        Make = make;
        Model = model;
    }

    public void Refuel(int fuel)
    {
        _currentFuelAmount += fuel;
    }
}
```

To call it, we can write
```csharp
Car car3 = new Car("Skoda", "Octavia");
car3.Refuel(60);
```

The above method modifies the internal state of the class. As for now, we cannot access its internal `_currentFuelAmount` value, so we will write a function to get this value outside the class.

**TEACHERS**: Show how to access the value without the below function using the VS debugger.

```csharp
public class Car 
{
    public string Make;
    public string Model;
    private float _currentFuelAmount = 0;

    public Car(string make, string model)
    {
        Make = make;
        Model = model;
    }

    public float GetCurrentFuel()
    {
        return _currentFuelAmount;
    }

    public void Refuel(float fuel)
    {
        _currentFuelAmount += fuel;
    }
}
```

or 

```csharp
public class Car 
{
    public string Make;
    public string Model;
    private float _currentFuelAmount = 0;

    public Car(string make, string model)
    {
        Make = make;
        Model = model;
    }

    public float GetCurrentFuel() => _currentFuelAmount;

    public void Refuel(float fuel)
    {
        _currentFuelAmount += fuel;
    }
}
```

Printing the value:

```csharp
Console.WriteLine("Car1 fuel amount: {0}", car1.GetCurrentFuel());
Console.WriteLine("Car3 fuel amount: {0}", car3.GetCurrentFuel());
```

The output should be like this:

```
Car1 fuel amount: 0
Car3 fuel amount: 60
```

### Part #4 - Properties
In C#, properties provide a way to encapsulate data within a class while offering controlled access to its fields. Properties act as accessors for class fields, allowing you to define how values are set or retrieved. They are typically used to safeguard fields by providing a layer of control, such as validation or logging, before values are assigned or returned. Unlike fields, properties provide greater flexibility since they can be read-only, write-only, or both. C# supports auto-implemented properties, which simplify the syntax when no additional logic is needed.

#### Basic Properties
Let's add property `CurrentFuelAmount` to provide controlled access to the `_currentFuelAmount` field:

```csharp
public class Car 
{
    ...
    private float _currentFuelAmount = 0;

    public float CurrentFuelAmount
    {
        get { return _currentFuelAmount; }  // Getter
        set { _currentFuelAmount = value; }  // Setter
    }
    ...
}
```

or 

```csharp
public class Car 
{
    ...
    private float _currentFuelAmount = 0;

    public float CurrentFuelAmount { get => _currentFuelAmount; set => _currentFuelAmount = value; }
    ...
}
```

#### Auto-implemented properties
Add auto-implemented properties to `Make` and `Model`:

```csharp
public class Car 
{
    public string Make {get; set;} // Auto-implemented property
    public string Model {get; set;} // Auto-implemented property
    ...
}
```


#### Read-only and write-only properties
Read-only properties define only the `get` part, whereas write-only properties define only the `set` part.

Example: if we define `Circle` class, we might want to define its area as the read-only property.

```csharp
public class Circle 
{
    public double Radius { get; set; }

    public double Area {
        get { return Math.PI * Radius * Radius; }  // No setter
    }
}
```

or

```csharp
public class Circle 
{
    public double Radius { get; set; }

    public double Area => Math.PI * Radius * Radius;
}
```

Usage:
```csharp
// Usage
Circle circle = new Circle { Radius = 5 };
Console.WriteLine($"Area: {circle.Area}");  // Read-only property
```


For our car example, let's remove the setter on the car's `Make` property - created cars won't have the possibility to change their make.

```csharp
public class Car 
{
    public string Make {get;} // Auto-implemented property
    public string Model {get; set;} // Auto-implemented property
    ...
}

```

#### Logic in properties
In properties, we can add logic to our setters and getters. As the fuel volume cannot be negative, let's have our car's `CurrentFuelAmount` setter handle such a case:

```csharp
public class Car 
{
    ...
    public float CurrentFuelAmount 
    { 
        get => _currentFuelAmount; 
        set
        {
            if (value > 0)
                _currentFuelAmount = value;
        }
    }
    ...
}
```

Now, setting a negative value to the fuel amount will be impossible:

```csharp
car1.CurrentFuelAmount = -100; // Won't set field value
```

### Part #5 - `static` keyword

The `static` keyword in C# plays a critical role in defining members and classes that belong to the type itself rather than to instances of the type. When something is declared as `static`, it means that it is shared across all instances of the class, and often, it can be accessed directly without creating an instance of the class. The `static` keyword can be applied to methods, properties, fields, classes, and even constructors.

#### Static Methods
A static method belongs to the class itself, not to any specific instance of the class. This means you can call the method directly on the class without creating an instance of the class. Static methods are often used for utility or helper methods where no object state is required. A static method cannot access instance members (non-static fields or properties) because it is not associated with an instance of the class.

In our car example, we can add a method to compare two cars - if both of them have the same make and model, the comparison should return true.

```csharp
class Car
{
    ...
    public static bool CompareCars(Car car1, Car car2)
    {
        return car1.Make == car2.Make && car1.Model == car2.Model;
    }
}
```

To use it, we will create two cars and call the method. Remember that static methods are called by calling the method on the class name, not the instance!

```csharp
Car toyota1 = new Car("Toyota", "Camry");
Car toyota2 = new Car("Toyota", "Camry");
Console.WriteLine("Is toyota1 the same car as toyota2? {0}", Car.CompareCars(toyota1, toyota2)? "Yes": "No");
```

The output will show the answer:

```
Is toyota1 the same car as toyota2? Yes
```

#### Static Fields
A static field is a variable that is shared by all instances of the class. Like static properties, static fields store data that is common to all objects of the class, but static fields do not provide encapsulation (since they don’t have a getter/setter). They are accessed directly through the class.

Let's add a static field to our `Car` class that will store the number of created instances. Thanks to a static field, we can generate consecutive numbers that can be later converted into unique IDs.

```csharp
public class Car 
{
    ...
    private static int _totalCarsCreated = 0;
    public int ID;


    public Car(string make, string model)
    {
        Make = make;
        Model = model;
        ID = ++_totalCarsCreated;
    }
    ...
}
```
The field has a private access modifier, so it can be used only inside the class. It is consistent with its purpose - it is only used to generate consecutive IDs of the cars.
We can check the IDs of each created car:

```csharp
Console.WriteLine("car1 ID: {0}", car1.ID);
Console.WriteLine("car2 ID: {0}", car2.ID);
Console.WriteLine("car3 ID: {0}", car3.ID);
Console.WriteLine("toyota1 ID: {0}", toyota1.ID);
Console.WriteLine("toyota2 ID: {0}", toyota2.ID);
```

Output:

```
car1 ID: 1
car2 ID: 1
car3 ID: 2
toyota1 ID: 3
toyota2 ID: 4
```

Notice that `car2` ID will be the same as `car1` ID - that's because class is a reference type. Only by using the constructor for `Car` we can generate a new instance with a new ID that will be increased.

#### Static Properties
A static property is like a static field but provides the flexibility of a getter and setter for controlled access. Static properties are used when you want to manage data that is common across all instances of a class. Static properties are typically used for tracking data, shared configuration values, or singleton pattern implementations.

**TEACHERS**: Ask students to change the field `_totalCarsCreated` into property.


#### Static Constructors
A static constructor is a special type of constructor that initializes static fields or performs setup tasks for a class itself. The static constructor is called automatically before any static members are accessed, and it only runs once per type (not per instance). Static constructors are useful for initializing static fields or performing other one-time setup tasks.

Example: a logger class that prints out the message passed as a parameter. We will create static constructor to initialize it before first use:

```csharp
public class Logger 
{
    public static string LogFilePath;

    // Static constructor
    static Logger() 
    {
        LogFilePath = "default_log.txt";
        Console.WriteLine("Initializing Logger.");
    }
    
    public static void Log(string message) 
    {
        Console.WriteLine($"Logging message to {LogFilePath}: {message}");
    }
}

// Usage
Logger.Log("Application started.");  // Static constructor runs before this
```

Using the logger will print to the console:
```
Initializing Logger.
Logging message to default_log.txt: Application started.
```

#### Static Classes
A static class is a class that can only contain static members. You cannot create instances of a static class, and all of its members must also be static. Static classes are useful for grouping related utility functions or constants that don’t need to maintain any instance-specific state.

Let's write a static class `CarFactory` that will have a method for creating specific types of cars that already contain fuel when they leave the factory:

```csharp
public static class CarFactory
{
    public static Car CreateHondaCivic()
    {
        var car = new Car("Honda", "Civic");
        car.Refuel(10);
        return car;
    }
}

// Usage
Car hondaCivic = CarFactory.CreateHondaCivic();
```


### Part #6 - `const` keyword

The `const` keyword in C# is used to declare fields or variables whose values are constant and cannot be changed after they are initially assigned. `const` fields must be assigned a value at the time of declaration, and that value is embedded into the compiled code, meaning it is determined at compile time and cannot be altered during program execution. Constants are implicitly static, meaning they belong to the class or struct itself rather than to individual instances. They can only hold simple data types such as numbers, strings, or enum values, and cannot be used with reference types (except for string and null-assigned reference types).

Let's add the `const` field to our class. Let's say that we have one tank supplier, so every car has the same maximum capacity.
Add a constant field that indicates the max capacity of the tank.

```csharp
public class Car 
{
    ...
    private float _currentFuelAmount = 0;
    public const int MaxFuelCapacity = 50; // Constant value for all cars
    ...
}
```

Observe that changing MaxCapacity in runtime is impossible:

```csharp
Car.MaxFuelCapacity = 100; // Error!
```

#### `const` vs `readonly` in C#

In C#, both `const` and `readonly` are used to define fields whose values cannot be modified after they are set. However, they differ in key ways in terms of when and how the values are assigned, as well as in their usage.

Initialization:
- `const`: Must be initialized at the time of declaration. The value of a const field is known at compile time and is hardcoded into the assembly. This means const fields are always the same for every instance of the class and are static by nature.
- `readonly`: Can be initialized either at the time of declaration or in the constructor. The value of a readonly field is set at runtime, meaning that it can differ between different instances of a class. readonly fields can be used to represent values that may vary but are still immutable after they are assigned.

Modification:
- `const`: Once defined, a const value cannot be modified. It is a compile-time constant and cannot be changed after the code is compiled.
- `readonly`: A readonly field can only be assigned once, either at the point of declaration or inside a constructor, but after that, it cannot be changed. However, since it is assigned at runtime, its value can vary across different instances of the class.

Type Restrictions:
- `const`: Can only be used with primitive types (e.g., int, double, bool, char) and strings. It cannot be used with reference types (except strings and null-assigned reference types) or structs.
- `readonly`: Can be used with both primitive and reference types. It is more flexible because it allows non-primitive objects (like custom classes or structs) to be initialized and set at runtime.

Example:
```csharp
public class Car 
{
    public const int MaxSpeed = 240;  // Must be assigned at declaration, same for all cars
    public readonly string VIN;       // Can be assigned in constructor, different for each car

    public Car(string vin) 
    {
        VIN = vin;  // VIN is set in the constructor, but cannot be changed afterward
    }
}
```
MaxSpeed is a `const`, which means it's the same for every instance of Car and cannot be modified after the code is compiled.
VIN is a `readonly` field that can be set when each Car object is created (via the constructor), but cannot be changed after that.

More information: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const

### Part #7 - Inheritance

Inheritance is a key concept in object-oriented programming (OOP) that allows one class (called a derived class) to inherit the properties, methods, and behavior of another class (called a base class). It promotes code reuse, making it easier to create and maintain programs by allowing common functionality to be defined once in a base class and shared among multiple derived classes. In C#, inheritance is implemented using the colon (`:`) syntax, and a class can only inherit from a single class (i.e., C# supports single inheritance).

#### Basic inheritance 
For our case, we will create class `Vehicle`, which will be the base class for our cars. Vehicle instances will have a field indicating how much distance in kilometers the vehicle has covered - `TotalDistanceCovered`. It will also have a function to move the vehicle by a specific distance.

```csharp
public class Vehicle 
{
    public float TotalDistanceCovered = 0; // [km]

    public void Move(float distance) 
    {
        TotalDistanceCovered += distance;
    }
}
```

To add the base class to our `Car` class, we need to add only the proper base class in the definition:


```csharp
public class Car : Vehicle 
{
    ...
}
```

Notice how `Vehicle` class instances can be created:

```csharp
Vehicle vehicle = new Vehicle();
```

Cars will now have the counter for covered distance and method `Move()`.

To see it work, we will create a car, and move it by some distance:

```csharp
Car tesla = new Car("Tesla", "Y");
Console.WriteLine("tesla TotalDistance: {0}", tesla.TotalDistanceCovered);
tesla.Move(100);
Console.WriteLine("tesla TotalDistance (after Move): {0}", tesla.TotalDistanceCovered);
```

We will now add another derived class that can be considered a vehicle: `Bike`.

```csharp
class Bike : Vehicle
{
}
```

We don't need to add anything else to the class definition right now, but feel free to enhance the class by additional properties, methods, etc.
We will create `Bike` instance and try to move it:

```csharp
Bike bike = new Bike();
Console.WriteLine("bike TotalDistance: {0}", bike.TotalDistanceCovered);
bike.Move(10);
Console.WriteLine("bike TotalDistance (after Move): {0}", bike.TotalDistanceCovered);
```

#### `this` and `base`

In C#, when a class inherits from another class, the constructor of the base class is automatically called before the derived class’s constructor. However, sometimes the base class may have multiple constructors, or you may want to pass specific values to it. To explicitly call a base class constructor, you use the `base` keyword in the derived class’s constructor.

The `base` keyword allows you to control how the base class is initialized, ensuring that the base class constructor receives any necessary parameters. This is useful when the base class constructor takes arguments that need to be passed from the derived class.

Similarly, you can use the `this` keyword to call another constructor of the same class.  This is useful for keeping initialization logic in one constructor, and calling it in other constructors, to ensure every initialization is performed in the same way.

Let's modify the `Vehicle` class to take a parameter for its name when it's created:


```csharp
public class Vehicle 
{
    ...
    public string Name { get; private set; }

    public Vehicle(string name) 
    {
        Name = name;
    }
    ...
}
```

Now, let's add constructor additional `name` parameter to the car. It's always nice to add a lovely nickname to your beloved car. This constructor will call the base class constructor using the `base` keyword.
To keep the possibility of creating cars without naming them, we will add a constructor with the old parameters `make` and `model`, and we will use the `this` keyword to call an already existing constructor. That way, we can be sure that initialization is the same, no matter which constructor we call.

```csharp
public class Car : Vehicle 
{
    ...
    public Car(string make, string model, string name) : base(name)
    {
        Make = make;
        Model = model;
    }

    public Car(string make, string model) : this(make, model, $"{make} {model}")
    {
    }
    ...
}

public class Bike : Vehicle 
{
    public Bike() : base("Bike") { }
}
```

The `this` keyword also indicates the current instance in the methods and constructors. It is commonly seen during fields assignment in the constructor, in operators, in Visitor design pattern, etc.

`Car` initialization using `this` keyword would be as follows:


```csharp
public class Car : Vehicle 
{
    ...
    public Car(string name, string make, string model) : base(name)
    {
        this.Make = make;
        this.Model = model;
        this.ID = ++_totalCarsCreated;
    }
    ...
}
```

Remember to update your code whenever you call the `Vehicle` constructor and pass the name of the vehicle to the constructor.

#### `new` keyword

In C#, the `new` keyword can be used to hide a member (method, property, or field) from the base class in a derived class when you want to provide a new implementation without overriding the base class method. This is different from overriding, as method hiding with `new` does not use polymorphism. Instead, it hides the base class member, meaning that the base class method is still callable if you reference the object as an instance of the base class.

When you use the new keyword, it signals that you are intentionally hiding the base class member and that you want to provide a new version of the method or property for the derived class.

Let's modify our `Vehicle` and Car `classes` to demonstrate method hiding:

```csharp
public class Car : Vehicle 
{
    ...

    // Using 'new' to hide the base class method
    public new void Move(float distance) 
    {
        Console.WriteLine($"{Make} {Model} drove {distance} kilometers.");
    }
}
```


#### `virtual` and `override`

Through inheritance, derived classes can also extend or modify the behavior of the base class by overriding methods, adding new properties or methods, or even providing new functionality. If a method in the base class is marked as `virtual`, the derived class can use the override keyword to provide a specific implementation for that method.

In our case, cars obviously use fuel to move. Override the function to use the fuel. If the car has insufficient fuel to cover the distance, go as far as the amount of fuel allows. You can define how many liters per 100 kilometers they use any way you want.

Remember to mark `Move()` function in the `Vehicle` as `virtual`.

```csharp
public class Car : Vehicle 
{
    ...
    public const float FuelConsumptionRatio = 12; // 12 liters per 100 km

    // Assume passed distance is in kilometers
    public override void Move(float distance) 
    {
        float maxDistance = CurrentFuelAmount / FuelConsumptionRatio * 100;
        float distanceCovered = Math.Min(maxDistance, distance);
        TotalDistanceCovered += distanceCovered;
        CurrentFuelAmount -= distanceCovered * FuelConsumptionRatio / 100;
    }
}
```

Riding a bike will still work as defined in the base class.

Example usage:

```csharp
Car kia = new Car("Kia", "Sportage");
kia.Refuel(6);
Console.WriteLine("kia TotalDistance: {0}", kia.TotalDistanceCovered);
kia.Move(100); // Try to go 100 km - the fuel amount is insufficent to cover such distance
Console.WriteLine("kia TotalDistance (after Move): {0}", kia.TotalDistanceCovered);

Bike bike2 = new Bike();
Console.WriteLine("bike2 TotalDistance: {0}", bike2.TotalDistanceCovered);
bike2.Move(20); // Moved 20 km by bike
Console.WriteLine("bike2 TotalDistance (after Move): {0}", bike2.TotalDistanceCovered);
```

and its result:

```
kia TotalDistance: 0
kia TotalDistance (after Move): 50
bike2 TotalDistance: 0
bike2 TotalDistance (after Move): 20
```

#### `abstract` keyword

While inheritance allows one class to inherit functionality from another, sometimes the base class is meant to serve as a template or blueprint without being fully implemented. In C#, this is where abstract classes and abstract methods come into play. An abstract class is a class that cannot be instantiated on its own and is intended to be subclassed. It may include abstract methods, which are methods without any implementation, and these must be overridden by derived classes.

##### Abstract Classes
An abstract class in C# is a class that cannot be instantiated directly. An abstract class can contain both abstract members (methods or properties with no implementation) and non-abstract members (fully implemented methods and properties).


##### Abstract Methods
An abstract method is a method declared in an abstract class that does not have a body or implementation. It acts as a placeholder for functionality that derived classes must provide. Any class that inherits from an abstract class must provide an implementation for all abstract methods using the override keyword.

In our example, we will mark the `Vehicle` as the `abstract` class. We will also mark the `Move()` method as an abstract method, and we will move its implementation to the `Bike` class.

Notice how the `Move()` method now has to be implemented inside the `Bike` class.

**TEACHERS**: Show students how to use Visual Studio Quick Actions to implement missing interfaces and base class' methods (CTRL + .).

```csharp

public abstract class Vehicle 
{
    ...
    public abstract void Move(float distance);
}

class Bike : Vehicle
{
    ...
    public override void Move(float distance)
    {
        TotalDistanceCovered += distance;
    }
}
```

Notice how a `Vehicle` class instance cannot be created with the `abstract` keyword.

```csharp
Vehicle vehicle = new Vehicle(); // Error
```
However, we can still create arrays to store `Vehicle`-derived objects. This part will be explained in the next section.

#### Polymorphism

Polymorphism is a fundamental concept in object-oriented programming that allows objects of different types to be treated as instances of the same base type. In C#, polymorphism enables a derived class to override methods from its base class, allowing the same method call to behave differently depending on the object it’s called on. Polymorphism can be achieved using inheritance, where a base class reference points to an object of a derived class, and the overridden method in the derived class is executed.

For example, consider an array of `Vehicle` objects that includes both `Car` and `Bike` instances. Even though the array is of type `Vehicle`, calling the `Move()` method will invoke the appropriate version of `Move()` based on the actual object type (either `Car` or `Bike`), thanks to polymorphism.

```csharp
Vehicle[] vehicles = new Vehicle[] {
    CarFactory.CreateHondaCivic(),
    new Bike()
};
foreach (var vehicle in vehicles) 
{
    vehicle.Move(100);  // Calls the appropriate Move method for Car or Bike
}
foreach (var vehicle in vehicles) 
{
    Console.WriteLine("{0} TotalDistance: {1}", vehicle.Name, vehicle.TotalDistanceCovered);
}
```

The output shows that proper Move functions were called:

```
Honda Civic TotalDistance: 83.33333
Bike TotalDistance: 100
```
