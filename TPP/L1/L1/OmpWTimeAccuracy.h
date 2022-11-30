#pragma once

#include <omp.h>

#include "CalcClockAccuracy.h"

auto OmpWTimeAccuracy()
{
    using timepoint = decltype(omp_get_wtime());
    using duration = timepoint;

    auto accuracy = CalcClockAccuracy<timepoint, duration>([]() { return omp_get_wtime(); });

    const double ns = 1000 * 1000 * 1000;
    return accuracy * ns;
}
