Laboratorium 15 - Retake

Upewnij się, że korzystasz z wersji .Net 7.0! Korzystaj z napisanego już kodu.  
Maksymalna liczba punktów do usyzkania to 6.0.  
Zależność miedzy etapami jest następująca:
* Etap 5 można wykonać niezleżnie.

Etap_1 (1.0): 
* Zaimplementuj klasę FileSource implementującą interfejs IEnumerable<char>.
    * Stwórz konstruktor, który przyjmuje ścieżkę do pliku.
    * Metoda GetEnumerator otwiera plik (zapisany w postaci binarnej), a następnie zwraca kolejne przeczytane znaki.

Etap_2 (2.0):
* Zaimplementuj abstrakcyjną klasę uogólnioną TreeNode<>, której parametr ogranicz do interfejsu INumber<>.
    * Powinna ona zawierać abstrakcyjną bezparametrową metodę Evaluate, zwracającą wartość danego węzła.
* Zaimplementuj klasę uogólnioną ValueTreeNode<> dziedziczącą po powyższym TreeNode<>.
    * Klasa zawiera prywatną wartość typu zadanego parametrem, odpowiedni konstruktor i implementację metod.
* Zaimplementuj abstrakcyjną klasę uogólnioną ArithmeticTreeNode<> dziedziczącą po powyższym TreeNode<>.
    * Zawiera prywatne właściwości referencji na prawe i lewe poddrzewo typu TreeNode<>.
    * Zaimplementuj konstruktor przyjmujący referencje na poddrzewa.
    * Zaimplementuj statyczną metodę Create zwracającą nowy obiekt reprezentujący odpowiednią operację arytmetyczną (AdditionTreeNode, SubtractionTreeNode, MultiplicationTreeNode, DivisionTreeNode). Metoda przyjmuje wykonywaną operację oraz referencję na lewe i prawe poddrzewo. Stwórz klasy dziedziczące po ArithmeticTreeNode<> reprezentujące odpowiednie operacje arytmetyczne, które w odpowiedni sposób przeciążają metodę Evaluete. Metoda zgłasza wyjątek ArgumentException z wiadomością *"Invalid operation: {nameof(operation)} was {operation}!"*, jeśli zwrócona operacja jest niepoprawna.
* Zaimplementuj metodę rozszerzającą typ char:
    * Metoda IsArithmeticOperator zwracająca czy dany znak jest jednym ze wspieranych operatorów arytmetycznych (+,-,*,/), a za pomocą parametru wyjściowego przekazuje konkretną operacje (enum Operation). W przypadku innego znaku zwraca false oraz odpowiednią operację. 

Etap_3 (1.0):
* Zaimplementuj uogólnioną klasę ExpressionTree<> reprezentującą drzewo wyrażeń.
    Konstruktor przyjmuje obiekt klasy FileSource reprezentujący wyrażenie w postaci [Odwrotnej Notacji Polskiej](https://pl.wikipedia.org/wiki/Odwrotna_notacja_polska), wspierający wartości z przedziału [0-9], podawane bez spacji.
    W celu stworzenia drzewa wyrażeń zastosuj nastepujący algorytm:

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
    * Konstruktor zgłasza ArgumentException, z wiadomością *"Invalid expression source stream - too many elements!"*, jeśli po wykonaniu algorytmu stos jest niepusty.
    * Dodatkowo dodaj obsługę innych wyjątków (zgłaszanych bezpośrednio przez stos lub formatowanie), kiedy zadane wyrażenie zawiera za mało elementów lub posiada niepoprawną strukturę zgłoś ArgumentException z wiadomością *"Invalid expression source stream - not enough elements or wrong expression!"* oraz kiedy w wyrażeniu pojawił się nieobsługiwany znak zgłoś ArgumentException z wiadomością *"Invalid expression source stream - invalid symbol found!"*.
    * HINT: T.Parse, NumberStyles.Number, NumberFormatInfo.InvariantInfo.

Etap_4 (0.5):
* W klasie ExpressionTree<> dodaj następujące operatory:
    * Operatory arytmetyczne (+,-,*,/) zwracające nowe drzewo będące wynikiem zadanej operacji na dwóch drzewach podanych jako parametry.
    * niejawny operator rzutowania na typ parametru T, który zwraca wynik operacji reprezentowanej tym drzewem.

Etap_5 (1.5):
* Zaimplementuj metodę rozszerzającą FindPrimesAsync zwracającą listę liczb pierwszych z sekwencji IEnumerable< long >.
    * Algorytm rozwiązuje problem asynchronicznie. Skorzystaj z dostępnej implementacji metod IsPrime i/lub FindPrimes.
    * Twój algorytm powinien być conajmniej 2 razy szybszy niż wersja synchroniczna.
    * HINT: Task, Environment.ProcessorCount.
