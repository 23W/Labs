#pragma once

#include <Windows.h>

#include "CalcClockAccuracy.h"

#if defined(_AMD64_)
    #define GetTickCountFuntcion GetTickCount64
#elif defined(_X86_)
    #define GetTickCountFuntcion GetTickCount
#else
    static_assert(false, "Unsupported Architecture type");
#endif


auto GetTickCountAccuracy()
{
    using timepoint = decltype(GetTickCountFuntcion());
    using duration = timepoint;

    auto accuracy = CalcClockAccuracy<timepoint, duration>([]() { return GetTickCountFuntcion(); });
    return accuracy;
}
