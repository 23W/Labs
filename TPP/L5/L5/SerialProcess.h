#pragma once

#include <algorithm>
#include <fstream>
#include <string>

#include "BinarySearch.h"
#include "Dictionary.h"

void SerialProcess(const TDictionary& dictionary, const std::string& searchPath, const std::string& resultPath)
{
    std::ifstream searchKeysStream(searchPath);
    std::ofstream resultsStream(resultPath);

    std::string key;
    while (std::getline(searchKeysStream, key))
    {
        const auto it = BinarySearch(std::begin(dictionary), std::end(dictionary), key);

        resultsStream << key;

        if (it != std::end(dictionary))
        {
            resultsStream << " is found at index " << std::distance(std::begin(dictionary), it) << std::endl;
        }
        else
        {
            resultsStream << " is NOT FOUND" << std::endl;
        }
    }
}
