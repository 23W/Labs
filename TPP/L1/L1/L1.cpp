#include <iostream>

#include "ChronoAccuracy.h"
#include "ForceToBeLink.h"
#include "GetSystemTimeAsFileTimeAccuracy.h"
#include "GetTickCountAccuracy.h"
#include "QueryPerformanceCounterAccuracy.h"
#include "Matrix.h"
#include "OmpWTimeAccuracy.h"
#include "RDTSCAccuracy.h"
#include "SumPerformance.h"
#include "TimeAccuracy.h"


int main()
{
    std::cout << "PP. Lab1" << std::endl;

    // Task 1
    {

    }

    // Task 2
    {
        std::cout << "Task 2. Clock functions accuracy" << std::endl;

        const auto time_res = TimeAccuracy();
        std::cout << "\ttime: " << time_res << " in seconds" << std::endl;

        const auto clock_res = ClockAccuracy();
        std::cout << "\tclock: " << clock_res << " in CLOCKS_PER_SEC (" << CLOCKS_PER_SEC << ")" << std::endl;

        const auto getSystemTimeAsFileTime_res = GetSystemTimeAsFileTimeAccuracy();
        std::cout << "\tGetSystemTimeAsFileTime: " << getSystemTimeAsFileTime_res << " in 100-nanosecond" << std::endl;

        const auto getSystemTimePreciseAsFileTime_res = GetSystemTimePreciseAsFileTimeAccuracy();
        std::cout << "\tGetSystemTimePreciseAsFileTime: " << getSystemTimePreciseAsFileTime_res << " in 100-nanosecond (should be <1000 nanosecond)" << std::endl;

        const auto getTickCount_res = GetTickCountAccuracy();
        std::cout << "\tGetTickCount: " << getTickCount_res << " milliseconds (should be in the range [10...16] milliseconds)" << std::endl;

        const auto rdtsc_res = RDTSCAccuracy();
        std::cout << "\t__rdtsc: " << rdtsc_res << " ticks" << std::endl;

        const auto queryPerformanceCounter_res = QueryPerformanceCounterAccuracy();
        std::cout << "\tQueryPerformanceCounter: " << queryPerformanceCounter_res << " nanoseconds (should be <1000 nanosecond)" << std::endl;

        const auto chrono_res = ChronoAccuracy();
        std::cout << "\tchrono::high_resolution_clock: " << chrono_res << " nanoseconds (should be 100 nanoseconds)" << std::endl;

        const auto omp_res = OmpWTimeAccuracy();
        std::cout << "\tomp_get_wtime: " << omp_res << " nanoseconds" << std::endl;
    }

    // Task 3
    {
        std::cout << "Task 3. Performance of Sum 1000" << std::endl;

        const auto sumPerformance_rdtsc = SumPerfromanceRDTSC();
        std::cout << "\tSum of 1000 by __rdtsc: " << sumPerformance_rdtsc << " ticks" << std::endl;

        const auto sumPerformance_qpc = SumPerfromanceQueryPerformaceCounter();
        std::cout << "\tSum of 1000 by QueryPerformanceCounter: ";
        (sumPerformance_qpc > 0) ? std::cout << sumPerformance_qpc : std::cout << "< 100";
        std::cout << " nanoseconds" << std::endl;
    }

    // Task 4
    {
        std::cout << "Task 4. Absolute & Relative Performance" << std::endl;

        std::cout << "\tPer data size" << std::endl;
        const int dataSets[] = { 100000, 2* 100000, 3* 100000 };
        double performance[] = { 0, 0, 0 };
        size_t relativePerformance[] = { 0, 0, 0 };

        for (size_t index = 0; index < std::size(dataSets); index++)
        {
            performance[index] = std::round(SumPerfromanceOmpWTime(dataSets[index]));
            relativePerformance[index] = SumRelativePerfromanceTickCount(dataSets[index]);
        }

        for (size_t index = 0; index < std::size(dataSets); index++)
        {
            std::cout << "\t\t" << dataSets[index];
            
            std::cout << " absolute: " << performance[index] << " nanoseconds";
            if (index > 0)
            {
                std::cout << " (Tx = " << performance[index]/ performance[0] << ")";
            }

            std::cout << ", relative: " << relativePerformance[index] << " times per 2 seconds";
            if (index > 0)
            {
                std::cout << " (Tx = " << ((double)relativePerformance[0]) / relativePerformance[index] << ")";
            }

            std::cout << std::endl;
        }
    }

    // Task 5
    {
        std::cout << "Task 5. Matrix functional vs object" << std::endl;

        std::cout << "\tFuntional A x B = C" << std::endl;
        {
            float a[3][3] = { {1,2,3}, {4,5,6}, {7,8,9} };
            float b[3][3] = { {9,8,7}, {6,5,4}, {3,2,1} };
            float c[3][3];

            const auto perf = CalcPerformanceChrono([&]() { MatrixMult((float*)a, (float*)b, (float*)c, 3); });
            MatrixOutput((float*)c, 3, "\t\t");

            std::cout << "\tPerformance: " << perf.count() << " nanoseconds" << std::endl;
        }

        std::cout << "\tObject A x B = C" << std::endl;
        {
            Matrix<float> a{ 1.f,2.f,3.f, 4.f,5.f,6.f, 7.f,8.f,9.f };
            Matrix<float> b{ 9.f,8.f,7.f, 6.f,5.f,4.f, 3.f,2.f,1.f };
            Matrix<float> c;

            const auto perf = CalcPerformanceChrono([&]() { c.Mult(a, b); });
            c.Output("\t\t");

            std::cout << "\tPerformance: " << perf.count() << " nanoseconds" << std::endl;
        }
    }

    // Task 7
    {
        std::cout << "Task 7. Matrix mutiplication performance per size" << std::endl;

        const int matrixSize[] = { 512, 1024, 2048 };
        size_t functional_performance[] = { 0, 0, 0 };
        size_t object_performance[] = { 0, 0, 0 };

        for (size_t index = 0; index < std::size(matrixSize); index++)
        {
            auto size = matrixSize[index];

            {
                std::vector<float> a(size* size, 0.f);
                std::vector<float> b(size* size, 0.f);
                std::vector<float> c(size* size);

                const auto perf = CalcPerformanceChrono([&]() { MatrixMult(a.data(), b.data(), c.data(), size); });
                functional_performance[index] = std::chrono::duration_cast<std::chrono::milliseconds>(perf).count();

                ForceToBeLinked(c.data());
            }

            {
                Matrix<float> a(size);
                Matrix<float> b(size);
                Matrix<float> c;

                const auto perf = CalcPerformanceChrono([&]() { c.Mult(a, b); });
                object_performance[index] = std::chrono::duration_cast<std::chrono::milliseconds>(perf).count();;

                ForceToBeLinked(c.GetData());
            }
        }

        for (size_t index = 0; index < std::size(matrixSize); index++)
        {
            auto size = matrixSize[index];
            std::cout << "\t" << size << std::endl;
            std::cout << "\t\tFunctional: " << functional_performance[index] << " milliseconds" << std::endl;;
            std::cout << "\t\tObject: " << object_performance[index] << " milliseconds" << std::endl;;
        }
    }
}
