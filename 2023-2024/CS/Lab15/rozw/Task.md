Lab15 - Retake

Make sure you're using .Net 7.0! Reuse code that you've written.  
You can do Stage_5 of the task independently.  

Stage_1 (0.5):
* Implement generic Vector3D<> struct where generic parameter is constrained to INumber<> interface.
    * Struct should contain X,Y,Z properties and appropriate constructor.
    * Implement arithmetic operators (+,-,*,/), that perform elementwise operations on individual components.
    * Struct should be written to the console with following format: '[X,Y,Z]', with 2 digits of precision.

Stage_2 (0.5):
* Implement FileSource class that implementis IEnumerable< Vector3D< float > > class.
    * Constructor takes a filepath to the source file. 
    * GetEnumerable method opens the file (binary format) and returns Vector3D. You can assume that the file contains of enough elements to construct whole Vector3Ds.

Stage_3 (2.0):
* Implement abstract generic TreeNode<> class.
    * Should containa abstract, parameterless Evaluate method that returns the value of a given node of type Vector3D<>.
* Implement generic ValueTreeNode<> class that inherits from the above TreeNode<>.
    * The class contains a private value of the type given by the parameter, the appropriate constructor and the implementation of the methods.
* Implement abstract generic ArithmeticTreeNode<> class that inherits from the above TreeNode<>.
    * Contains private properties for the right and left subtrees of type TreeNode<>.
    * Implement a constructor that takes both subtrees.
    * Implement a static Create method that returns a new object representing the appropriate arithmetic operation (AdditionTreeNode, SubtractionTreeNode, MultiplicationTreeNode, DivisionTreeNode). The method accepts the operation being performed and a left and right subtrees. Create classes inheriting from ArithmeticTreeNode<> that represent appropriate arithmetic operations that overload the Evaluete method accordingly. The method throws an ArgumentException with the message "Invalid operation: {nameof(operation)} was {operation}!" if the returned operation is invalid.
* Implement a method that extends the char type:
    * The IsArithmeticOperator method returns whether a given character is one of the supported arithmetic operators (+,-,*,/), and uses the output parameter to pass a specific operation (enum Operation). In case of any other character, it returns false and the invalid operation.

Stage_4 (1.0):
* Implement generic ExpressionTree<> class representing an expression tree. The constructor accepts a filepath to the file that contains representation of an expression in the form of [Reverse Polish Notation](https://en.wikipedia.org/wiki/Reverse_Polish_notation), supporting values in the range [0-9], separated with various whitespaces. To create an expression tree, use the following algorithm:

    ```C#
    Stack<Node> stack = new Stack<Node>();

    foreach (character in expressionString)
    {
        if (character is operator)
        {
            rightNode = stack.Pop();
            leftNode = stack.Pop();

            stack.Push(new Node(left, right));
        }
        else
        {
            stack.Push(new Node(character));
        }
    }

    Root = stack.Pop();
    ```

    * The constructor throws an ArgumentException, with the message "Invalid expression source stream - too many elements!", if the stack is non-empty after the algorithm is executed.
    * Additionally, add support for other exceptions (thrown directly by the stack, array or formatting), when the given expression contains too few elements or has an incorrect structure, throw an ArgumentException with the message "Invalid expression source stream - not enough elements or wrong expression!", when an unsupported character appears in the expression, throw an ArgumentException with the message "Invalid expression source stream - invalid symbol found!" and if index out of range exception occurs throw an ArgumentException with the message "Invalid expression source stream - invalid amount of vector components per line!".
    * You can assume that single line of a file if it contains any numbers it contains at least one full representation of Vector3D (so at least three values).
    * HINT: T.Parse, NumberStyles.Number, NumberFormatInfo.InvariantInfo.

Stage_5 (1.0):
* Change the implementation of PrimesSequence so it extends IEnumerable< long > and IAsyncEnumerable< long > that return all prime numbers in sequence and asynchronous manners respectively.
     * The algorithm solves the problem asynchronously. Use the available implementation of the IsPrime method.
     * Your algorithm should be at least 2 times faster than the synchronous version.
     * HINT: Task, Environment.ProcessorCount.
