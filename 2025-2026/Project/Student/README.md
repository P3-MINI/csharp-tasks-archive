# Native Code Interoperability

**Total Points:** 8

## 1. Overview
In this assignment, you will implement a high-performance communication layer between a native C++ ray tracing engine and a managed C# application. You are provided with the core mathematics and physics code for "Ray Tracing in One Weekend" (including a progressive renderer in `camera.h`) and a pre-built Avalonia-based Windowing library (`Windowing`).

Your goal is to build the `RayTracing` library (containing both native C++ code and C# bindings) and the `RayTracingDemo` console application to drive the rendering process and visualize the results in real-time.

## 2. Requirements

### Part 1: The Native Export Layer (C++) [2 Points]
**File:** `RayTracing/native/export.cpp`

You must create a C-compatible ABI to expose the provided C++ core classes and utilities.

1.  **Opaque Handles:** Create functions to instantiate (`CreateScene`, `CreateMaterial`, etc.) and return pointers to these objects. Do not expose C++ class layouts directly to C#.
2.  **Memory Management:** Implement corresponding `Destroy...` functions for every creator function.
3.  **Render Function:** Implement `RenderScene` that:
    *   Accepts a `CameraConfig` struct (passed by value) containing all camera parameters.
    *   Instantiates a local `camera` object and populates it with the config data.
    *   Calls the camera's built-in `render` method, passing the scene world, the output buffer pointer, and the callback.
4.  **Callback Specification:**
    *   The render function must accept a function pointer with the signature:  
        `typedef void (*RenderCallback)(int samples, uint8_t* buffer);`
    *   Pass this callback to the camera's render method.
5.  **Image Output:** Provide a `SavePng` function that utilizes the included `stb_image_write` library to save the RGBA buffer to a file.

### Part 2: Low-Level Bindings (C#) [2 Points]
**File:** `RayTracing/NativeMethods.cs`

Bind the exported C functions to .NET.

1.  **P/Invoke:** Preferably use the `[LibraryImport]` source generator.
2.  **SafeHandles:** Use `SafeHandle` for all native resources requiring cleanup.
    *   Implement `SceneSafeHandle` and `MaterialSafeHandle`.
    *   The runtime must automatically invoke the C++ `Destroy` functions via these handles.
3.  **Delegates:** Define the `RenderCallback` delegate matching the C++ signature.
4.  **String Marshalling:** Ensure the `SavePng` binding correctly handles string marshalling for the filename.

### Part 3: High-Level Safe API (C#) [2 Points]
**File:** `RayTracing/*.cs`

Create an idiomatic C# object-oriented wrapper that ensures memory and type safety.

1.  **Safety:** The public API must never expose `IntPtr`/`nint` or pointers to the consumer.
2.  **Resource Ownership:** Implement the `IDisposable` pattern to manage the lifetime of the native scene handles.

### Part 4: Demo & Integration [2 Points]
**File:** `RayTracingDemo/Program.cs`

Implement the main demo application using your library and the provided `Windowing` library.

1.  **Scene Setup:** Procedurally generate the iconic scene from the **cover of the first book** (*Ray Tracing in One Weekend*).
2.  **Visual Integration:** Use `Windowing.Viewer.Show` to spawn the window and start the render loop.
3.  **Live Feedback:**
    *   Update the window's status text with the rendering progress (samples/time).
    *   Update the window's image buffer in real-time using the data provided in the callback.
4.  **Output:** Once rendering is finished, use your `SavePng` method to save the final image as `output.png`.

## 3. Build

### Build
The project uses CMake integrated with MSBuild. Building the C# solution will automatically trigger the C++ build.
```bash
dotnet build -c Release
dotnet run -c Release --project RayTracingDemo/RayTracingDemo.csproj
```