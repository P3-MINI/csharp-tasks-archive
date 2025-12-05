#include <algorithm>
#include <deque>
#include <filesystem>
#include <fstream>
#include <future>
#include <iostream>
#include <thread>
#include <vector>

class Frame {

public:
    Frame();
    Frame(const std::vector<char>& data)
        : data(data)
    {
        std::cout << "Called Frame constructor - data vector was copied" << std::endl;
    };

    Frame(std::vector<char>&& data)
        : data(data) {};

    Frame(const Frame&) = delete;
    Frame& operator=(const Frame& obj) = delete;

    void TimeConsumingOperationOnFrame()
    {
        std::this_thread::sleep_for(std::chrono::milliseconds(20));
        for (char& c : data) {
            c = (c + 1) % 200;
        }
    };

    Frame(Frame&& other)
    {
        this->data = std::move(other.data);
    }
    void operator=(Frame&& other)
    {
        this->data = std::move(other.data);
    }

    friend std::ostream& operator<<(std::ostream& stream, const Frame& f)
    {
        unsigned int val = 0;
        for (const char& c : f.data) {
            val *= 101;
            val += c;
            val %= 1'000'000'007;
        }
        stream << std::hex << val << std::dec;
        return stream;
    }

private:
    std::vector<char> data;
};

bool DisplayNextFrame(std::deque<Frame>& frames)
{
    if (frames.empty()) {
        return false;
    }
    std::this_thread::sleep_for(std::chrono::milliseconds(rand() % 10));
    std::cout << "Displaying frame " << frames.front() << std::endl;
    frames.pop_front();

    return true;
}

void ReadFrames(std::deque<Frame>& frames)
{
    //Phase 2 here

    std::vector<std::filesystem::path> pathes;
    for (auto const& file : std::filesystem::directory_iterator("dataset")) {
        if (!file.is_regular_file()) {
            continue;
        }
        if (file.path().extension() != ".frame") {
            continue;
        }
        pathes.emplace_back(file.path());
    }

    std::sort(pathes.begin(), pathes.end());

    for (const auto& path : pathes) {
        std::ifstream fs(path);
        if (fs.is_open()) {
            std::vector<char> buff(std::istreambuf_iterator<char> { fs },
                std::istreambuf_iterator<char>());
            frames.emplace_back(std::move(buff));
        }
    }
}

int main()
{
    srand(1234);

    {
        /* Phase 0 (0.0p - it's hint how std::move works)
         */
        std::vector<char> vec { 'a' };
        //a) Uncomment line below, compile and run program.
        //Frame test(vec);
        //b) Now comment out line above and uncomment line below. Compile & run.
        //Frame test(std::move(vec));
        //c) Comment line above. I hope you now understand better how std::move works
    }

    std::deque<Frame> frames;
    Frame starting_frame({ 's', 't', 'a', 'r', 't' });

    std::cout << " --------------------- Phase 1 (1.0 Pts) ---------------------" << std::endl;
    /* Phase 1 (1.0p)
     * Implement all necessary things in Frame class to move starting_frame into frames queue
     * You can only add new code to Frame class
     */

    frames.push_back(std::move(starting_frame));

    std::cout << "First frame " << frames.front() << std::endl;
    frames.pop_front();

    std::cout << " --------------------- Phase 2 (1.5 Pts) ---------------------" << std::endl;

    /* Phase 1 (1.5p)
     * Make sure you copied directory "dataset" to the location of executable
     *
     * Implement function ReadFrames. You should:
     * 1. Read in text mode every file from directory "dataset" which has ".frame" extension
     * 2. Construct Frames
     * 3. Inserert into frames vector in alphabetical order (over file names)
     *
     * You should avoid any unneccesary copies.
     *
     * Read documentation (especially examples):
     *  - https://en.cppreference.com/w/cpp/io/basic_ifstream
     *  - https://en.cppreference.com/w/cpp/filesystem
     *  - https://en.cppreference.com/w/cpp/filesystem/directory_iterator
     *
     *  There is many useful functions in std::filesystem which can make this task very pleasant.
     *  Before writing anything complicated I recommed checking documentation
     */

    ReadFrames(frames);

    std::cout << "Read " << frames.size() << " frames" << std::endl;
    std::cout << "First frame: " << frames.front() << std::endl;
    std::cout << "Last frame: " << frames.back() << std::endl;

    std::cout << " --------------------- Phase 3 (1.0 Pts) ---------------------" << std::endl;
    /* Phase 3 (1.0p)
     * Now we want to display all frames with propter framerate - 60FPS
     * That means, we want to display each frame exactly every 16.(6)ms
     * Complete code below, use std::chrono:
     * https://en.cppreference.com/w/cpp/chrono
     * https://en.cppreference.com/w/cpp/thread/sleep_for
     * Keep in mind, that displaying frame can take some time!
     */

    auto start = std::chrono::high_resolution_clock::now();
    while (!frames.empty()) {
        auto frame_start = std::chrono::high_resolution_clock::now();

        //make line below printing time from phase 3 start in miliseconds
        const int time_from_start_ms = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::high_resolution_clock::now() - start).count();
        std::cout << time_from_start_ms << "ms from start" << std::endl;
        DisplayNextFrame(frames);
        const int time_took_ms = std::chrono::duration_cast<std::chrono::microseconds>(std::chrono::high_resolution_clock::now() - frame_start).count();
        if (time_took_ms < 16666) {
            std::this_thread::sleep_for(std::chrono::microseconds(16666 - time_took_ms));
        }
    }

    std::cout << " --------------------- Phase 4 (1.5 Pts) ---------------------" << std::endl;
    /* Phase 4 (1.5p)
     * Modern C++ provides convenient utilities to asynchronous programming - executing many task in parallel.
     * Take a look at:
     * https://en.cppreference.com/w/cpp/thread/async
     * 1. Using std::chrono measure time of calling TimeConsumingOperationOnFrame on each frame in miliseconds
     * 2. Modify code below so TimeConsumingOperationOnFrame will be called asynchronously
     */
    ReadFrames(frames);

    std::vector<std::future<void>> futures;
    futures.reserve(frames.size());

    //start measuring time here
    start = std::chrono::high_resolution_clock::now();

    for (auto& frame : frames) {
        futures.emplace_back(std::async(&Frame::TimeConsumingOperationOnFrame, &frame));
    }

    for (auto& future : futures) {
        future.wait();
    }

    //end measuring time here
    const int time_ms = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::high_resolution_clock::now() - start).count();
    std::cout << "TimeConsumingOperation on all frames took: " << time_ms << "ms" << std::endl;

    std::cout << "First frame: " << frames.front() << std::endl;
    std::cout << "Last frame: " << frames.back() << std::endl;

    return 0;
}
