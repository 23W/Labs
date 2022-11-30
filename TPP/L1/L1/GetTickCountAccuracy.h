#pragma once

#include <Windows.h>

#include "CalcClockAccuracy.h"

auto GetTickCountAccuracy()
{
    using timepoint = decltype(GetTickCount64());
    using duration = timepoint;

    auto accuracy = CalcClockAccuracy<timepoint, duration>([]() { return GetTickCount64(); });
    return accuracy;
}
