# Programowanie 3 - Zaawansowane
## Laboratory 1 - MSBuild & .NET CLI

0. Lecture / Laboratory Schedule, Subject Rules and Organization:
    * Lecture, mgr inż. Tomasz Herman: https://pages.mini.pw.edu.pl/~hermant/zajecia/programowanie-3/
    * Laboratories, mgr inż. Cezary Bella: https://pages.mini.pw.edu.pl/~bellac/P3Z_2024Z/schedule.html
    * Rules: [https://usosweb.usos.pw.edu.pl](https://usosweb.usos.pw.edu.pl/kontroler.php?_action=katalog2/przedmioty/pokazPrzedmiot&kod=1120-IN000-ISP-0234)

1. What is MSBuild?

    MSBuild [(Microsoft Build Engine)](https://learn.microsoft.com/en-us/visualstudio/msbuild/walkthrough-using-msbuild?view=vs-2022) is the build platform used by Visual Studio, .NET Core, and .NET Framework projects to compile source code, generate executables, package applications, and handle project dependencies. Its most popular use case is automation of build processes (like CI/CD platforms).

    It's operating on an XML-based file that describes actions and their properties and configuration to perform on given projects/files.

    Basic components of MSBuild, that being files and XML tags, are listed below:

    * **Project File**: XML-based file that define the project configuration and how MSBuild should build it. Usualy *\*.csproj / \*.vcsproj* files, but the extension is not relevant (it is recommended to use *proj* to indicate that it is a MSBuild project).
        ```xml
        <?xml version="1.0" encoding="utf-8"?>
        <Project Sdk="Microsoft.NET.Sdk">
        ...
        ```
        Where ```Sdk``` indicates which SDK (Software Development Kit) your project is going to target. It is a set of targets and associated tasks that are responsible for compiling and publishing your code. Some properties and tasks are also defined within the SDK. Take a look yourself:
        ```
        C:\Program Files\dotnet\sdk{version}\Sdks{Sdk}\targets\
        ```
        For more information about SDKs, please refer to [the dedicated page](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview) in the documentation.

    * **Tasks**: They are the smallest units of work. Tasks are independent executable components, which can have inputs and outputs. Full list of tasks is available in [MSBuild Task Reference Documentation](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-task-reference?view=vs-2022).
        ```xml
        <Delete Files="..."></Delete>
        <Message Text="Hello World"></Message>
        <CombinePath BasePath="..." Paths="...">
            <Output TaskParameter="..." ItemName="..."/>
        </CombinePath>
        <GetReferenceAssemblyPaths ...></GetReferenceAssemblyPaths>
        <Exec Command="copy '$(ProjectDir)Resources.txt' $(OutDir)"/>
        ```
    * **Targets**: They are named sequences of Tasks. Defines steps in the build process, like ```Build```, ```Clean```, ```Restore```.
        ```xml
        <Target Name="HelloWorld">
            <Message Text="Hello"></Message>
            <Message Text="World"></Message>
        </Target>
        ```
        ```xml
        <Target Name="DeleteDebugSymbolFile">
            <Delete Files="$(OutDir)$(AppName).pdb">
                <Output TaskParameter="DeletedFiles" ItemName="DeletedList"/>
            </Delete>
            <Message Text="Deleted files: '@(DeletedList)'"/>
        </Target>
        ```
    * **Properties**: They are name-value pairs that define configuration settings, like ```Configuration=Debug``` or ```Platform=AnyCPU```. All properties are child elements of ```PropertyGroup``` elements. The name of the property is the name of the child element, and the value of the property is the text element of the child element.
        ```xml
        <PropertyGroup>
            <TargetFrameworkVersion>v4.5</TargetFrameworkVersion>
            <ProductVersion>10.0.11107</ProductVersion>
            <SchemaVersion>2.0</SchemaVersion>
            <ProjectGuid>{30E3C9D5-FD86-4691-A331-80EA5BA7E571}</ProjectGuid>
            <OutputType>WinExe</OutputType>
        </PropertyGroup>
        ```
    * **Items**: Collection of inputs for Tasks, typically filenames, like source files to compile. Items are children of ```ItemGroup``` elements. The name is the name of the child element, and the value is the value of the Include attribute of the child element. 
        ```xml
        <ItemGroup>
            <Compile Include="Program.cs"/>
            <Compile Include="MyClass.cs"/>
        </ItemGroup>
        ```
        ```xml
        <ItemGroup>
            <Compile Include="Program.cs ; MyClass.cs"/>
        </ItemGroup>
        ```
        As shown above, you can include specific files by listing them individually, or use wildcards to include many files within single item:
        ```xml
        <ItemGroup>
            <Compile Include="**/*.cs"/>
        </ItemGroup>
        ```

    * You might have noticed things like ```$(OutDir)``` or ```@(DeletedList)```, these are more advanced features of MSBuild, that allow you to examine values of both properties and items respectively. In other words in MSBuild, ```@(ItemType)``` denotes an item collection, allowing you to reference and manipulate groups of files or other items, while ```$(PropertyName)``` refers to a property, which represents a single value.

    * You can get the value of any property by using ```$``` symbol followed by the name of the property:
        ```xml
        $(PropertyName)
        ```
        Example:
        ```xml
        <Target Name="HelloWorld">
            <Message Text="Configuration is $(Configuration)" />
            <Message Text="MSBuildToolsPath is $(MSBuildToolsPath)" />
        </Target>
        ```
        There are also many properties already defined for you to use. For more information about properties, please refer to [the dedicated page](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-properties?view=vs-2022#define-and-reference-properties-in-a-project-file) in the documentation.

    * You can get the value of any item by using ```@``` symbol followed by the name of the item:
        ```xml
        @(ItemType)
        ```
        Example:
        ```xml
        <Target Name="HelloWorld">
            <Message Text="Compile item type contains @(Compile)" />
        </Target>
        ```
        You can read more about that on the [dedicated page](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-items?view=vs-2022#reference-items-in-a-project-file) in the documentation.

    Another aspect of MSBuild is that it supports *Conditional Properties*. Many properties like *Configuration* are defined conditionally, that is, the *Condition* attribute appears in the property element. *Conditional Properties* are defined or redefined only if the condition evaluates to "true":
    ```xml
    <Configuration Condition="'$(Configuration)' == ''">Debug</Configuration>
    ```
    Undefined properties are given the default value of an empty string, that means that above expands to "If the Configuration property hasn't been defined yet, define it and give it the value 'Debug'."

    One can also use [*Reserved Properties*](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-reserved-and-well-known-properties?view=vs-2022) or [*Environment Variables*](https://learn.microsoft.com/en-us/visualstudio/msbuild/how-to-use-environment-variables-in-a-build?view=vs-2022), please refer to documentation for more details.

    Please note that conditions can be applied to any other tag group in MSBuild. For more information, please refer to the dedicated page in the documentation.

    To build your solution, you need to perform some commands in your console. Here are some useful **MSBuild Commands**:
    * To build a project: ```msbuild <project_file>.csproj```
    * To specify a target (like only clean or restore): ```msbuild <project_file>.csproj /t:Clean```
    * To set a property: ```msbuild <project_file>.csproj /p:Configuration=Release```

2. MSBuild Workshop:
    * Inside the folder of your choice, create HelloWorld folder, then create ***HelloWorld.cs*** file, then copy and paste the following code:
        ```CSharp
        using System;

        class HelloWorld
        {
            static void Main()
            {
                Console.WriteLine("Hello, world!");
            }
        }
        ```

    * We do have a minimal application source file, so we can create a minimal project file to build the application. This project file contains the following elements:
        * The required root ```Project``` node.
        * An ```ItemGroup``` node to contain item elements.
        * An item element that refers to the application source file.
        * A ```Target``` node to contain tasks that are required to build the application.
        * A ```Task``` element to start the C# compiler to build the application.

    * Create new file ***HelloWorld.myproj*** and copy and pase following code:
        ```xml
        <Project>
            <ItemGroup>
                <Compile Include="HelloWorld.cs"/>
            </ItemGroup>
            <Target Name="Build">
                <Csc Sources="@(Compile)"/>
            </Target>
        </Project>
        ```

    * Build the application using project that you just created:
        ```bash
        msbuild HelloWorld.myproj -t:Build -verbosity:detailed
        ```

    * Now let's delete existing executable manually and add more to your project file.

    * Add ```AssemblyName``` and ```OutputPath``` properties to customize the process. Remember about ```PropertyGroup```:
        ```xml
        <PropertyGroup>
            <AssemblyName>MSBuildHelloWorld</AssemblyName>
            <OutputPath>Bin\</OutputPath>
        </PropertyGroup>
        ```

    * Add another task to your target, it should happen before compilation process:
        ```xml
        <MakeDir Directories="$(OutputPath)" Condition="!Exists('$(OutputPath)')"/>
        ```
        The ```MakeDir``` task creates a folder that is named by the *OutputPath* property, provided that no folder by that name currently exists.

    * Modify compilation task with additional parameters, to indicate the ```OutputAssembly``` name:
        ```xml
        <Csc Sources="@(Compile)" OutputAssembly="$(OutputPath)$(AssemblyName).exe"/>
        ```
        This instructs the C# compiler to produce an assembly that is named by the *AssemblyName* property and to put it in the folder that is named by the *OutputPath* property.

    * So your project file should look like this. 
        ```xml
        <Project>
            <PropertyGroup>
                <AssemblyName>MSBuildHelloWorld</AssemblyName>
                <OutputPath>Bin\</OutputPath>
            </PropertyGroup>
            <ItemGroup>
                <Compile Include="HelloWorld.cs"/>
            </ItemGroup>
            <Target Name="Build">
                <MakeDir Directories="$(OutputPath)" Condition="!Exists('$(OutputPath)')"/>
                <Csc Sources="@(Compile)" OutputAssembly="$(OutputPath)$(AssemblyName).exe"/>
            </Target>
        </Project>
        ```

    * Let's test your setup by building your project using following command:
        ```bash
        msbuild HelloWorld.myproj -t:Build
        ```

    * Your new executable should be in *Bin* folder this time, run using following command:
        ```bash
        Bin\MSBuildHelloWorld
        ```

    * If everything works, we're going to add more targets to our project file.

    * Add *Clean* and *Rebuild* targets, they're going to delete existing executables and/or build the project again:
        ```xml
        <Target Name="Clean" >
            <Delete Files="$(OutputPath)$(AssemblyName).exe"/>
        </Target>
        <Target Name="Rebuild" DependsOnTargets="Clean;Build"/>
        ```
        The *Clean* target invokes the *Delete* task to delete the application. The *Rebuild* target does not run until both the *Clean* target and the *Build* target have run. Although the *Rebuild* target has no tasks, it causes the *Clean* target to run before the *Build* target.

    * Indicate default target for your project by changing ```Project``` element:
        ```xml
        <Project DefaultTargets="Build">
        ```

    * Your project file should look like this:
        ```xml
        <Project DefaultTargets="Build">
            <PropertyGroup>
                <AssemblyName>MSBuildHelloWorld</AssemblyName>
                <OutputPath>Bin\</OutputPath>
            </PropertyGroup>
            <ItemGroup>
                <Compile Include="HelloWorld.cs"/>
            </ItemGroup>
            <Target Name="Build">
                <MakeDir Directories="$(OutputPath)" Condition="!Exists('$(OutputPath)')"/>
                <Csc Sources="@(Compile)" OutputAssembly="$(OutputPath)$(AssemblyName).exe"/>
            </Target>
            <Target Name="Clean">
                <Delete Files="$(OutputPath)$(AssemblyName).exe"/>
            </Target>
            <Target Name="Rebuild" DependsOnTargets="Clean;Build"/>
        </Project>
        ```

    * Play around with newly created targets, see what happens when you use one or the other:
        * Building the default build.
        * Setting the application name at the command prompt.
        * Deleting the application before another application is built.
        * Deleting the application without building another application.

    * Another thing we can do with MSBuild is to build out a solution incrementally, that means only when source code has changed. To achieve that, you need to modify your target again to specify that the target depends on the input files that are specified in the *Compile* item group, and that the output target is the application file:
        ```xml
        Inputs="@(Compile)" Outputs="$(OutputPath)$(AssemblyName).exe"
        ```

    * The resulting *Build* target should resemble the following code:
        ```xml
        <Target Name="Build" Inputs="@(Compile)" Outputs="$(OutputPath)$(AssemblyName).exe">
            <MakeDir Directories="$(OutputPath)" Condition="!Exists('$(OutputPath)')"/>
            <Csc Sources="@(Compile)" OutputAssembly="$(OutputPath)$(AssemblyName).exe"/>
        </Target>
        ```

    * Test your project file by running the following command:
        ```bash
        msbuild -v:d
        ```
        Remember that *HelloWorld.myproj* is the default project file, and that *Build* is the default target. The ```-v:d``` switch is an abbreviation of ```-verbosity:detailed``` that you used previously. If you already built the output, these lines should be displayed:
        ```
        Skipping target "Build" because all output files are up-to-date with respect to the input files.
        ```
        MSBuild skips the *Build* target because none of the source files have changed since the application was last built.

    * Final form of your *HelloWorld.myproj* project file should be as follows:
        ```xml
        <Project DefaultTargets="Build">
            <PropertyGroup>
                <AssemblyName>MSBuildHelloWorld</AssemblyName>
                <OutputPath>Bin\</OutputPath>
            </PropertyGroup>
            <ItemGroup>
                <Compile Include="HelloWorld.cs"/>
            </ItemGroup>
            <Target Name="Build" Inputs="@(Compile)" Outputs="$(OutputPath)$(AssemblyName).exe">
                <MakeDir Directories="$(OutputPath)" Condition="!Exists('$(OutputPath)')"/>
                <Csc Sources="@(Compile)" OutputAssembly="$(OutputPath)$(AssemblyName).exe"/>
            </Target>
            <Target Name="Clean">
                <Delete Files="$(OutputPath)$(AssemblyName).exe"/>
            </Target>
            <Target Name="Rebuild" DependsOnTargets="Clean;Build"/>
        </Project>
        ```

    * Now know enough to experiment more, with Visual Studio this time around. Follow teacher's instructions on how to create a console application project, which framework we're going to use etc.

    * Open *\*.csproj* file that Visual Studio created within the project. Please note that it specifies ```Sdk="Microsoft.NET.Sdk"``` argument for the project, that means it imports some basic targets/tasks/properties/items. Thus, there's not much going on in the file, and definitely there's no target nor task defined (they all come from within the SDK).

    * The ```Sdk="Microsoft.NET.Sdk"```, as mentioned in first section, defines whole bunch of targets/tasks/properties/items, all things that are helpful while managing your solutions. It does it way better than what we did in the first part of this workshop. Thus, it's recommended to use predefined elements whenever possible.

    * While keeping the project file open and visible, modify project properties using Visual Studio. After each step observe how things change in the file (remember to save the properties):
        * Change ```Output Type``` from *Console Application* to *Class Library*.
        * Change ```Target Framework``` from *.NET 8.0* to *.NET 7.0*.
        * Change ```Startup Object``` from *(None)* to *WhateverYourAppNameIs.Program*.
        * Change it back again to *(None)*.

    * Scroll down within the properties and change following options:
        * Change ```Nullable``` from *Enable* to *Disable*.
        * Change ```Base Output Path``` from *Nothing* to *Binaries*.
        * Change ```Pre-Build Events``` from *Nothing* to *copy "\$(ProjectDir)Resources.json" \$(OutDir)*.
        
        Before running *Build* command remember to create *Resources.json* file inside your project directory.

    * Take a look at what was created in project file once you've modified *Pre-Build Events* using Visual Studio.

    * Note that there's a better way of copying files. Within your project file you can specify special ```Item``` denoted ```None```, meaning that these files are not relevant for the compilation process, but are part of the project itself. Using *Update* property you can specify the file you want to perform an action on, then with ```CopyToOutputDirectory``` item you may specify what's the behavior for this particular file, options are *Never*, *Always*, *PreserveNewest*. Please follow below example to improve what we've created by altering properties in Visual Studio during previous steps (do not delete *Pre-Build Events* target yet):
        ```xml
        <ItemGroup>
            <None Update="Data\Epsilon.json">
                <CopyToOutputDirectory>Always</CopyToOutputDirectory>
            </None>
            <None Update="Data\Missions.json">
                <CopyToOutputDirectory>Always</CopyToOutputDirectory>
            </None>
        </ItemGroup>
        ```
        You can read more about [Common MSBuild Items](https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022) in the documentation.

    * Note that above can be achieved by altering properties of given file using Visual Studio, right click on the given file in *Solution Explorer*, *Properties* and change *Copy to Output Directory* property. The *Build Action* section indicates the item group given file was clasified into. Note that for our *Resources.json* it's *None*, so the same as we specified ourselves, but for source code it's *C# compiler*.

    * Within the project file, based on what *Pre-Build Event* target looks like, add *Post-Build Event* that will archive the whole output directory and place zip file in the project directory. You may want to take a more in-depth look at [build events](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview#build-events) documentation and how to use [zip](https://learn.microsoft.com/en-us/visualstudio/msbuild/zipdirectory-task?view=vs-2022) commands.


3. What is .NET CLI?
    The .NET CLI [(Command-Line Interface)](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet) is a cross-platform tool used for developing, building, running, and publishing .NET applications. It allows you to manage projects without Visual Studio, providing greater flexibility.

    NOTE: .NET CLI uses MSBuild under the hood, but provides a little bit more automation. Even though it uses MSBuild, it's limitted to operating on .Net applications, whereas MSBuild otself can be configured to compile other languages like C/C++.

    It's a console-based solution that allows you to run commands. Some of the key ones are listed below:

    * General Commands:
        * ```dotnet --help```: Prints out a list of available commands.
        * ```dotnet --info```: Prints out detailed information about a .NET installation and the machine environment, such as the current operating system, and commit SHA of the .NET version.
        * ```dotnet --version```: Prints out the version of the .NET SDK used by dotnet commands, which may be affected by a global.json file. Available only when the SDK is installed.
        * ```dotnet --list-sdks```: Prints out a list of the installed .NET SDKs.
        * ```dotnet --list-runtimes```: Prints out a list of the installed .NET runtimes. An x86 version of the SDK lists only x86 runtimes, and a x64 version of the SDK lists only x64 runtimes.

    * Project Management:
        * ```dotnet new```: Creates a new solution, project or file.
        * ```dotnet add package```: Adds a NuGet package to the project.
        * ```dotnet add reference```: Adds a reference to the project.
        * ```dotnet sln [command]```: Manages solution file.

    * Building and Running:
        * ```dotnet clean```: Cleans the output of a project.
        * ```dotnet build```: Compiles the code and produces an output.
        * ```dotnet run```: Builds and runs the application in one step.

    * Other Useful Commands:
        * ```dotnet test```: Runs unit tests in the solution or project.
        * ```dotnet watch```: Responds with specified action, when changes in source code were detected.
        * ```dotnet format```: Formats code to match editorconfig settings.
        * ```dotnet restore```: Restores all NuGet packages specified in the project file.
        * ```dotnet publish```: Prepares the application for deployment. It builds the project and packages all dependencies.

    Using .NET CLI you can create more advanced workflows to perform on your solution and project, some examples below:
    * Create a New Project:
        ```bash
        dotnet new <template> --name <project_name> -f <framework>
        ```
        The full list of available templates, can be found in official documentation's [arguments](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new#arguments) section. You can also use ```dotnet new list``` to list them in your console.

    * Build and Run:
        ```bash
        dotnet build -a <architecture> -c <configuration>
        ```
        Full documentation about *build* command is available in [dotnet build](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build) website. 
        ```bash
        dotnet run -a <architecture> -c <configuration> -f <framework>
        ```
        Full documentation about *run* command is available in [dotnet run](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run) website.

    * Add a NuGet Package:
        ```bash
        dotnet add package Newtonsoft.Json
        ```
        But before we dive in deeper, let's clarify what NuGet is? It is the package manager for .NET, that provides client tools with the ability to produce and consume packages (external libraries that you can download and use directly within your project). You can learn more on official [NuGet](https://www.nuget.org/) website and/or [dotnet restore](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-restore) command.

4. .NET CLI Workshop:

    * Create console application with *lab01* name using .net 8.0 framework:
        ```bash
        dotnet new console -n lab01 -f net8.0
        ```
        Example output:
        ```
        The template "Console App" was created successfully.

        Processing post-creation actions...
            Restoring C:\Users\...\lab01\lab01.csproj:
            Determining projects to restore...
            Restored C:\Users\...\lab01\lab01.csproj (in 74 ms).
        Restore succeeded.
        ```

    * Before we proceed with verification, we need to make sure we are in the correct directory. Since ```dotnet new``` creates a folder for your project, to build it, you need to be in the same directory as *\*.csproj* file. Thus, run the following command first:
        ```bash
        cd lab01
        ```
    * Verify whether everything is ok by building and running the solution:
        ```bash
        dotnet build
        dotnet run
        ```
        Example output:
        ```
        MSBuild version 17.7.3+8ec440e68 for .NET
            Determining projects to restore...
            All projects are up-to-date for restore.
            lab01 -> C:\Users\...\lab01\bin\Debug\net8.0\lab01.dll

        Build succeeded.
            0 Warning(s)
            0 Error(s)

        Time Elapsed 00:00:02.66
        ```
        ```
        Hello, World!
        ```
        Note that, by default, the solution is being built with *Debug* Configuration, but you can specify both Architecture and Configuration yourself.

    * There's no solution yet, you've just created a single project outside the solution. To create solution and group projects together, you need to run some more commands. But before we can proceed, we need to establish whether we want our *\*.sln* file to be in the same directory as our *\*.csproj* file or whether we want to create a hierarchy for our solution.

        If you want your *\*.sln* to be in the same directory:
        ```bash
        dotnet new sln
        dotnet sln add lab01.csproj
        ```
        Example output:
        ```
        Dodano projekt „lab01.csproj” do rozwiązania.
        ```
        If you want to create hierarchy:
        ```bash
        cd ..
        dotnet new sln -n lab01
        dotnet sln add lab01
        ```
        Example output:
        ```
        The template "Solution File" was created successfully.
        ```
        ```
        Project `lab01\lab01.csproj` added to the solution.
        ```
        Note the difference, not only in changing directories, but especially how you need to refer to your project file specifically. Additionally, by default ```dotnet new sln``` command creates the solution's name based on the current directory's name, to override this behavior one need to provide ```--name``` an argument.

        Important: We're going to follow the hierarchy creation here, as all below commands are following this schema.

    * Verify that you've included project into solution by running following command:
        ```bash
        dotnet sln list
        ```
        Example output (solution and projects are located in the same directory):
        ```
        Project(s)
        ----------
        lab01\lab01.csproj
        ```

    * Open up your solution with Visual Studio, so you can see how all the things you've done are resembled visually.

    * Add another project to the solution. This time around create class library project:
        ```bash
        dotnet new classlib -n myClassLibrary -f net8.0
        ```
        Example output:
        ```
        The template "Class Library" was created successfully.

        Processing post-creation actions...
        Restoring C:\Users\...\myClassLibrary\myClassLibrary.csproj:
            Determining projects to restore...
            Restored C:\Users\...\myClassLibrary\myClassLibrary.csproj (in 66 ms).
        Restore succeeded.
        ```
    
    * Remember about including your project within solution you've created earlier:
        ```bash
        dotnet sln add myClassLibrary
        ```
        Example output:
        ```
        Project `myClassLibrary\myClassLibrary.csproj` added to the solution.
        ```

    * Verify that project was successfully included into solution:
         ```bash
        dotnet sln list
        ```
        Example output (solution and projects are located in folder hierarchy):
        ```
        Project(s)
        ----------
        lab01\lab01.csproj
        myClassLibrary\myClassLibrary.csproj
        ```

    * Changes should be immediately visible in Visual Studio as well (reload might be required).

    * Change default name of *Class1.cs* of your newly created class library to *MyClass.cs*.

    * Using Visual Studio modify your class library project, by altering *MyClass.cs* file with following code:
        ```CSharp
        namespace myClassLibrary;

        public class MyClass
        {
            public static string FromClassLibrary()
            {
                return "Hello from Class Library";
            }
        }
        ```

    * Using Visual Studio modify your console application project, so it prints out above string. Modify *Program.cs* file with following code:
        ```CSharp
        using myClassLibrary;

        Console.WriteLine(MyClass.FromClassLibrary());
        ```
        Note that by default, ```dotnet new console``` command uses a new template version for your C# program. You can find more information on how to use the old template version in the [documentation](https://learn.microsoft.com/en-us/dotnet/core/tutorials/top-level-templates#use-the-old-program-style-in-visual-studio).

    * Build your project again:
        ```bash
        dotnet build
        ```
        Example output:
        ```
        MSBuild version 17.7.3+8ec440e68 for .NET
            Determining projects to restore...
            All projects are up-to-date for restore.
        C:\Users\...\lab01\Program.cs(1,7): error CS0246: The type or namespace name 'myClassLibrary' could not be found (are you missing a using directive or an assembly reference?) [C:\Users\...\lab01\lab01.csproj]
            myClassLibrary -> C:\Users\...\myClassLibrary\bin\Debug\net8.0\myClassLibrary.dll

        Build FAILED.

        C:\Users\...\lab01\Program.cs(1,7): error CS0246: The type or namespace name 'myClassLibrary' could not be found (are you missing a using directive or an assembly reference?) [C:\Users\...\lab01\lab01.csproj]
            0 Warning(s)
            1 Error(s)

        Time Elapsed 00:00:01.65
        ```
        Note that the solution cannot be built. There's a missing dependency between your console application and your class library.

    * Let's fix that by adding a project reference to your console application:
        ```bash
        dotnet add <project_name> reference [-f <framework>] <project_references>
        ```
        So in our case that's going to be as follows:
        ```bash
        dotnet add lab01\lab01.csproj reference myClassLibrary\myClassLibrary.csproj
        ```
        Example output:
        ```
        Reference `..\myClassLibrary\myClassLibrary.csproj` added to the project.
        ```

    * Let's build your solution one more time:
        ```bash
        dotnet build
        ```
        Example output:
        ```
        MSBuild version 17.7.3+8ec440e68 for .NET
            Determining projects to restore...
            Restored C:\Users\...\lab01\lab01.csproj (in 113 ms).
            1 of 2 projects are up-to-date for restore.
            myClassLibrary -> C:\Users\...\myClassLibrary\bin\Debug\net8.0\myClassLibrary.dll
            lab01 -> C:\Users\...\lab01\bin\Debug\net8.0\lab01.dll

        Build succeeded.
            0 Warning(s)
            0 Error(s)

        Time Elapsed 00:00:01.47
        ```
        Note again that Debug is a default configuration. To specify another configuration, you need to provide a suitable argument.

    * Build using Release configuration:
        ```bash
        dotnet build -c:Release
        ```
        Example output:
        ```
        MSBuild version 17.7.3+8ec440e68 for .NET
            Determining projects to restore...
            All projects are up-to-date for restore.
            myClassLibrary -> C:\Users\...\myClassLibrary\bin\Release\net8.0\myClassLibrary.dll
            lab01 -> C:\Users\...\lab01\bin\Release\net8.0\lab01.dll

        Build succeeded.
            0 Warning(s)
            0 Error(s)

        Time Elapsed 00:00:01.34
        ```

    * Add NuGet *Newtonsoft.Json* package to our class library project:
        ```bash
        cd myClassLibrary
        dotnet add package Newtonsoft.Json
        ```
        Example output:
        ```
            Determining projects to restore...
            Writing C:\Users\...\AppData\Local\Temp\tmp412C.tmp
        info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
        info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
        info : Adding PackageReference for package 'Newtonsoft.Json' into project 'C:\Users\...\myClassLibrary\myClassLibrary.csproj'.
        info :   CACHE https://api.nuget.org/v3/registration5-gz-semver2/newtonsoft.json/index.json
        info : Restoring packages for C:\Users\...\myClassLibrary\myClassLibrary.csproj...
        info : Package 'Newtonsoft.Json' is compatible with all the specified frameworks in project 'C:\Users\...\myClassLibrary\myClassLibrary.csproj'.
        info : PackageReference for package 'Newtonsoft.Json' version '13.0.3' added to file 'C:\Users\...\myClassLibrary\myClassLibrary.csproj'.
        info : Writing assets file to disk. Path: C:\Users\...\myClassLibrary\obj\project.assets.json
        log  : Restored C:\Users\...\myClassLibrary\myClassLibrary.csproj (in 74 ms).
        ```
        Using command like you can also specify other parameters, like version of the package you want to install. For more information refer to [the dedicated page](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-package) in the documention.

    * You already know that you can install NuGet packages using dotnet commands. But one can also use Visual Studio to do so. Please navigate to Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution. Browse for *Newtonsoft.Json* package, select project and install. Please note that it's already installed for *myClassLibrary* project, which is indicated by current version being visible next to the project's name. Once installation is finished, you may start using the package. In case of *Newtonsoft.Json* you just need to add the following code at the top of your file:
        ```CSharp
        using Newtonsoft.Json;
        ```

    * Before we finish please make some random changes, that includes wrong placemet of parenthesis or brackets and random whitespaces, in you source files, so it resembles below code:
        ```CSharp
        namespace           myClassLibrary;

        public  class        MyClass
        {
            public    static    string FromClassLibrary()
        {
                return      "Hello from Class Library";
                        }
                    }
        ```
        You can format you code automatically usind ```(Ctrl+K, Ctrl+D)``` shortcut while in Visual Studio or with ```dotnet format``` command while usign console. You can use this command do format (with specific format rule sets) code automatically for example while pushing code to git server.

5. Create Github account and link with your USOS.
    * Visit https://ghlabs.mini.pw.edu.pl/manual for more instructions.
