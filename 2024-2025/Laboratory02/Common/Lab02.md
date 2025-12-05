# Programowanie 3 - Zaawansowane
## Laboratory 2 - Git & C# Basics

### Part #1 - GIT Theory

0. Git is a distributed version control system used to track changes in files and coordinate work on projects among multiple people. It allows developers to collaborate on code by managing changes, maintaining history, and ensuring that multiple versions of a project can be merged efficiently.

    Key features of Git include:
    * **Version Control**: Tracks changes to files over time, enabling rollback to earlier versions if necessary.
    * **Branching and Merging**: Developers can create branches to work on different features or fixes independently and merge them back into the main project when complete.
    * **Distributed**: Every user has a full copy of the repository, allowing for offline work and greater flexibility.

    There's few important git-related terms you need to know before we dive in:
    * **Git Repository** is a storage space where your project's files, along with their revision history, are stored. It tracks all changes, allowing you to manage versions, collaborate, and revert to previous states. Repositories can be local (on your computer) or remote (on platforms like GitHub).
    * **Git Commit** is a snapshot of changes in your project. It records a point in the project's history, including what has been added, modified, or deleted. Each commit includes a message describing the changes and helps track the evolution of the codebase.
    * **HEAD** refers to the current commit you are working on. It usually points to the latest commit on your current branch. You can think of it as a reference to your current position in the repository's history.
    * **.gitignore** file specifies which files or directories Git should ignore and not track. It's used to prevent unnecessary files (e.g., temporary files, logs, build outputs) from being included in commits.


1. Initialize a Git Repository
    ```
    git init
    ```  
    This command initializes a new Git repository in your current directory. It creates a .git folder that tracks changes to your files.

2. Clone a Repository
    ```
    git clone <repository> <directory>
    ```
    This command allows you to clone an existing repository. Note that you can clone a repository from a remote server (like GitHub).
    ```
    git clone https://github.com/<username>/<repo>.git
    ```
    Or you can specify a local repository as well.
    ```
    git clone myLocalRepository/ myLocalRepositoryCloned
    ```

3. Check the Status of Your Repository
    ```
    git status
    ```
    This shows the current state of your working directory and staging area. It will display any untracked, modified, or staged files.

4. Stage Changes for Commit
    ```
    git add filename.txt
    ```
    To stage a specific file for commit, you use git add. This adds changes from your working directory to the staging area. The ```git add .``` command stages all modified and new files.

5. Commit Staged Changes
    ```
    git commit -m "Add feature X"
    ```
    This commits the staged changes to the repository with a descriptive message. It saves a snapshot of your changes.

6. View Commit History
    ```
    git log
    ```
    This command shows the commit history of the repository, including commit hashes, messages, authors, and dates.

7. Push Changes to Remote Repository
    ```
    git push origin main
    ```
    This command pushes your local commits to the remote repository (e.g., GitHub). The origin refers to the remote name, and main is the branch.

8. Fetch Changes from a Remote Repository
    ```
    git fetch
    ```
    Fetches changes from the remote repository, so them you can integrate them later on.

9. Pull Changes from a Remote Repository
    ```
    git pull origin main
    ```
    This command fetches and integrates changes from the remote repository into your current branch. It helps you stay up to date with the latest changes made by others.

10. Create a New Branch
    ```
    git checkout -b <new-feature>
    ```
    This command creates and switches to a new branch called *new-feature*. Branches allow you to work on features independently from the main branch.

11. Switch Between Branches
    ```
    git checkout main
    ```
    This switches to the main branch. You can switch between any branches that exist locally.

12. Merge a Branch
    ```
    git checkout main
    git merge new-feature
    ```
    First, switch to the branch you want to merge into (e.g., main), then merge the changes from another branch (e.g., new-feature). This integrates the changes from new-feature into main.

13. Resolve Merge Conflicts
    ```
    git add conflicted-file.txt
    git commit -m "Resolve merge conflict"
    ```
    When Git detects that changes conflict between two branches, it pauses the merge and marks the conflict areas in your files. After resolving the conflicts manually, you can complete the merge with above commands.

14. Stash Changes (Temporary Save)
    ```
    git stash
    ```
    This command saves your local modifications without committing them. It's useful when you need to switch branches quickly without committing your current work.

15. Restore Stashed Changes:
    ```
    git stash pop
    ```
    Above restores the stashed changes back into your working directory.

16. Discard all local changes:
    ```
    git reset --hard
    ```
    Discards ALL changes in your local repository. You can also specify *--soft* or *--mixed* in order to revert only some changes.

17. Check the Difference Between Commits or Stages:
    ```
    git diff
    ```
    Above command shows the changes made to your files compared to the last commit.
    ```
    git diff HEAD~1 HEAD
    ```
    This shows the difference between the latest commit (HEAD) and the previous commit (HEAD~1).

18. Remove a File from Git Tracking
    ```
    git rm filename.txt
    ```
    This command removes a file from the Git repository and also deletes it from the local working directory.\
    If you want to remove the file from Git but keep it in your local directory, use:
    ```
    git rm --cached filename.txt
    ```

19. Delete a Branch
    ```
    git branch -d branch-name
    ```
    This command deletes the specified branch from your local repository.

20. Set Remote URL
    ```
    git remote add origin https://github.com/username/repo.git
    ```
    This command links your local repository to a remote one. The name origin is the default remote repository name.

21. Check Remote Repository
    ```
    git remote -v
    ```
    This shows the remote repositories associated with your local repository.

    Full documentation available here: https://git-scm.com/docs.


### Part #2 - GIT & C# Practice

* TEACHERS: Check the everyone has [MiNI C# Github](https://ghlabs.mini.pw.edu.pl/manual) account connected with USOS.

* Make sure you have main task **P3Z_24Z_Lab02** Repository visible, you're gonna download each and every task from such repository.

* Make sure you have your own solution **P3Z_24Z_{USOS_ID}_Lab02** Repository visible, you're gonna upload your solution to such repository.

* Such repositiries are gonna be created for you for each laboratory task.

* Clone task repository to the desired folder on you computer:
    ```bash
    git clone https://github.com/WUT-MiNI/P3A_24Z_Lab02.git <local_folder>
    ```
    Example output:
    ```
    Cloning into '<local_folder>'...
    remote: Enumerating objects: 3, done.
    remote: Counting objects: 100% (3/3), done.
    remote: Total 3 (delta 0), reused 0 (delta 0), pack-reused 0 (from 0)
    Receiving objects: 100% (3/3), done.
    ```

* Take a look at the code you've just downloaded and follow teacher describing the code.

* TEACHERS: Briefly explain all things as you go.

* TEACHERS: Abstract class Person with two publicly available fields: non-nullable string name, integer age. Protected constructor that initializes all fields. Public Display method that is gonna display basic information about person. \
So access modifiers, fields, constructors, methods.

* TEACHERS: Enumeration Position with following values: ChiefExecutiveOfficer, SoftwareEngineer. \
So enum, how they work in C#, what are their values.

* TEACHERS: Employee class that inherits from Person with two additional fields: integer id and enum position. Public constructor that is gonna initialize both Person and Employee class fields and PromoteEmployee method. \
So inheritance, static, base constructors.

* TEACHERS: Tell them about object and that all classes inherits from it. Indicate briefly about reference and value types, what are the differences there.

* TEACHERS: Let's talk about Display method and how we can change that to ToString, why it's better (show Console.Write example) and how that is even possible (inheriting from object).

* TEACHERS: Talk about how you can assign new Employees (to both Person and Employee thanks to inheritance) etc.

* Before you start coding you need to switch the remote for you local repository in order to push to your very own repository.

* Remember you need to be inside git repository folder.

* List your remotes to see what you're working with.
    ```bash
    git remote -v
    ```
    Example output:
    ```
    origin  https://github.com/WUT-MiNI/P3A_24Z_Lab02.git (fetch)
    origin  https://github.com/WUT-MiNI/P3A_24Z_Lab02.git (push)
    ```

* Change the remote so it points to your very own laboratory repository.
    ```bash
    git remote set-url origin https://github.com/WUT-MiNI/P3Z_24Z_{USOS_ID}_Lab02
    ```

* Check whether the remote has changed.
    ```bash
    git remote -v
    ```
    Example output:
    ```
    origin  https://github.com/WUT-MiNI/P3Z_24Z_{USOS_ID}_Lab02 (fetch)
    origin  https://github.com/WUT-MiNI/P3Z_24Z_{USOS_ID}_Lab02 (push)
    ```

* In order to submit the initial code into your repository just push the changes.
    ```bash
    git push
    ```
    Example output:
    ```
    Enumerating objects: 3, done.
    Counting objects: 100% (3/3), done.
    Writing objects: 100% (3/3), 883 bytes | 883.00 KiB/s, done.
    Total 3 (delta 0), reused 3 (delta 0), pack-reused 0
    To https://github.com/WUT-MiNI/P3Z_24Z_{USOS_ID}_Lab02
    * [new branch]      main -> main
    ```

    You can also verify that by visiting https://github.com/WUT-MiNI/P3Z_24Z_{USOS_ID}_Lab02, that code should be visible there.

* Using Visual Studio create additional file **Company.cs** and move all user-defined classes (not the Program).
    * Right Click on project in Solution Explorer window.
    * Add -> New Item -> Rename -> Add.

* You can check whether everything is working proparly by building the solution.

* Prepare to create new commit with your changes.
    ```bash
    git status
    ```
    Example output:
    ```
    On branch main
    Your branch is up to date with 'origin/main'.

    Changes not staged for commit:
    (use "git add <file>..." to update what will be committed)
    (use "git restore <file>..." to discard changes in working directory)
            modified:   Lab02/Program.cs

    Untracked files:
    (use "git add <file>..." to include in what will be committed)
            Lab02/Company.cs

    no changes added to commit (use "git add" and/or "git commit -a")
    ```

* Even though you have build your solution and Visual Studio created some intermediate and output files, they're not listed by git. That's, once again, thanks to our .gitignore file, that defines all things that git should not track / ignore changes to.

* TEACHERS: Briefly show students how that works.

* In order to add your changes to commit you need to run add command.
    ```bash
    git add .
    ```
    Remember that **.** indicates that all files are gonna be added, but you can speficy individual files or use wildcards to add all files from given folder.

* Verify that our files have been added properly.
    ```bash
    git status
    ```
    Example output:
    ```
    On branch main
    Your branch is up to date with 'origin/main'.

    Changes to be committed:
    (use "git restore --staged <file>..." to unstage)
            new file:   Lab02/Company.cs
            modified:   Lab02/Program.cs
    ```

* Before committing your changes it's adviced to review them, use diff command to do so.
    ```bash
    git diff Lab02/Program.cs
    ```
    Example output:
    ```
    diff --git a/Lab02/Program.cs b/Lab02/Program.cs
    index cd0205f..e325a76 100644
    --- a/Lab02/Program.cs
    +++ b/Lab02/Program.cs
    @@ -24,70 +24,4 @@ namespace Lab02
                employee_2.PromoteEmployee(Position.SoftwareEngineer);
            }
        }
    -
    -    internal abstract class Person
    -    {
    -        public string? name;
    -        public int age;
    -
    -        protected Person(string name, int age)
    -        {
    -            this.name = name;
    -            this.age = age;
    -        }
    -
    -        public void Display() // Version #1
    -        {
    -            Console.WriteLine($"Name: {name}, Age: {age}");
    -        }
    -
    -        public override string ToString() // Version #2
    -        {
    -            return $"Name: {name}, Age: {age}";
    -        }
    ```
    You can clearly see that red color and '-' indicate lines deleted from your code, while green color and '+' indicate lines added to you code. 

* In order to commit and push just run following commands.
    ```bash
    git commit -m "My first changes."
    git push
    ```
    Example output:
    ```
    [main 5844a79] My first changes.
    2 files changed, 75 insertions(+), 66 deletions(-)
    create mode 100644 Lab02/Company.cs
    ```
    ```
    Enumerating objects: 9, done.
    Counting objects: 100% (9/9), done.
    Delta compression using up to 12 threads
    Compressing objects: 100% (6/6), done.
    Writing objects: 100% (6/6), 3.88 KiB | 994.00 KiB/s, done.
    Total 6 (delta 1), reused 0 (delta 0), pack-reused 0
    remote: Resolving deltas: 100% (1/1), completed with 1 local object.
    To https://github.com/WUT-MiNI/P3Z_24Z_{USOS_ID}_Lab02
    4b1b56f..5844a79  main -> main
    ```
    You should be able to see your changes being reflected on github website.

* Ideally you should work on given feature on a separate branch, create one.
    ```bash
    git checkout -b new_feature
    ```
    Example output:
    ```
    Switched to a new branch 'new_feature'
    ```

* Add *HumanResources* entry as a last value in *Position* enumeration.

* Commit and push changes on 'new_feature' branch.

* Switch back to 'main' branch.
    ```bash
    git checkout main
    ```

* Add *ChiefExecutiveOfficer* entry as a last value in *Position* enumeration.

* Commit and push changes to 'main' branch.

* New, merge new_feature branch into main.
    ```bash
    git merge new_feature
    ```
    Example output:
    ```
    Auto-merging Lab02/Company.cs
    CONFLICT (content): Merge conflict in Lab02/Company.cs
    
    Automatic merge failed; fix conflicts and then commit the result.
    ```
    As you can see, since you've modified the same line on two different branches, git cannot resolve the dependencied between these two (which one comes before the other). This results in conflict.

* A git conflict occurs when two or more people (might be single person using different branches) make changes to the same part of a file, and git cannot automatically merge those changes. This usually happens during a merge or a rebase.

    When a conflict occurs, git marks the conflicting section in the file like this:
    ```
    <<<<<<< HEAD
    Your changes
    =======
    Their changes
    >>>>>>>
    ```
    To resolve the conflict, you must manually edit the file to choose, combine, or rewrite the conflicting changes, then remove the conflict markers (<<<<<<<, =======, >>>>>>>) and commit the resolved file.

* Now, when you know what conflict is and how to overcome it, let's modify our file in order to merge changes properly. Final version should look like this:
    ```CSharp
    internal enum Position
    {
        ScrumMaster,
        SoftwareEngineer,
        HumanResources,
        ChiefExecutiveOfficer,
    }
    ```

* Commit and push changes to 'main' branch.

* HOMEWORK: Explore 'Git' Tab in Visual Studio.
