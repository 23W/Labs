#pragma once

#include <algorithm>
#include <vector>

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

template <class It, typename TIt>
std::vector<It> ParallelBinarySearch(It first, It last, TIt firstValue, TIt lastValue)
{
    const auto size = std::distance(firstValue, lastValue);
    std::vector<It> res(size);

    #pragma omp parallel for
    for (int i = 0; i < static_cast<int>(size); ++i)
    {
        const auto& value = *std::next(firstValue, i);
        It it = BinarySearch(first, last, value);

        res[i] = it;
    }

    return res;
}
