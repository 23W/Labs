#include <chrono>
#include <iostream>

#include "CalcPerformance.h"
#include "Dictionary.h"
#include "ParallelProcess.h"
#include "SerialProcess.h"

int main()
{
    std::cout << "PP. Lab 5" << std::endl;

    // read dictionary
    TDictionary dictionary = LoadDictionary("Keys.txt");

    // serial processing
    {
        std::cout << "Serial Processing. 1000 samples" << std::endl;

        const auto duration = CalcPerformanceChrono([&]() { SerialProcess(dictionary, "Search.txt", "SerialResults.txt"); }, 1000);

        std::cout << "\tThe best cast: " << std::chrono::duration_cast<std::chrono::microseconds>(duration) << std::endl;
    }

    // parallel processing
    {
        std::cout << "Parallel Processing. 1000 samples" << std::endl;
        std::cout << "\t" << (IsParallelEnabled() ? "Open MP is enbled." : "Open MP is not defined.") << std::endl;

        const auto duration = CalcPerformanceChrono([&]() { ParallelProcess(dictionary, "Search.txt", "ParallelResults.txt"); }, 1000);

        std::cout << "\tThe best cast: " << std::chrono::duration_cast<std::chrono::microseconds>(duration) << std::endl;
    }
}
