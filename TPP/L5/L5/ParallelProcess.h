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

void DoParallelBunchProcess(const TDictionary& dictionary, const std::vector<TDictionary::value_type>& bunch, std::ofstream& resultsStream)
{
    std::vector<TDictionary::const_iterator> results(bunch.size());
    ParallelBinarySearch(std::begin(dictionary), std::end(dictionary),
                         std::begin(bunch), std::end(bunch),
                         std::begin(results));

    auto resultIt = std::begin(results);
    for (auto bunchIt = std::begin(bunch); bunchIt != std::end(bunch); ++bunchIt, ++resultIt)
    {
        auto& key = *bunchIt;
        auto& dictIt = *resultIt;

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
}


void ParallelProcess(const TDictionary& dictionary, const std::string& searchPath, const std::string& resultPath)
{
    std::ifstream searchKeysStream(searchPath);
    std::ofstream resultsStream(resultPath);

    const auto maxBunchSize = omp_get_num_procs();

    std::vector<std::string> bunch;
    bunch.reserve(maxBunchSize);

    std::string key;
    while (std::getline(searchKeysStream, key))
    {
        if (bunch.size() == maxBunchSize)
        {
            DoParallelBunchProcess(dictionary, bunch, resultsStream);
            bunch.clear();
        }

        bunch.emplace_back(std::move(key));
    }

    if (!bunch.empty())
    {
        DoParallelBunchProcess(dictionary, bunch, resultsStream);
    }
}
