#pragma once

#include <Intrin.h>

#include "CalcClockAccuracy.h"

auto RDTSCAccuracy()
{
    using timepoint = decltype(__rdtsc());
    using duration = timepoint;

    auto accuracy = CalcClockAccuracy<timepoint, duration>([]() { return __rdtsc(); });
    return accuracy;
}
