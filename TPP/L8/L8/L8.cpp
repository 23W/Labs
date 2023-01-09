#include <chrono>
#include <iostream>
#include <list>
#include <random>
#include <thread>
#include <vector>


struct Position
{
    size_t row;
    size_t col;
};

// number of processor stops: n*m
void Task(int* data, size_t n, size_t startRow, size_t endRow, int val, std::list<Position>& result)
{
    for (size_t row = startRow; row < endRow; row++)
    {
        for (size_t col = 0; col < n; col++)
        {
            if (data[row * n + col] == val)
            {
                Position pos;
                pos.row = row;
                pos.col = col;

                result.push_back(pos);
            }
        }
    }
}

// time complexity (T1) = O(n*m)
std::list<Position> SerialSearch(int* data, size_t n, size_t m, int val)
{
    std::list<Position> result;
    Task(data, n, 0, m, val, result);

    return result;
}

// time complexity (Tp) = O(n*m/p)
std::list<Position> ParalellSearch(int* data, size_t n, size_t m, int val)
{
    std::list<Position> result;

    size_t taskCount = std::max<>(std::thread::hardware_concurrency(), 4U);
    size_t bunch = (m / taskCount);

    std::vector<std::thread*> threads(taskCount);
    std::vector<std::list<Position>> parallelResults(taskCount);

    // run tasks
    for (size_t task = 0; task < taskCount; task++)
    {
        size_t startRow = bunch * task;
        size_t endRow = task != (taskCount - 1) ? startRow + bunch : m;

        threads[task] = new std::thread(Task, data, n, startRow, endRow, val, std::ref(parallelResults[task]));
    }

    // wait and delete all tasks
    for (size_t task = 0; task < taskCount; task++)
    {
        threads[task]->join();
        delete threads[task];
    }

    // copy all parallel results to overall result
    for (size_t task = 0; task < taskCount; task++)
    {
        for (Position& parallelResult : parallelResults[task])
        {
            result.push_back(parallelResult);
        }
    }

    return result;
}

// Output:
//
// Serail duartion result:948909us
// Parallel duartion result:174401us
// Performance:5,44096
// Serial and Parallel results are equal
// 
// Cost: 16 * 174401us = 2790416us
// Theoretical performance = 16
// Practical = 5,44
// Efficiency = 5,44 / 16 = 0,34
int main()
{
    const size_t n = 25000;
    const size_t m = 25000;
    const int minVal = 0;
    const int maxVal = 5000;
    const int searchVal = 100;

    int* data = new int[m * n];

    // Init data
    {
        std::random_device rd;
        std::uniform_int_distribution<int> distr(minVal, maxVal);

        for (size_t row = 0; row < m; row++)
        {
            for (size_t col = 0; col < n; col++)
            {
                data[row * n + col] = distr(rd);
            }
        }
    }

    std::list<Position> serialRes;
    std::list<Position> parallelRes;

    std::chrono::high_resolution_clock::duration serialDuration;
    std::chrono::high_resolution_clock::duration parallelDuration;

    // run serial
    {
        auto start = std::chrono::high_resolution_clock::now();

        serialRes = SerialSearch(data, n, m, searchVal);

        auto end = std::chrono::high_resolution_clock::now();
        serialDuration = end - start;

        std::cout << "Serail duartion result:";
        std::cout << std::chrono::duration_cast<std::chrono::microseconds>(serialDuration) << std::endl;
    }

    // run parallel
    {
        auto start = std::chrono::high_resolution_clock::now();

        parallelRes = ParalellSearch(data, n, m, searchVal);

        auto end = std::chrono::high_resolution_clock::now();
        parallelDuration = end - start;

        std::cout << "Parallel duartion result:";
        std::cout << std::chrono::duration_cast<std::chrono::microseconds>(parallelDuration) << std::endl;
    }

    // calc performace
    {
        const auto performance = static_cast<double>(std::chrono::duration_cast<std::chrono::microseconds>(serialDuration).count()) /
                                 static_cast<double>(std::chrono::duration_cast<std::chrono::microseconds>(parallelDuration).count());
        std::cout << "Performance:" << performance;
    }

    // check results equality
    {
        bool ok = true;

        if (serialRes.size() == parallelRes.size())
        {
            for (size_t i = 0; i < serialRes.size(); i++)
            {
                auto it1 = std::next(std::begin(serialRes), i);
                auto it2 = std::next(std::begin(parallelRes), i);

                if (it1->col != it2->col || 
                    it1->row != it2->row)
                {
                    ok = false;
                    break;
                }

            }
        }

        if (ok)
        {
            std::cout << "Serial and Parallel results are equal" << std::endl;
        }
        else
        {
            std::cout << "Serial and Parallel results are NOT equal" << std::endl;
        }
    }

    delete[] data;
}
