#pragma once

#include <algorithm>
#include <vector>

// Sequential binary search for one value
// It - iterator of values ordered collection
// val - value to find
template <class It, typename T>
It BinarySearch(It first, It last, const T& val)
{
    first = std::lower_bound(first, last, val);
    if ((first != last) && (val < *first))
    {
        first = last;
    }

    return first;
}

// Parallel binary search for several values (value per thread)
// It - iterator of values ordered collection
// TIt - iterator of values collection to find
// TOut - iterator of results collection,
//        each item in result collection is an `It` iterator from ordered collection where value was found 
//        (or `last` if value was not found).
// Size of results collection must be equal or bigger than `lastValue - firstValue`.
template <class It, typename TIt, typename TOut>
void ParallelBinarySearch(It first, It last, TIt firstValue, TIt lastValue, TOut result)
{
    #pragma omp parallel for
    for (int i = 0; i < static_cast<int>(std::distance(firstValue, lastValue)); ++i)
    {
        const auto& value = *std::next(firstValue, i);
        It it = BinarySearch(first, last, value);

        *std::next(result, i) = it;
    }
}
