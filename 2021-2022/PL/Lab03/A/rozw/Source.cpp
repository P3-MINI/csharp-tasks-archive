#include <memory>
#include <string>
#include <future>
#include <chrono>
#include <thread>
#include <random>
#include <sstream>
#include <variant>
#include <iostream>
#include <algorithm>
#include <filesystem>

struct OfflineResource
{
    OfflineResource() : m_BufferData(nullptr) { }

    OfflineResource(size_t bufferSize)
    {
        m_BufferData = new char[m_BufferSize = bufferSize];
    }

    OfflineResource(OfflineResource&&) = delete;
    OfflineResource(const OfflineResource&) = delete;

    OfflineResource& operator=(const OfflineResource& other)
    {
        if (this != &other)
        {
            delete[] this->m_BufferData;

            this->m_Path = other.m_Path;
            this->m_BufferSize = other.m_BufferSize;

            this->m_BufferData = new char[this->m_BufferSize];

            std::copy(other.m_BufferData, other.m_BufferData + other.m_BufferSize, this->m_BufferData);
        }

        return *this;
    }

    OfflineResource& operator=(OfflineResource&& other)
    {
        if (this != &other)
        {
            delete[] this->m_BufferData;

            this->m_Path = other.m_Path;
            this->m_BufferSize = other.m_BufferSize;
            this->m_BufferData = other.m_BufferData;

            other.m_BufferSize = 0U;
            other.m_BufferData = nullptr;
            other.m_Path = std::string();
        }

        return *this;
    }

    ~OfflineResource()
    {
        if (this->m_BufferData != nullptr)
            delete[] this->m_BufferData;
    }

private:

    size_t m_BufferSize = 0U;
    char* m_BufferData = nullptr;

    std::filesystem::path m_Path;
};

void PrintEntities(std::filesystem::path currentPath)
{
    for (auto& directoryEntry : std::filesystem::directory_iterator(currentPath))
    {
        std::filesystem::path currentPath = directoryEntry.path();

        if (currentPath.filename().string().find("cppReference") != std::string::npos && currentPath.extension() == ".txt")
        {
            std::cout << currentPath.string() << " has type: " << static_cast<int>(std::filesystem::status(currentPath).type()) << std::endl;
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
            std::vector<std::future<void>> futures;

            for (int index = 0; index < 10; index++)
            {
                futures.push_back(std::async(std::launch::async, [this, index]()
                {
                    m_Resources.push_back(CreateEntity(index));
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

unsigned long long int operator""_Mb(unsigned long long int size)
{
    return size * (1 << 10);
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
            Additionally using std::chrono::system_clock calculate and print out the total time of all the invocations.

            - New C++ provides multiple classes and functions to assist with asynchronous implementation. The std::async function allows to run functions asynchronously.
                As the first argument it takes the enum type describing the way the function will be run (more info in documentation).
                Second argument is the function itself alongside with its arguments. Today for simplicity use parameterless lambda function.
                The std::async function returns the object std::future that provides access to results of asynchronous operations.
                Note: If std::future obtained from std::async is not stored the descructor of the std::future will block the execution causing the operations to be synchronous.
            - The std::chrono::system_clock represents the system-wide real time clock, that can be used for simple time  queries.

        Usage Of:
            - std::async (https://en.cppreference.com/w/cpp/thread/async).
            - std::launch (https://en.cppreference.com/w/cpp/thread/launch).
            - std::future (https://en.cppreference.com/w/cpp/thread/future).
            - std::chrono::system_clock (https://en.cppreference.com/w/cpp/chrono/system_clock).
    */
    {
        OnlineResourceManager onlineResourcesManager; onlineResourcesManager.GetEntries();

        std::cout << std::endl << "Resources Acquired:" << std::endl;

        for (auto& resource : onlineResourcesManager.m_Resources)
            std::cout << resource << std::endl;

        std::cout << std::endl << std::endl;
    }

    std::cout << " --------------------- ETAP_2 (1.0 Pts) ---------------------" << std::endl;

    /*
        Stage_2 (1.0 Pts):
            Implement PrintEntities function that prints out all files with a name cppReference and extension .txt.
            Display information about type of given file. The format of the output should be similar to what's in Output.txt.
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

    std::cout << " --------------------- ETAP_3 (0.5 Pts) ---------------------" << std::endl;

    /*
        Stage_3 (0.5 Pts):
            Implement appropriate operator"" with '_Mb' name, that returns '1 << 10' value multiplied by value given as an argument.

            - The C++ language introduces operator"" aka literals that allows to define different types in a handy way. The example of its usage is std::chrono_literals - like '1s' or '4h'.
                The language allows us to implement our own operator"". The arguments determine the type for which the literal will be created.
                    - For char strings the arguments are (const char*, std::size_t).
                    - For integer values the arguments are (unsigned long long int).

        Usage Of:
        - operator"" (https://en.cppreference.com/w/cpp/language/user_literal).
    */
    {
        unsigned long long int desiredSizeMb = 4_Mb;
    }

    std::cout << " --------------------- ETAP_4 (2.0 Pts) ---------------------" << std::endl;

    /*
        Stage_4 (2.0 Pts):
            Implement necessary assignment operators, so that the code below compiles.

            - The copy assignment operator is called whenever selected by overload resolution,
                i.e. when an object appears on the left side of an assignment expression.
            - The move assignment operator is called whenever it is selected by overload resolution,
                i.e. when an object appears on the left-hand side of an assignment expression,
                where the right-hand side is an rvalue of the same or implicitly convertible type.
            - The std::unique_ptr is a smart pointer that owns and manages another object. It frees the object when goes out of scope.
                The C++ language provides some external functions allowing to create std::unique_ptr.
                The std::make_unique which is a templated class provides such a functionality.
                The std::move allows moving ownership of given object to another memory - the destination of the function.

        Usage Of:
            - Move semantics (https://en.cppreference.com/w/cpp/language/move_assignment).
            - Copy assignment operator (https://en.cppreference.com/w/cpp/language/copy_assignment).
            - Move assignment operator (https://en.cppreference.com/w/cpp/language/move_assignment).
    */
    {
        OfflineResource offlineResource_1 = OfflineResource(static_cast<size_t>(2_Mb));

        OfflineResource offlineResource_2;
        OfflineResource offlineResource_3;
        OfflineResource offlineResource_4;

        offlineResource_2 = offlineResource_1;
        offlineResource_3 = std::move(offlineResource_1);
        offlineResource_4 = offlineResource_1;
    }

    return 0;
}
