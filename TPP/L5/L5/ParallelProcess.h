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

void OutputBunchProcess(const TDictionary& dictionary, const std::vector<TDictionary::value_type>& bunch, std::vector<TDictionary::const_iterator>& results, std::ofstream& resultsStream)
{
    auto resultIt = std::begin(results);
    for (auto bunchIt = std::begin(bunch), bunchEndIt = std::end(bunch); bunchIt != bunchEndIt; ++bunchIt, ++resultIt)
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
    const auto maxBunchSize = 4000 * omp_get_num_procs();

    std::vector<std::string> bunch;
    bunch.reserve(maxBunchSize);

    std::vector<TDictionary::const_iterator> bunchResults(maxBunchSize);

    std::ifstream searchKeysStream(searchPath);
    std::ofstream resultsStream(resultPath);

    std::string key;
    while (std::getline(searchKeysStream, key))
    {
        if (key.empty())
        {
            continue;
        }

        if (bunch.size() == maxBunchSize)
        {
            ParallelBinarySearch(std::begin(dictionary), std::end(dictionary),
                                 std::begin(bunch), std::end(bunch),
                                 std::begin(bunchResults));

            OutputBunchProcess(dictionary, bunch, bunchResults, resultsStream);

            bunch.clear();
        }

        bunch.emplace_back(std::move(key));
    }

    if (!bunch.empty())
    {
        ParallelBinarySearch(std::begin(dictionary), std::end(dictionary),
                             std::begin(bunch), std::end(bunch),
                             std::begin(bunchResults));

        OutputBunchProcess(dictionary, bunch, bunchResults, resultsStream);
    }
}
