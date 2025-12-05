# LAB 1 -eng

## Intro

### Info about me
- p.wolszakiewicz@mini.pw.edu.pl
- Graduated in 2008 Warsaw University 
- 2019 Postgraduate studies in DataScience, algorithms and applications for BigData problems
- 12 years of professional experience as 
Developer, Designer, sometimes in role as Architect, Team Leader or ScrumMaster
- Last 3 years working with Azure Cloud
- This is my first year of work as assistant 
- I am pragmatic and best learning by doing
- Hope that it would be interesting journey for both me and you
- consultations (online) on Monday 13 - 14, please write me e-mail in advanced
E-mail: p.wolszakiewicz@mini.pw.edu.pl

### Technical intro about laboratories

- few (two) lectures will be introduction
- next one will be the one to gather the points  
  Using zoom to share a screen
  LiveShare to share a code and solve some issues
  Sending results to me at the end
- There is option to write advanced solution in C++  
  and send it to gather extra points
  and then your presence on intro is no needed  
  or if you prefers you need to be present


### How todays lab will look like

- I will do some presentation 
- In some points I will invite you  
  to write some code

## VS Tutorial

### Project creation (5 - 10 minut)
- multiplatform
- Consolidation .NET Core and .Net Framework in .NET 5
- LTS (please be vise)
https://dotnet.microsoft.com/platform/support/policy/dotnet-core

- creating .NET Core ConsoleApp C#

### Overview of a project (15 - 20 minut)

- CTrl + F5
- Solution Explorer
- Dependencies
- In real case you will have here multiple projects e.g. BLL, DAL etc
- Inbuilt packages:  
  System.IO
  System.Net
  System.Net.Http
  System.Text
  System.Threading - multi thread
  System.Threading.Tasks - asynchronous operations

Code: 
- namespaces 
- Fully objective
- Comment System and show Ctrl + .

- Our namespace
  If somebody wants to use need to add using or ...

- Add implementation for printing args  
  Tools -> CommandLine -> Developer Command line
  dotnet run -- first-arg second-arg

### Calculate polynomial using Horner's rule in point (15 minut)
https://www.math10.com/en/algebra/horner.html

- simple example to calculate value of polynomial
- require to get number of coefficients
- require to get coefficients values
- value of point for which we will calculate polynomial

- similar to the Program from lectures

> REMARKS: Verify presence in the meantime

Give them chance to do that e.g. 7 minutes, later start doing it with them.
- first using iterative method
- 
- Secondly using Horner's rule


### Factorial
- Ask students to write Factorial function using recurrence

- show 0, 1, 2, 3  
  Recurrence
- Why there is negative for 20? 
- We are multiplying only positives  
  (Overflow exception for int, max 2^31)
https://docs.microsoft.com/en-us/dotnet/api/system.int32.maxvalue?view=netcore-3.1

- there are methods to observe that
- by default OverflowException is not treated as error  
  sometimes it is acceptable e.g. when we calculate hash for object  
- enable catching exceptions like this  
  (Project properties -> Build -> Advanced -> Check for arithmetic overflow)
- show arithmetic overflow exception is caught

### Debugger show
- show windows Locals, Watch, Call stack, Exception Settings
- Locals  
  Variables in local scope, exception
- Watch  
  Our expression to watch value
- Call stack  
  Whole program execution until current breakpoint
  We can click to see value in other frames (breakpoint in factorial -> Show parameter Values)
- Exception Settings  
  By default Debugger stopped at Exception that is not caught  
  Set CLR All exceptions - showed that stopped in function
- Debug using step by step (F10)  
  Run execution here using mouse  
  Breakpoint with conditions  

### Write iterative version of Fatorial

- Simple but want some users to do something

### How to configure your package to send
- what files are generated

- sln - project information

Project directory contains
- bin - binaries with compiled code
- obj - temporary object files used to create final bin (Incremental build)
- csproj - definition of project

Build -> Clean Solution (bin should be deleted)

To send we select only:
- All *.cs files packing them naming first and second name
  please confirm that there is no `obj` and `bin` inside the folder
  
### Additionally 
- move class to calculate Polynomial to separate class
- Modify program to 