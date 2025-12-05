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


std::tuple<std::unique_ptr<Asset>, std::string> operator""_ASSET(const char* cString, std::size_t size)
{
    std::filesystem::path filePath = std::filesystem::path(cString);

    if (filePath.extension().string() == ".SOUND")
        return { std::make_unique<SoundAsset>(filePath.filename().string()), filePath.string() };

    if (filePath.extension().string() == ".MESH")
        return { std::make_unique<MeshAsset>(filePath.filename().string()), filePath.string() };

    return { nullptr,std::string() };
}

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
                        The std::chrono_literals utilizes overloading of operator"" (more info in next stages) to provide shortcuts for time definition like literal '1s' stands for 1 second.
                    - New C++ provides also pseudo random number generation. It consists of two main part, the generation engine and the distribution mechanism.
                        To output random value the distribution has to be feed with the engine as the argument - run this like a function - operator(). More info in the documentation.

                - (1.0 Pts) Implement the 'ShowFilesystem' function.
                - (1.0 Pts) Iterate over the current directory and for every item check its last write time.
                            Store the result in the std::vector then change current time of some of those files with +5 seconds - use random number generation to determine whether the file should change or not.
                            The file should be changed with probability of 30%. Remember to change only the files, do not change the directories.
                            Iterate over the current directory again and compare current write time with the one stored inside the vector.
                            Print appropriate message when time differes.

            Usage Of:
                - std::filesystem::* (https://en.cppreference.com/w/cpp/filesystem).
                - std::chrono_literals (https://en.cppreference.com/w/cpp/chrono/duration#Literals
                    & https://en.cppreference.com/w/cpp/thread/sleep_for).
                - std::default_random_engine (https://en.cppreference.com/w/cpp/numeric/random).
                - std::uniform_real_distribution (https://en.cppreference.com/w/cpp/numeric/random/uniform_real_distribution).

            Note: Usage of explicit paths is not allowed - only classes/methods/functions provided within std::filesystem.
            Note: While changing the timestamp of a source files, Visual Studio can ask you to reload them. Do not worry about that.
        */

        {
            /* Uncomment And Fill When ShowFilesystem Function Implemented */
            //ShowFilesystem(std::string(), /* Current Path Here */);

            std::cout << std::endl;

            std::default_random_engine randomEngine; std::uniform_real_distribution distribution;

            std::vector<std::filesystem::file_time_type> lastWriteTimes;

            /* Implement Directory Iteration Here */
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
                    - The templates allow to parametrize the functions, methods, classes by some parameter.
                        We can restrict usage of given template by applying std::enable_if clausule that enables SFINAE (https://en.cppreference.com/w/cpp/language/sfinae).
                        Allows to disable functions and methods based on a given condition.
                        The condition can be one of many presented here (https://en.cppreference.com/w/cpp/header/type_traits). In our case the std::is_base_of.
                        The std::is_base_of allows to determine whether the given Type is in the specified hierarchy.
                    - Take a look at the operator"" at the top of the file. It is a literal and allows to define different types in a handy way. The example of its usage is std::chrono_literals - like '1s' or '4h'.
                        The language allows us to implement our own operator"". The arguments determine the type for which the literal will be created.
                            - For char strings the arguments are (const char*, std::size_t).
                            - For integer values the arguments are (unsigned long long int).

                - (1.0 Pts) Change the template signature of the 'AddAsset' method inside the 'AssetManager' class.
                            It should accept any type in 'Asset' class hierarchy - Only types that are derived from 'Asset' class.
                            For each element inside 'assets' vector add them to 'assetManager' - Remember about the ownership.
                            The Asset class hierarchy is localted in AssetManager.h file.

            Usage Of:
                - templates (https://en.cppreference.com/w/cpp/language/templates).
                - std::is_base_of (https://en.cppreference.com/w/cpp/types/is_base_of).
                - std::enable_if (https://en.cppreference.com/w/cpp/types/enable_if).
                - std::move (https://en.cppreference.com/w/cpp/utility/move).
        */

        {
            std::vector<std::unique_ptr<Asset>> assets; AssetManager assetManager;

            auto[asset_1, name_1] = "Suzanne.MESH"_ASSET;
            auto[asset_2, name_2] = "Vulcan.SOUND"_ASSET;
            auto[asset_3, name_3] = "Teapot.MESH"_ASSET;
            auto[asset_4, name_4] = "Weapon.SOUND"_ASSET;

            assets.push_back(std::move(asset_1));
            assets.push_back(std::move(asset_2));
            assets.push_back(std::move(asset_3));
            assets.push_back(std::move(asset_4));

            /* Implement AddAsset Usage Here */


            /* This should not compile while using enable_if in template */
            /*
                assetManager.AddAsset(std::make_unique<int>(6));
            */

            std::cout << "No Memory Leaks - Hurra..!" << std::endl;
        }
    }

    return 0;
}
