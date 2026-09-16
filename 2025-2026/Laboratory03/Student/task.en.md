# Lab3: Events, Files, Filesystem, Streams
The goal of this laboratory is to finish the implementation of a note reading app. Fortunately, there is a ready framework for creating applications just like this one. You only need to implement the parts immediately interacting with the filesystem. **The stages need to be done in order.**
## Stage 1: Filesystem utilities
Prepare the following functions in `FileSystemUtils.cs`. They have several tests written for them, so you can verify your results before handing them in.
### 1.1. PrepareDirectory - 2p
Our note reading app is going to open a directory and browse it. The `string PrepareDirectory(string path)` function's role is to prepare the directory for use with the app. So, if `path` is a directory, there is nothing to be done and the function returns the same string.

If `path` points to a file with the `.zip` extension, it's an archive and it needs to be extracted into a temporary browsing environment. Create a directory in the temporary folder with the same name as the archive but without the extension. Extract the contents of the zip archive into it and return the path to that directory.

Example: when called with `"library.zip"` (and such file exists), the function would return `/tmp/library/`, `C:\Users\...\AppData\Local\Temp\library\`, or whatever the user's operating system says is the temporary folder.

If neither case is true, the function should throw a `FileSystemException` with a descriptive message.

### 1.2. CountFiles - 1p
The user might want to know how many notes they have accumulated. The `int CountFiles(string path, string suffix)` function recursively counts all files in the `path` directory whose names end with `suffix`.

## Stage 2: Directory watcher
The main strength of our note reading app is responsivity. It needs to react to changes in the browsed directory. When a file or directory is renamed, added or removed, the program must react accordingly, so that the interface matches with what is in the filesystem. This stage also has several tests prepared. 
### 2.1. Implement the `DirectoryWatcher` class - 4p
You are only allowed to enumerate the entries of the watched directory once, to initialize. Afterwards - add, remove and rename incrementally after each change to the files and directories. It exposes these as public:

- constructor with one `string` argument, specifying what directory will be watched;
- `EventHandler? DirectoryChanged` event, which is raised whenever a directory or file in the watched directory is created, destroyed or has its name changed;
- read only property `string[] Files`, returning names of all files immediately in the watched directory in a new array;
- read only property `string[] Directories`, returning names of all directories immediately in the watched directory in a new array.

It also implements the `IDisposable` interface, and should release all disposable resources it uses in the `Dispose` function.

*Hint: use `FileSystemWatcher`. Misunderstandings arise from the fact that on some systems this class raises events **before** an action is completed. It means that for a supposedly just created directory `src`, `Directory.Exists("path_to/src")` would likely return `false`. Use `NotifyFilter` to watch specifically for files or directories instead.*

### 2.2. WatchDirectory - 1p
In `FileSystemUtils.cs` create a `DirectoryWatcher` for the directory `path`. Then subscribe to `DirectoryChanged` with a function that will write out all directories in the format "d:<directory_name>" and then all files in the format "f:<file_name>". It allows you to watch all reported changes and test them yourself until you press enter. Keep the code that creates the `Log` and makes sure it gets closed. You may use `Log` for debugging. Make sure that all disposable resources get properly disposed.
## Stage 3: Note Reader
Time to bridge the functionality with an external API. Implementing an existing interface will allow us to provide functionality to be used with the prepared `NoteReader` class using `ConsolePainter` without ever having to directly interact with either. Your job is to implement two sources. After that, you can read your notes with the interactive console app. To run the app, you will need to uncomment the `#define` in `Program.cs` and `NoteReader.cs`. Control the program with up and down arrow keys, enter and escape.

### 3.1 Sources and input processing - 1p
Implement the correct interfaces and properties for `DirectorySource` and `FileSource`, so that `NoteReader` compiles. They don't have to work correctly yet.

At the end of `NoteReader`'s constructor subscribe to `inputEventGenerator.Input` with a lambda function, so that input events are processed by `HandleInput`. Do not change the function signature of `HandleInput`.

### 3.2. DirectorySource - 2p
It allows viewing and selecting from directories and files in the given directory. It implements the `IDataSource<string>` and `IDisposable` interfaces. It should be up to date with the filesystem at all times.

- The source's `Name` will be the path to the watched directory. 
- `Data` is an `IEnumerable` of `string`s build from a character showing if this is a file or directory and whether it's selected, as well as the entry's name. Use the markers provided in the comment above the class. All directories should also have the total, recursive number of files whose names end in `en.md` after its name.
  - Example: a selected directory named `Lab06`, containing `index.en.md` will be represented by `»Lab06 (1)`.
- The source's `Count` property gives the total length of the series.
- `DataChanged` is an event alerting whenever the data is changed in any way. That involves changes in entry names and selection changes.
- `Dispose` releases all resources that need to be disposed.

This class also exposes several functions and properties that interact with the selection of a particular entry. 
- property `int Select` is publicly gettable, but privately settable. When `Data` changes, it should always point to a valid `string` in `Data`, but not necessarily the same one. When there's no `string` available, it should be -1. 
- `Selected` provides the name of the selected entry, without any markers.
- `SelectUp` and `SelectDown` respectively decrease and increase `Select` by one, with boundary checks.
### 3.3. FileSource - 1p
This data source represents a text file in the filesystem. It implements the same interfaces as `DirectorySource`. It simply reads the lines in the file and makes it available under `Data`. When and only when the file is written to in any way (detected by a change to the file's last write), the data need to be replaced by the new content lines. Some operating systems might need to busy wait ( :( ) until `Guard.FileReadAvailable` says that the file is available for reading.