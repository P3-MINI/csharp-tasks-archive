0) klasa abstrakcyjna jest zaimplementowana
1) zaimplementować jedna Circle / Rectangle
pola są oznaczone jako readonly
dodać aby obie wyznaczały obwód
Sprawdzić czy finalizatory się uruchamiają
(to jest metoda protected)
Wywołać Clone i sprawdzić że to jest inna klasa
2) Zaimplementować w klasie Shape2D
ciało metody ScaleShape2D
3) jest Stworzona klasa Cone / Cuboid
zaimplementować dla nich klasę bazową Shape3D
metody abstrakcyjne
gadający konstruktor
można dodać metodę potrzebną do dostępu do prywatnych pól w klasach Shape2D
4) Zaimplementuj klasę drugą dziedziczącą po Shape3D Cylinder / Pyramid
Pyramid w podstawie kwadrat o boku a, wysokość h
Pole = 2 * a * s + a ^ 2
Objętość = 1 / 3 * b ^ 2 * h
Sprawdzić że pole statyczne wyświetli inną ilość obiektów dla Shape3D a Pyramid i Cylinder


Uwaga: Każda klasa powinna posiadać "gadający" konstruktor
Który wypisze informację że jest wołany konstruktor i
numer obiektu danego typu np:
"Constructor Shape2D (1)"
Każda klasa powinna również posiadać gadający finalizator
wypisze że jest wołany finalizator i numer obiektu danego typu np:
"Finalizer Shape2D (1)"

Co można sprawdzić:
Pole readOnly
Statyczny pole dobrze nadpisane
Dobrze metoda odziedziczona overwrite
Złamane dziedziczenie new
Wymaganie aby wypisywało konstruktory
zawołanie jednego konstruktora z drugiego
Kolekcję obiektów
Kompilacja nie powinna dawać żadnych ostrzeżeń(warning). e.g.Warning CS0108