#include <chrono>
#include <format>
#include <iostream>
#include <random>

#include "CalcPerformance.h"
#include "Dictionary.h"
#include "ParallelProcess.h"
#include "SerialProcess.h"

void GenerateKeys()
{
    const auto keySize = 2 * 1024 * 1024;
    const auto searchSize = 20 * keySize;

    std::ofstream keysStream("TestCase1\\Keys.txt");
    std::ofstream searchStream("TestCase1\\Search.txt");

    // generate dictionary
    TDictionary keys;
    keys.reserve(keySize);
    for (size_t i = 0; i < keySize; i++)
    {
        keys.emplace_back(std::format("{:015d}", i));
    }
    std::sort(std::begin(keys), std::end(keys));

    // save it
    keysStream << keySize << std::endl;
    for (auto& key : keys)
    {
        keysStream << key << std::endl;
    }

    // generate collection of keys to find
    std::uniform_int_distribution<int> distribute(0, keySize * 2);
    std::default_random_engine e;
    for (size_t i = 0; i < searchSize; i++)
    {
        searchStream << std::format("{:015d}", distribute(e)) << std::endl;
    }
}

int main()
{
    const size_t samplesCount = 2;

    std::cout << "PP. Lab 5" << std::endl;

    // generate huge data
    //std::cout << "Generate data." << std::endl;
    //GenerateKeys();

    // read dictionary
    std::cout << "Load keys dictionary" << std::endl;
    TDictionary dictionary = LoadDictionary("TestCase1\\Keys.txt");

    // serial processing
    {
        std::cout << "Serial Processing. " << samplesCount << " samples" << std::endl;

        const auto duration = CalcPerformanceChrono([&]() { SerialProcess(dictionary, "TestCase1\\Search.txt", "TestCase1\\SerialResults.txt"); }, samplesCount);

        std::cout << "\tThe best case: " << std::chrono::duration_cast<std::chrono::seconds>(duration) << std::endl;
    }

    // parallel processing
    {
        std::cout << "Parallel Processing. " << samplesCount << " samples" << std::endl;
        std::cout << "\t" << (IsParallelEnabled() ? "Open MP is enbled." : "Open MP is not defined.") << std::endl;

        const auto duration = CalcPerformanceChrono([&]() { ParallelProcess(dictionary, "TestCase1\\Search.txt", "TestCase1\\ParallelResults.txt"); }, samplesCount);

        std::cout << "\tThe best case: " << std::chrono::duration_cast<std::chrono::seconds>(duration) << std::endl;
    }
}
