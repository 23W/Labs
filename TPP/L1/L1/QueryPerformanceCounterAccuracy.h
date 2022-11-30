#pragma once

#pragma once

#include <Windows.h>

#include "CalcClockAccuracy.h"

auto QueryPerformanceCounterAccuracy()
{
    using timepoint = long long;
    using duration = timepoint;

    auto accuracy = CalcClockAccuracy<timepoint, duration>([]()
        {
            LARGE_INTEGER res;
            QueryPerformanceCounter(&res);

            return res.QuadPart;
        });

    LARGE_INTEGER freq;
    QueryPerformanceFrequency(&freq);

    const double ns = 1000 * 1000 * 1000;
    return accuracy / (freq.QuadPart / ns);
}
