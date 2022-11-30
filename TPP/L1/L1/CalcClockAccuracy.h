#pragma once

template<typename TimePoint, typename Duration, class F>
Duration CalcClockAccuracy(F f)
{
    TimePoint startPoint = f();
    TimePoint endPoint = startPoint;

    while (startPoint == endPoint)
    {
        endPoint = f();
    }

    return endPoint - startPoint;
}