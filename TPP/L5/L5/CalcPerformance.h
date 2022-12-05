#pragma once

#include <algorithm>
#include <chrono>
#include <vector>

template<typename TimePoint, typename Duration, class Timer, class F>
Duration CalcPerformance(F f, Timer t, unsigned int attempts = 1)
{
    std::vector<Duration> samples;

    for (auto i = 0U; i < attempts; i++)
    {
        TimePoint startPoint = t();
        f();
        TimePoint endPoint = t();

        samples.emplace_back(static_cast<Duration>(endPoint - startPoint));
    }

    const auto it = std::min_element(std::begin(samples), std::end(samples));
    return *it;
}

template<class F>
std::chrono::high_resolution_clock::duration CalcPerformanceChrono(F f, unsigned int attempts = 1)
{
    using timer = std::chrono::high_resolution_clock;
    using time_point = timer::time_point;
    using duration = timer::duration;

    duration performance = CalcPerformance<time_point, duration>(f, []() { return timer::now(); });
    return performance;
}
