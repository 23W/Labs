#pragma once

#include <algorithm>
#include <numeric>
#include <vector>

#include <Intrin.h>
#include <Windows.h>

#include "ForceToBeLink.h"
#include "CalcPerformance.h"

static const int sumPerformanceSize = 1000;
static const int sumPerformanceSamples = 10;

auto SumPerfromanceRDTSC()
{
    using timepoint = decltype(__rdtsc());
    using duration = timepoint;

    std::vector<int> data(sumPerformanceSize);
    for (auto& v : data) { v = rand(); }
    int sum = 0;

    auto performance = CalcPerformance<timepoint, duration>(
        [&]() { sum = std::accumulate(std::begin(data), std::end(data), 0); },
        []() { return __rdtsc(); }, 
        sumPerformanceSamples);

    ForceToBeLinked forceSum(&sum);

    return performance;
}


auto SumPerfromanceQueryPerformaceCounter()
{
    using timepoint = long long;
    using duration = timepoint;

    std::vector<int> data(sumPerformanceSize);
    for (auto& v : data) { v = rand(); }
    int sum = 0;

    auto performance = CalcPerformance<timepoint, duration>(
        [&]() { sum = std::accumulate(std::begin(data), std::end(data), 0); },
        []() { LARGE_INTEGER res; QueryPerformanceCounter(&res); return res.QuadPart; },
        sumPerformanceSamples);

    ForceToBeLinked forceSum(&sum);

    LARGE_INTEGER freq;
    QueryPerformanceFrequency(&freq);

    const double ns = 1000 * 1000 * 1000;
    return performance / (freq.QuadPart / ns);
}
