# Programming 3 - Advanced

## Laboratory 4 - Operators, Indexers, BCL:

Your company has been chosen to develop an order management system for an international delivery service provider. As an experienced C# developer, you are responsible for implementing some key system components.

### Stage01: String manipulation and data parsing.

Throughout the application, use the cultural settings `CultureInfo.InvariantCulture`.

#### Creating the `Customer` class:

In the `Models` folder, create a `Customer` class based on the following specification:

| Property              | Type       |
| --------------------- | ---------- |
| `FirstName`           | `string`   |
| `LastName`            | `string`   |
| `PhoneNumber`         | `string`   |
| `EmailAddress`        | `string`   |
| `SatisfactionRatings` | `double[]` |

Requirements:

- The class properties are read-only.
- The class has a single constructor that takes all properties in the order defined by the specification table.
- The `ToString` method should be overridden so that objects are printed in the format: `(John Doe, 1234567890, john.doe@gmail.com, [ 5.0 , 4.5 , 1.3 , 4.5 ])`.

#### Customer data validation:

- In the `Validators` folder, implement an abstract class `Validator` with a `Validate` method that accepts a `string?` parameter and returns a `boolean` value (`bool`).
- Implement the following classes that inherit from `Validator`:

  - `NameValidator` - First and last names are considered valid if they consist only of letters (characters `[a-zA-Z]`). Before being stored in the object, values should be capitalized.
  - `PhoneNumberValidator` - A phone number is valid if it consists of exactly 9 digits. Before saving to the object, every occurrence of the digit 6 should be replaced with the digit 9.
  - `EmailAddressValidator` - An email address is valid if it ends with `.com` and contains the `@` symbol.

#### Parsing customer data:

Customer data is stored in `csv` files, where fields are separated by semicolons (`;`) and records by newline characters (`\n`). A careless coworker forgot to trim whitespace from the data before saving it to the file. Your task is to read this data, clean it, and validate it according to the business rules.

In the `Services` folder, implement a class `CsvParser` with a method `ParseCustomers` that accepts a `string` parameter called content and returns an array of `Customer` objects.

Requirements:

- All records that do not meet at least one business rule should be omitted from the return value.
- In the case of an invalid record, a message should be displayed on the console in the format: `[09/10/2024 19:18] Invalid Customer in line 5`.
- To format the date, use the [General date/time pattern (short time)](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings).

The `customers.csv` file in the `Data` folder describes the format in which customer data is stored and contains sample data.

#### Useful links:

- [Immutability of strings](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#immutability-of-strings).
- [String interpolation](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#string-interpolation).
- [Using StringBuilder for fast string creation](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#using-stringbuilder-for-fast-string-creation).
- [Extract substrings from a string](https://learn.microsoft.com/en-us/dotnet/standard/base-types/divide-up-strings).
- [StringSplitOptions Enum](https://learn.microsoft.com/en-us/dotnet/api/system.stringsplitoptions?view=net-8.0).
- [String.Join Method](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-8.0).
- [String.Format Method](https://learn.microsoft.com/en-us/dotnet/api/system.string.format?view=net-8.0).
- [Parsing numeric strings in .NET](https://learn.microsoft.com/en-us/dotnet/standard/base-types/parsing-numeric).
- [DateTime Struct](https://learn.microsoft.com/en-us/dotnet/api/system.datetime?view=net-8.0).
- [TimeSpan Struct](https://learn.microsoft.com/en-us/dotnet/api/system.timespan?view=net-8.0).
- [Choose between DateTime, DateOnly, DateTimeOffset, TimeSpan, TimeOnly, and TimeZoneInfo](https://learn.microsoft.com/en-us/dotnet/standard/datetime/choosing-between-datetime).
- [Standard date and time format strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings).
- [Standard numeric format strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings).

### Stage02: Defining operators, tuples, and formatting.

#### Creating the `Package` class:

In the `Models` folder, create a `Package` class representing cubic packages with a specified weight.

| Field    | Type     |
| -------- | -------- |
| `Size`   | `double` |
| `Weight` | `double` |

Requirements:

- Fields `Size` and `Weight` should be read-only (use the `readonly` keyword).
- Add a Volume property (read-only) that returns the package's volume.
- Implement the addition operator for packages — the resulting package should have a volume and weight that are the sum of the operands.
- Implement the comparison operator for two packages. Add any additional methods or operators needed for this functionality.
- Enable package decomposition into a tuple `(Weight, Size)`.
- Add explicit casting from a tuple `(double, double)` to a package.

#### Creating the `Location` class:

In the `Models` folder, create a `Location` class representing the package's location:

| Property  | Type          |
| --------- | ------------- |
| `X`       | `double`      |
| `Y`       | `double`      |
| `Name`    | `string`      |
| `Culture` | `CultureInfo` |

Requirements:

- Implement a subtraction operation for locations, returning a vector representing the translation from the first location to the second one.
- Override the `ToString` method to return the location in the format `[Name] at ([X]; [Y])`.
- Coordinates should be formatted according to the culture, have a width of 12, four decimal places, and be right-aligned in the field.

#### Useful links:

- [Operator overloading - predefined unary, arithmetic, equality and comparison operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading).
- [Equality operators - test if two objects are equal or not](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators#equality-operator-).
- [Tuple types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples).
- [User-defined explicit and implicit conversion operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators).

### Stage03: Enums, `null`, date and time operations

#### Enum `Priority`:

In the `Models` folder, create an enum `Priority` with the following values (this type should allow for bitwise operators):

| Priority   | Value |
| ---------- | ----- |
| `Standard` | `0`   |
| `Express`  | `1`   |
| `Fragile`  | `2`   |

#### Extending the `Package` class:

Add the following properties to the `Package` class (except for `Priority`, all may have a `null` value):

| Property      | Type        |
| ------------- | ----------- |
| `Sender`      | `Customer?` |
| `Recipient`   | `Customer?` |
| `Source`      | `Location?` |
| `Destination` | `Location?` |
| `ShippedAt`   | `DateTime`  |
| `DeliveredAt` | `DateTime?` |
| `Priority`    | `Priority`  |

The default value for `Priority` is `Standard`.

#### Calculating package cost and delivery speed:

Add a `Cost` property that returns the delivery cost depending on priority and the distance between locations (calculated as the length of the vector in the Cartesian system), multiplied by a factor:

- 100 - if the order priority is a combination of `Standard` and `Fragile`,
- 200 - if the priority is a combination of `Express` and `Fragile`,
- 50 - in all other cases.

Add a `DeliverySpeed` property that returns the delivery speed as the ratio of the distance between locations to the order processing time, expressed in hours.

If it is not possible to determine the cost of the order, these properties should return `null`.

#### Useful links:

- [Enumeration types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum).
- [System.FlagsAttribute class](https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-flagsattribute).
- [Nullable value types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types).
- [Nullable reference types](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references).

### Stage04: Indexers and the `Random` class

#### The `PackageManager` class:

In the `Repository` class located in the Models folder, uncomment the `_locations` and `_customers` collections. Implement the `DrawLocation` and `DrawCustomer` methods, which accept a `Random` object and return a randomly selected element from the respective collections.

In the `Services` folder, create a `PackageManager` class with the following implementations:

- The `CreatePackage` method, which returns a `Package` object. Use `12345` as the seed for generating size and weight. Each created package should be stored within the class's state (in a collection of your choice).
  - `Size` and `weight` should be randomly selected from the range `[10, 100)`.
  - The shipping date should be set to the current date, offset (in the past) by a number of days randomly selected from the range `[1, 10]`.
  - The delivery date, in 25% of cases, is unknown (`null`), while in the remaining cases, it should be offset (into the future) by a number of days randomly selected from the range `[1, 10]`.
  - All other reference fields should be fetched from the `Repository` class.
- The `MakeReport` method, which prints a message to the console for each package created by the `PackageManager` object in the format:
  - `Warsaw (at [Long date pattern]) => Berlin (at [Long date pattern])`.
  - `Warsaw (at [Long date pattern]) => Berlin (not delivered yet)` if `DeliveredAt` is `null`.
  - Dates should be formatted using appropriate `CultureInfo` objects.
- An indexer that accepts a parameter of type `System.Range` and returns a part of the `Packages` collection corresponding to the given range (in the body of the indexer, you have to use the `for` loop - it is forbidden to index the `Packages` collection directly by the range parameter).
- An indexer that accepts `from` and `to` parameters of type `DateTime` and returns all orders whose processing time falls within the range `[from, to]`.

#### Useful links:

- [Random Class](https://learn.microsoft.com/en-us/dotnet/api/system.random?view=net-8.0).
- [Random.Shared Property](https://learn.microsoft.com/en-us/dotnet/api/system.random.shared?view=net-8.0#system-random-shared).
- [Indexers](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/).
- [Index Struct](https://learn.microsoft.com/en-us/dotnet/api/system.index?view=net-8.0).
- [Range Struct](https://learn.microsoft.com/en-us/dotnet/api/system.range?view=net-8.0).
