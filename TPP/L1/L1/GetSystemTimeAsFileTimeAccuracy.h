#pragma once

#include <Windows.h>

#include "CalcClockAccuracy.h"

auto GetSystemTimeAsFileTimeAccuracy()
{
    using timepoint = unsigned long long;
    using duration = timepoint;

    auto accuracy = CalcClockAccuracy<timepoint, duration>([]()
        {
            FILETIME ft;
            GetSystemTimeAsFileTime(&ft);

            ULARGE_INTEGER res;
            res.LowPart = ft.dwLowDateTime;
            res.HighPart = ft.dwHighDateTime;

            return res.QuadPart;
        });

    return accuracy;
}


auto GetSystemTimePreciseAsFileTimeAccuracy()
{
    using timepoint = unsigned long long;
    using duration = timepoint;

    auto accuracy = CalcClockAccuracy<timepoint, duration>([]()
        {
            FILETIME ft;
            GetSystemTimePreciseAsFileTime(&ft);

            ULARGE_INTEGER res;
            res.LowPart = ft.dwLowDateTime;
            res.HighPart = ft.dwHighDateTime;

            return res.QuadPart;
        });

    return accuracy;
}