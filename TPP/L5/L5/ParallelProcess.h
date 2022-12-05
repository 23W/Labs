#pragma once

#include <algorithm>
#include <fstream>
#include <string>

#include <omp.h>

#include "BinarySearch.h"
#include "Dictionary.h"


bool IsParallelEnabled()
{
#ifdef _OPENMP
    return true;
#else
    return false;
#endif // _OPENMP
}

void ParallelProcess(const TDictionary& dictionary, const std::string& searchPath, const std::string& resultPath)
{
    std::ifstream searchKeysStream(searchPath);
    std::ofstream resultsStream(resultPath);

    const auto maxBunchSize = omp_get_num_procs();

    std::vector<std::string> bunch;
    bunch.reserve(maxBunchSize);

    const auto doParallelBunchSearch = [&]()
    {
        const auto searchResult = ParallelBinarySearch(std::begin(dictionary), std::end(dictionary),
                                                       std::begin(bunch), std::end(bunch));

        auto keyIt = std::begin(bunch);
        auto resIt = std::begin(searchResult);
        for (; keyIt != std::end(bunch); ++keyIt, ++resIt)
        {
            auto key = *keyIt;
            auto dictIt = *resIt;

            resultsStream << key;

            if (dictIt != std::end(dictionary))
            {
                resultsStream << " is found at index " << std::distance(std::begin(dictionary), dictIt) << std::endl;
            }
            else
            {
                resultsStream << " is NOT FOUND" << std::endl;
            }
        }
    };

    std::string key;
    while (std::getline(searchKeysStream, key))
    {
        if (bunch.size() < maxBunchSize)
        {
            bunch.emplace_back(std::move(key));
        }
        else
        {
            doParallelBunchSearch();
        }
    }

    if (!bunch.empty())
    {
        doParallelBunchSearch();
    }
}
