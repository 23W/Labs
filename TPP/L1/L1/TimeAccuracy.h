#pragma once

#include <time.h>

#include "CalcClockAccuracy.h"

auto TimeAccuracy()
{
    using timepoint = decltype(time(nullptr));
    using duration = timepoint;

    auto accuracy = CalcClockAccuracy<timepoint, duration>([]() { return time(nullptr); });
    return accuracy;
}

auto ClockAccuracy()
{
    using timepoint = decltype(clock());
    using duration = timepoint;

    auto accuracy = CalcClockAccuracy<timepoint, duration>([]() { return clock(); });
    return accuracy;
}
