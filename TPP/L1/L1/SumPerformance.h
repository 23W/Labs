#pragma once

#include <algorithm>
#include <numeric>
#include <vector>

#include <omp.h>

#include <Intrin.h>
#include <Windows.h>

#include "ForceToBeLink.h"
#include "CalcPerformance.h"

auto SumPerfromanceRDTSC(int dataSize = 1000, int samples = 10)
{
    using timepoint = decltype(__rdtsc());
    using duration = timepoint;

    std::vector<int> data(dataSize);
    for (auto& v : data) { v = rand(); }

    int sum = 0;
    auto performance = CalcPerformance<timepoint, duration>(
        [&]() { sum = std::accumulate(std::begin(data), std::end(data), 0); },
        []() { return __rdtsc(); }, 
        samples);

    ForceToBeLinked forceSum(&sum);

    return performance;
}

auto SumPerfromanceQueryPerformaceCounter(int dataSize = 1000, int samples = 10)
{
    using timepoint = long long;
    using duration = timepoint;

    std::vector<int> data(dataSize);
    for (auto& v : data) { v = rand(); }

    int sum = 0;
    auto performance = CalcPerformance<timepoint, duration>(
        [&]() { sum = std::accumulate(std::begin(data), std::end(data), 0); },
        []() { LARGE_INTEGER res; QueryPerformanceCounter(&res); return res.QuadPart; },
        samples);

    ForceToBeLinked forceSum(&sum);

    LARGE_INTEGER freq;
    QueryPerformanceFrequency(&freq);

    const double ns = 1000 * 1000 * 1000;
    return performance / (freq.QuadPart / ns);
}

auto SumPerfromanceOmpWTime(int dataSize = 1000, int samples = 10)
{
    using timepoint = double;
    using duration = timepoint;

    std::vector<int> data(dataSize);
    for (auto& v : data) { v = rand(); }

    int sum = 0;
    auto performance = CalcPerformance<timepoint, duration>(
        [&]() { sum = std::accumulate(std::begin(data), std::end(data), 0); },
        []() { return omp_get_wtime(); },
        samples);

    ForceToBeLinked forceSum(&sum);

    const double ns = 1000 * 1000 * 1000;
    return performance * ns;
}

auto SumRelativePerfromanceTickCount(int dataSize = 1000)
{
    using timepoint = decltype(GetTickCount64());
    using duration = timepoint;

    std::vector<int> data(dataSize);
    for (auto& v : data) { v = rand(); }

    int sum = 0;
    auto relativePerformance = CalcRelativePerformance<timepoint, duration>(
        [&]() { sum = std::accumulate(std::begin(data), std::end(data), 0); },
        []() { return GetTickCount64(); },
        duration(2*1000));

    ForceToBeLinked forceSum(&sum);

    return relativePerformance;
}
