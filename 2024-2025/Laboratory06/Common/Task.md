# Laboratory 6 - Collections and LINQ
## 0 - Introduction
>*Our team is proud to announce our newest development in graphics technology. They have implemented a high-end graphic engine for our newest game, **God of Ducks**! You have drawn the short straw and are tasked with implementing the game's logic.*

During today's laboratory, you will use your knowledge and skills in using Collections and LINQ to work on a partially complete game. In the task's repository, a solution contains the initial code for the tasks. You can launch it, resize the console window, change the font size and walk around using the WASD keys. There is not much going on there yet, but that will change as you finish the tasks.
### 0.1 - Solution structure
The solution contains 4 projects:
* **Common** - definitions shared across the solution,
* **Play** - responsible for launching the application and displaying the content of the game,
* **UnitTest** - unit tests you can utilize to check your tasks for correct results,

and finally,
* ***Task*** - the project you will edit to complete your tasks.
## Task 1 - `Map` generation (2 points)

>*The first, most crucial part of every game is level generation. Our design team has prepared sets of rooms to be placed in the play area. Your task is to figure out what they meant and bring their artistic visions to life:*

Implement the constructor for the class `Map`.
* The `Map` should hold its own copies of the rooms and items it contains.
* The `Data` array should have hollow rectangles of `Tile.Wall` and a correctly placed `Tile.Door` where the rooms are.
* `Data` should have the smallest possible size.
```
Room at (1,2), (4,5), with a door at (1,3):
f - floor, w - wall, d - door
Data:
 fffff
 fffff
 fwwww
 fdffw
 fwffw
 fwwww
```
## Task 2 - `Search` function (2 points)
>*Our players have complained that the game is tiring on the eyes and finding items is difficult. We're going to implement a function that will help them search! It would of course be too overpowered if they could search the whole level, so we'll restrict it to just the room the player is in.*

Implement the `Search` function within the `Map` class. Given the coordinates of a tile, this function should return a list of all items located in the same `Room` as the tile. The function should return an empty list if the tile is not within any `room`.
## Task 3 - `Collect` and `Interact` functions (1 point)
>*Whoops! We've forgotten to implement the player's inventory. The player was supposed to be able to interact with and pick up the items they were searching for.*

Implement the `Interact` function within the `Map` class. This function should return the string of the item in the given position. If there is no item present, it should return an empty string.

Implement the `Collect` function within the `Player` class. This function should add a new item to the player's equipment. If the item is already present in the equipment, increase its quantity. if not, set its quantity to 1.

## Task 4 - `Enemy` class (1 point)
>*Our game lacks real excitement, and we have heard that enemies will add just that! We've never seen how enemies work in games, so we'll try to come up with our own extremely exciting and adrenaline-pumping movement schema:*

Complete the `Enemy` class. At creation, an Enemy is assigned a collection of points that define its patrol points. When prompted to move, it teleports from its patrol path to the next location. When it reaches the end, it returns to the first position and starts the cycle again.

**Example:**
The enemy receives {(1,0), (5, 1), (4,5)}. Every time the game moves it, it does so in the following sequence: (1,0), (5,1), (4,5), (1,0), (5,1)....

`MoveNext` - Set Position to the next one from the sequence.

**Hint:** Consider which data structure would be the most suitable for this purpose.
## Task 5 - `CountItemsOnQuarter` Function (2 points)
>*Our players have noticed that items seem to appear abnormally often in the top left quarter of our levels. We checked our algorithms for such bias and didn't find any, but we still have to check it experimentally! We have already generated the levels and there is only one thing left to do:*

Implement the `CountItemsOnQuarter` function in the Analysis class. This function should return the number of items present in each quarter across multiple maps.

**Restriction:** You may only use LINQ expressions. Loops are not permitted. 

**Hint:** Consider using SelectMany, Select, and GroupBy to achieve the desired result. The function `WhichQuarter` may be helpful.
