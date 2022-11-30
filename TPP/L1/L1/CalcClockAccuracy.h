#pragma once

template<typename TimePoint, typename Duration, class F>
Duration CalcClockAccuracy(F f)
{
    TimePoint startPoint = f();
    TimePoint endPoint;

    do
    {
        endPoint = f();
    }
    while (startPoint == endPoint);

    return endPoint - startPoint;
}