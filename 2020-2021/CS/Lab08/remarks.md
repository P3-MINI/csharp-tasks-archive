# Remarks used during verification

//Points:
// ! No floating points

//1.5p - constructors, properties, simplify fraction and Reciprocal() method
// Properties- Numerator, Denominator(denominator > 0)
// Denominator <= 0 -> ArgumentException
// Constructors (long n) -> (set d=1) + (long n, long d)
// Simplify 2/4 -> 1/2 + Simplify during update (GreatestCommonDivisor)
// Reciprocal 2/3 -> 3/2 -> 1 1/2 (simplified)

//1.5p - converters and arithmetic operators
// implicit long -> Fraction
// explicit Fraction -> long
// explicit Fraction -> double
// Arithmetic + - * / - ; overflow + simplified

//1p - comparison operators and methods related to them
// == != < > <= >= Equals, GetHashCode

//1p - hard tests (operations on large numbers)

// Total: 5pkt


// PW: You could use default parameter in first constructor public Fraction(long n, long d =1)
// Or call here Fraction(long n) : this(n, 1)