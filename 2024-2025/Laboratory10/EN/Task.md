# Laboratory 10 - Streams and Serialization
## Introduction
> *As a computer science student, you are part of a team working together to create a game for your group project assessment. After long discussions and brainstorming, your team decided on an RPG genre in which the player takes on the role of a duck navigating the endless ocean with its flock, trying to find its way home. As a team member, you have been entrusted with a key task: implementing the scene loading mechanism as well as a quick save and load function. It is up to you to ensure the player can freely continue their adventure after closing the game. The challenge is significant, but solving these problems is the key to the success of the entire production.*

## Project Structure

The solution consists of 2 projects: 
* **Duck** – the game project where the task is assigned to you,
* **OpenGL** – the project responsible for low-level communication with the graphics card.

Most of your work will focus on the `Scene` class. This is where you will implement the key functions related to your task. Additionally, you will need to introduce minor changes to the classes subject to serialization. These changes will only require you to add specific attributes to control the serialization process.

## Stage 1 (2 points)
> *What would a duck be without its loyal flock? Your first challenge will be to implement a mechanism for loading the scene, which will define the initial arrangement of ducks in the flock. This is a key feature that will allow the player to immediately bond with the group and immerse themselves in the game world.*

In the `Scene.cs` file, start with the constructor, which takes the path to a file embedded as a resource in the executable. This is where you will implement the logic for loading the flock of ducks data. Information about the ducks is stored in the file `Resources/ducks.bin`, which contains data in binary format with ASCII-encoded characters, following this specification:

1. The first two bytes encode an unsigned short representing the number of ducks serialized in the file.
2. Each duck's data is then represented, consisting of:
   - The duck's name – a string.
   - Position in the world – X and Z coordinates, indicating the duck's location on the plane in 3D space (2 floats).
   - Rotation – the orientation of the duck in degrees, representing its direction (1 float).
   - Scale – the proportions of the duck, determining its size in the game world (1 float).

The parameters loaded from the `ducks.bin` file map directly to the arguments required by the duck object's constructor. Be sure to convert rotation values from degrees to radians when passing them to the constructor. Newly created duck objects should be then added to the `Ducks` list, which functions as a container for all the ducks present in the current scene.

The `Ducks` list is a crucial element of the game engine. Once ducks are added to this list, they will automatically appear in the virtual world, visible on the scene and ready for interaction. This mechanism lets players instantly immerse themselves in their duck adventure, surrounded by a thoughtfully positioned flock that enhances the gameplay experience.

## Stage 2 (3 points)
> *The next essential step will be implementing the quick save option. What if the player makes poor decisions and wants to return to an earlier game state? And what if they need to pause the game and return to it later? Your task will be to create a system that saves key game elements, such as the current settings of the ducks – their positions, rotations, and scales – allowing players to continue their adventure at any time conveniently.*

In this stage, your task is to implement the `QuackSave` function in the `Scene.cs` file. This function will be responsible for serializing the entire scene into XML format. To further optimize the space occupied by the quick save on the player's disk, you will need to use a compression mechanism – you can choose any, such as GZip or Deflate. Add the appropriate attributes to the `Duck` class where necessary to enable correct serialization. **Do not make any modifications to Duck.cs other than adding attributes.**

Set up the quick save function so that the file is saved in the user's Documents directory in a dedicated subdirectory named `Duck` under the filename `quack.save`.

Avoid serializing unnecessary properties. Do not serialize the texture, mesh, or billboard (Mesh, Texture, and NameBillboard); their serialization would be redundant since these properties can be reconstructed.

**Hints:**  
- The directory path can be obtained using the `Environment.GetFolderPath` function by passing the appropriate enum. https://learn.microsoft.com/en-us/dotnet/api/system.environment.specialfolder?view=net-9.0
- Serializing a type that is an abstract class (DuckBehaviour) can be problematic. During deserialization, the deserializer must know the possible implementations of the interface. In such cases, the interface should be marked with the `XmlInclude` attribute describing its possible implementations. https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlincludeattribute?view=net-8.0

## Stage 3 (3 points)
> *What would a quick save be without the ability to load quickly? The final stage of your task will be to implement a system that allows the game state to be restored from the previously created `quack.save` file. Thanks to this, the player will be able to return to their duck world at any moment, continuing the adventure exactly where they left off. Your work will combine both mechanisms into a cohesive system, which is essential for a smooth gameplay experience.*

Your final task will be implementing the `QuackLoad` function in the `Scene.cs` file. This function will be responsible for loading the serialized and compressed game state from the `quack.save` file. Using the built-in deserialization mechanism, recreate the saved objects and restore the scene to the exact state the player left it in. To ensure robustness, implement exception handling – if the file does not exist or cannot be correctly deserialized, the function should return `null`, indicating to the game engine that the loading process failed.
