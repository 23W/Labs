#pragma once

#include <chrono>

#include "CalcClockAccuracy.h"

auto ChronoAccuracy()
{
    using Timer = std::chrono::high_resolution_clock;

    auto accuracy = CalcClockAccuracy<Timer::time_point, Timer::duration>([]() { return Timer::now(); });
    return accuracy.count();
}
