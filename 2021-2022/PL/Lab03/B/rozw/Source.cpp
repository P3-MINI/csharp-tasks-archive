#include <memory>
#include <string>
#include <future>
#include <chrono>
#include <thread>
#include <random>
#include <sstream>
#include <variant>
#include <iostream>
#include <optional>
#include <algorithm>
#include <filesystem>

struct OfflineResource
{
    OfflineResource(const std::string& bufferData)
    {
        this->m_BufferData = new char[this->m_BufferSize = bufferData.size()];
    }

    OfflineResource& operator=(OfflineResource&) = delete;
    OfflineResource& operator=(OfflineResource&&) = delete;

    OfflineResource(const OfflineResource& other)
    {
        this->m_Path = other.m_Path;
        this->m_BufferSize = other.m_BufferSize;

        this->m_BufferData = new char[this->m_BufferSize];

        std::copy(other.m_BufferData, other.m_BufferData + other.m_BufferSize, this->m_BufferData);
    }

    OfflineResource(OfflineResource&& other)
    {
        this->m_Path = other.m_Path;
        this->m_BufferSize = other.m_BufferSize;
        this->m_BufferData = this->m_BufferData;

        this->m_BufferSize = 0U;
        this->m_BufferData = nullptr;
        this->m_Path = std::string();
    }

    ~OfflineResource()
    {
        if (this->m_BufferData != nullptr)
            delete[] this->m_BufferData;
    }

    std::filesystem::path m_Path;

private:

    size_t m_BufferSize = 0U;
    char* m_BufferData = nullptr;
};

void PrintEntities(std::filesystem::path currentPath)
{
    for (auto& directoryEntry : std::filesystem::directory_iterator(currentPath))
    {
        std::filesystem::path currentPath = directoryEntry.path();

        if (currentPath.filename().string().find("cppReference") != std::string::npos && currentPath.extension() == ".txt")
        {
            auto permissions = std::filesystem::status(currentPath).permissions();

            std::string hasNot = (std::filesystem::perms::others_write & permissions) != std::filesystem::perms::none ? "" : "not";

            std::cout << currentPath.string() << " has " << hasNot << "others_write permission." << std::endl;
        }
    }
}

struct OnlineResourceManager
{
    void GetEntries()
    {
        std::chrono::system_clock systemClock; 
        std::chrono::system_clock::time_point timeStart = systemClock.now();

        {
            std::mutex mutex; std::vector<std::future<void>> futures;

            for (int i = 0; i < 10; i++)
            {
                futures.push_back(std::async(std::launch::async, [this, i, &mutex]()
                {
                    {
                        std::lock_guard<std::mutex> lockGuard(mutex);

                        std::cout << "Thread No: " << std::this_thread::get_id() << std::endl;
                    }

                    m_Resources.push_back(CreateEntity(i));
                }));
            }
        }

        std::chrono::system_clock::time_point timeEnd = systemClock.now();
        std::chrono::system_clock::duration duration = timeEnd - timeStart;

        std::cout << "CURL Elapsed: " << duration.count() / 10000.0 << std::endl;
    }

    std::vector<std::string> m_Resources;

private:

    std::string CreateEntity(int index)
    {
        std::ostringstream filenameString; filenameString << "cppReference_" << index << ".txt";

        std::ostringstream commandString; commandString << "curl -s https://en.cppreference.com/w/ -o " << filenameString.str();

        system(commandString.str().c_str()); return filenameString.str();
    }
};

std::string operator""_Resource(const char* cString, std::size_t size)
{
    return std::string(cString);
}

int main(int argc, char** argv)
{
    /*
        Before you start make sure to enable C++17 settings by going to Properties -> C/C++ -> Language -> C++ Language Standard -> Choose the C++ Version.
    */

    std::cout << " --------------------- ETAP_1 (1.5 Pts) ---------------------" << std::endl;

    /*
        Stage_1 (1.5 Pts):
            Modify GetEntries method so that the invocation of private CreateEntity method is asynchronous.
            Additionally before invocation of CreateEntity print out the message about the ID of current thread.
            Use synchronization mechanism between threads, to ensure adequate format of the output. 
            
            - New C++ provides multiple classes and functions to assist with asynchronous implementation. The std::async function allows to run functions asynchronously.
                As the first argument it takes the enum type describing the way the function will be run (more info in documentation).
                Second argument is the function itself alongside with its arguments. Today for simplicity use parameterless lambda function.
                The std::async function returns the object std::future that provides access to results of asynchronous operations.
                Note: If std::future obtained from std::async is not stored the descructor of the std::future will block the execution causing the operations to be synchronous.
            - While working with asynchronous methods some form of synchronizations is required.
                The std::mutex is a basic synchronizations primitive that assists while accessing shared data from multiple threads.
                The std::lock_guard is a mutex wrapper that provides RAII mechanism for owning the mutexes. It locks the mutex on initialization and frees it upon destruction.
            - The std::this_thread namespace gives the access to thread functions, like making thread to sleep or get some datails, like its ID value.

        Usage Of:
            - std::async (https://en.cppreference.com/w/cpp/thread/async).
            - std::launch (https://en.cppreference.com/w/cpp/thread/launch).
            - std::future (https://en.cppreference.com/w/cpp/thread/future).
            - std::mutex (https://en.cppreference.com/w/cpp/thread/mutex).
            - std::this_thread (https://en.cppreference.com/w/cpp/thread/get_id).
            - std::lock_guard (https://en.cppreference.com/w/cpp/thread/lock_guard).
    */
    {
        OnlineResourceManager onlineResourcesManager; onlineResourcesManager.GetEntries();

        std::cout << std::endl << "Resources Aqquired:" << std::endl;

        for (auto& resource : onlineResourcesManager.m_Resources)
            std::cout << resource << std::endl;

        std::cout << std::endl << std::endl;
    }

    std::cout << " --------------------- STAGE_2 (1.0 Pts) ---------------------" << std::endl;

    /*
        Stage_2 (1.0 Pts):
            Implement PrintEntities function that prints out all files with a name cppReference and extension .txt.
            Display information about permission to given file. The format of the output should be similar to what's in output.txt.
            Use current working path as the argument when invoking the function in main.

            - The std::filesystem is a C++17 feature that allows to perform operations on file system and its components such as paths, files, and directories.
                It allows to set and get some of their properties like permissions, write time or types. Can also create files, directories and links.

        Usage Of:
            - std::filesystem::* (https://en.cppreference.com/w/cpp/filesystem).

        Note: Usage of explicit paths is not allowed - only classes/methods/functions provided within std::filesystem.
    */
    {
        PrintEntities(std::filesystem::current_path());
    }

    std::cout << " --------------------- STAGE_3 (0.5 Pts) ---------------------" << std::endl;

    /*
        Stage_3 (0.5 Pts):
            Implement appropriate operator"" with '_Resource' name, that returns std::string created from const char* value given as an argument.

            - The C++ language introduces operator"" aka literals that allows to define different types in a handy way. The example of its usage is std::chrono_literals - like '1s' or '4h'.
                The language allows us to implement our own operator"". The arguments determine the type for which the literal will be created.
                    - For char strings the arguments are (const char*, std::size_t).
                    - For integer values the arguments are (unsigned long long int).

        Usage Of:
        - operator"" (https://en.cppreference.com/w/cpp/language/user_literal).
    */
    {
        OfflineResource offlineResource_1 = "myOffline"_Resource;
    }

    std::cout << " --------------------- STAGE_4 (2.0 Pts) ---------------------" << std::endl;

    /*
        Stage_4 (2.0 Pts):
            Implement necessary constructors, so that the code below compiles.

            - The copy constructor is called whenever an object is initialized from another object of the same type.
            - The move constructor is typically called when an object is initialized from rvalue of the same type,
                which means that it "steal" the resources held by the argument.
            - The std::unique_ptr is a smart pointer that owns and manages another object. It frees the object when goes out of scope.
                The C++ language provides some external functions allowing to create std::unique_ptr.
                The std::make_unique which is a templated class provides such a functionality.
                The std::move allows moving ownership of given object to another memory - the destination of the function.

        Usage Of:
            - Move semantics (https://en.cppreference.com/w/cpp/language/move_assignment).
            - Copy constructor (https://en.cppreference.com/w/cpp/language/copy_constructor).
            - Move constructor (https://en.cppreference.com/w/cpp/language/move_constructor).
    */
    {
        OfflineResource offlineResource_1 = "myOffline"_Resource;

        OfflineResource offlineResource_2 = offlineResource_1;
        OfflineResource offlineResource_3 = std::move(offlineResource_1);
        OfflineResource offlineResource_4 = offlineResource_1;
    }

    return 0;
}
