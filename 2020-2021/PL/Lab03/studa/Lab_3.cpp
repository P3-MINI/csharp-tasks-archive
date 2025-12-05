#include <tuple>
#include <memory>
#include <string>
#include <future>
#include <chrono>
#include <thread>
#include <random>
#include <iostream>
#include <algorithm>
#include <filesystem>

#include "AssetManager.h"

using namespace std::chrono_literals; /* Enables the Chrono Literals */


/* Implement operator"" Here */


/*
    Function that recursivelly travers the given filesystem path and prints their content. Uses offsetPrefix to offset catalog depth layers.

    @param offsetPrefix - Describes the '\t' offset that should be applied to current line.
    @param currentPath  - The filesystem path that should be enumerated and check for other directories.
*/
void ShowFilesystem(std::string offsetPrefix, std::filesystem::path currentPath)
{

}

int main(int argc, char** argv)
{
    /*
        Before you start make sure to enable C++17 settings by going to Properties -> C/C++ -> Language -> C++ Language Standard -> Choose the C++ Version.
    */

    std::cout << " --------------------- ETAP_1 (2.0 Pts) ---------------------" << std::endl;
    {
        /*
            Stage_1 (2.0 Pts):
                    - The std::filesystem is a C++17 feature that allows to perform operations on file system and its components such as paths, files, and directories.
                        It allows to set and get some of their properties like permissions, write time or types. Can also create files, directories and links.
                    - The std::chrono defines types and utility functions for time manipulation like clocks, time points and durations.
                        The std::chrono_literals utilizes overloading of operator"" (more info in next stages) to provide shortcuts for time definition like literal '1s' which stands for 1 second.

                - (1.0 Pts) Implement the 'ShowFilesystem' function.
                - (1.0 Pts) Iterate over the current directory and for every item check its permissions.
                            Print appropriate message when Other_Write permission has been granted.

            Usage Of:
                - std::filesystem::* (https://en.cppreference.com/w/cpp/filesystem).
                - std::chrono_literals (https://en.cppreference.com/w/cpp/chrono/duration#Literals).

            Note: Usage of explicit paths is not allowed - only classes/methods/functions provided within std::filesystem.
        */

        {
            /* Uncomment And Fill When ShowFilesystem Function Implemented */
            //ShowFilesystem(std::string(), /* Current Path Here */);

            std::cout << std::endl;

            /* Implement Directory Iteration Here */

            std::cout << std::endl;
        }
    }

    std::cout << " --------------------- ETAP_2 (2.0 Pts) ---------------------" << std::endl;
    {
        using namespace std::chrono_literals;

        /*
            Stage_2 (2.0 Pts):
                    - New C++ provides multiple classes and functions to assist with asynchronous implementation. The std::async function allows to run functions asynchronously.
                        As the first argument it takes the enum type describing the way the function will be run (more info in documentation).
                        Second argument is the function itself alongside with its arguments. Today for simplicity use parameterless lambda function.
                        The std::async function returns the object std::future that provides access to results of asynchronous operations.
                        Note: If std::future obtained from std::async is not stored the descructor of the std::future will block the execution causing the operations to be synchronous.
                    - While working with asynchronous methods some form of synchronizations is required.
                        The std::mutex is a basic synchronizations primitive that assists while accessing shared data from multiple threads.
                        The std::lock_guard is a mutex wrapper that provides RAII mechanism for owning the mutexes. It locks the mutex on initialization and frees it upon destruction.
                    - The std::chrono defines types and utility functions for time manipulation like clocks, time points and durations.
                        The std::chrono_literals utilizes overloading of operator"" (more info in next stages) to provide shortcuts for time definition like literal '1s' stands for 1 second.
                    - The std::this_thread namespace gives the access to current thread function, like making thread to sleep or datails, like its ID value.

                - (1.0 Pts) Implement asynchronous behaviour that imitates to work for some time.
                            For the 'threadsCount' elements call the asynchronous lambda function.
                            Inside the lambda print appropriate messages about start and end events.
                            Between those messages for 'amountLoops' times wait for one second and display given 'displayValue' - To visualize the asynchronous behaviour.
                            Use capture mechanizm to provide necessary data for your lambda function.
                - (1.0 Pts) Use synchronization mechanizm to push thread IDs into the sts::vector.
                            Print all the thread IDs when all the threads are finished.

            Usage Of:
                - std::async (https://en.cppreference.com/w/cpp/thread/async).
                - std::launch (https://en.cppreference.com/w/cpp/thread/launch).
                - std::future (https://en.cppreference.com/w/cpp/thread/future).
                - std::this_thread (https://en.cppreference.com/w/cpp/thread/sleep_for
                    & https://en.cppreference.com/w/cpp/thread/get_id).
                - std::lock_guard (https://en.cppreference.com/w/cpp/thread/lock_guard).
                - std::mutex (https://en.cppreference.com/w/cpp/thread/mutex).
                - std::chrono_literals (https://en.cppreference.com/w/cpp/chrono/duration#Literals
                    & https://en.cppreference.com/w/cpp/thread/sleep_for).
        */

        std::vector<std::thread::id> threadIDs; std::mutex vectorMutex;

        {
            std::default_random_engine randomEngine; std::uniform_int_distribution distribution(3, 10);

            std::vector<std::future<void>> futures; int threadsCount = 4;

            for (int i = 0; i < threadsCount; i++)
            {
                char displayValue = 'A' + i; int amountLoops = distribution(randomEngine);

                /* Implement Async Run Here */
            }
        }

        {
            std::cout << std::endl << "Threads Processed: ";

            /* Implement Vector print Here */

            std::cout << std::endl;
        }
    }

    std::cout << " --------------------- ETAP_3 (1.0 Pts) ---------------------" << std::endl;
    {
        /*
            Stage_3 (1.0 Pts):
                    - The std::filesystem is a C++17 feature that allows to perform operations on file system and its components such as paths, files, and directories.
                        It allows to set and get some of their properties like permissions, write time or types. Can also create files, directories and links.
                    - The std::unique_ptr is a smart pointer that owns and manages another object. It frees the object when goes out of scope.
                        The C++ language provides some external functions allowing to create std::unique_ptr.
                        The std::make_unique which is a templated class provides such a functionality.
                        The std::move allows moving ownership of given object to another memory - the destination of the function.
                    - The C++ language introduces operator"" aka literals that allows to define different types in a handy way. The example of its usage is std::chrono_literals - like '1s' or '4h'.
                        The language allows us to implement our own operator"". The arguments determine the type for which the literal will be created.
                            - For char strings the arguments are (const char*, std::size_t).
                            - For integer values the arguments are (unsigned long long int).
                    - Structured Binding allows to bind objects returned by some function with given names.
                         It allows to bind the return type like tuples to local variables without need to use special legacy 'get<>' functions.

                - (1.0 Pts) Implement custom string literal aka operator"", named '_ASSET' that returns the 'std::tuple<std::unique_ptr<Asset>, std::string>' and takes two arguments - 'const char* cString' and 'std::size_t size'.
                                <returnType> operator""<literalName>(<arguments>)
                            Use Structured Binding to assign operator's results to variables.
                            Push the asset elements into vector and print them afterwards using STL algorithms. Remember about the ownership.
                            The Asset class hierarchy is localted in AssetManager.h file.

            Usage:
                - std::filesystem::* (https://en.cppreference.com/w/cpp/filesystem).
                - std::unique_ptr (https://en.cppreference.com/w/cpp/memory/unique_ptr).
                - std::move (https://en.cppreference.com/w/cpp/utility/move).
                - std::forward (https://en.cppreference.com/w/cpp/utility/forward).
                - std::make_unique (https://en.cppreference.com/w/cpp/memory/unique_ptr/make_unique).
                - operator"" (https://en.cppreference.com/w/cpp/language/user_literal).
                - structuredBindings (https://en.cppreference.com/w/cpp/language/structured_binding).
        */

        {
            std::vector<std::unique_ptr<Asset>> assets;

            /* Implement Operator Usage Here */

            // /* structuredBindings */ = "Suzanne.MESH"_ASSET;
            // /* structuredBindings */ = "Vulcan.SOUND"_ASSET;
            // /* structuredBindings */ = "Teapot.MESH"_ASSET;
            // /* structuredBindings */ = "Weapon.SOUND"_ASSET;
        }
    }

    return 0;
}
