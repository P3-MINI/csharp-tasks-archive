## Stage 1 - 1 point 

Define `struct` named Point2D. 
- It contains `public` fields `x`, `y` of type `double`. 
- Fields are immutable.
- It contains constructor that takes from 0, 1 or 2 `double`s.
  In case when number of passed arguments is 0 or 1 then value of not specified coordinates should be set to  0.
  Order of passed parameters from left to right is: x, y. 
- It allows to provide value for specific coordinate and set other coordinates to 0.
- When instance is passed as argument to `Console.WriteLine` method it output message in following format: (x, y)
- Creating instance of `Point2D` with some explicit coordinates should print following message on `Console`
  "Point2D(x, y) has been created".
  Remember about 'Do not repeat yourself (DRY)' rule

## Stage 2 - 1 point

Create static `class` named `Geometry`. It contains 2 public methods:

`Distance` method that takes two `Point2D` points and returns cartesian distance between them.

`PolygonCircuit` method that takes variable number of `Point2D`s and calculates circuit of created polygon.
 In case where number of passed points is less than 3 method should return -1.0.

Both methods are not modifying instance of passed arguments and should be annotated with proper keyword for compiler.

## Stage 3 - 1 point

Create abstract `class` named `Shape`. 
It contains abstract method `Circuit` that calculates circuit of given shape.

Create abstract `class` named `Polygon` that inherits from `Shape` class. 
It contains protected constructor that takes variable number of `Point2Ds`. 
It contains array of points of given polygon - public field.
It provides implementation for `Circuit` method that calculates circuit for any polygon.

Create `Rectangle` class that inherits from `Polygon`. `Polygon` has two constructors:
- constructor that takes single Point2D as left bottom point and 2 double numbers that represent width and height.
- Constructor that takes single Point2D as left bottom point and 1 double number that represents side of square - special case of rectangle.
`Rectangle` has public field of type `double` that holds length of rectangle's diagonal.

Create `Circle` class that inherits from `Shape`.
Constructor takes single `Point2D` as circle center and 1 number that represents length of the radius.
Values passed are accesible as public fields.

## Stage 4 - 1 point

We will try to describe objects in array. In case:

When object is not a `Shape` program will display: 
- Object is not a shape.

When object is `Shape` program will display:
- Object is a shape
- Shape has circuit of $circuit

When object is convertible to `Polygon` program will display:
- Shape is Polygon 
- List of polygon points

When object is not convertible to `Polygon` program will display:
- Shape is not a Polygon

When object is convertible to `Circle` program will display:
- Shape is a Circle that has center at $center and radius of $radius

When object is convertible to `Rectangle` program will display:
- Shape is a Rectangle and has diagonal of $diagonal

Add code in `Main` function in Stage 4 section. 
See sample output file for more details of expected output.

## Stage 5 - 1 point

Add `ToOriginDistance` method to `Geometry` class that takes two arguments:  
first argument - named tuple of 2 `double`s (x, y) - center of the circle  
second argument - `double` - radius of that circle  
and returns distance to the origin point (0,0) of the closest point on the circle.

Uncomment stage 5 and make the code compile - you need to define some deconstructors.
