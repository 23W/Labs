#pragma once

#include <Windows.h>

// https://devblogs.microsoft.com/oldnewthing/20150102-00/?p=43233
// This is special tric to avoid c++ optimizer discarding function or variable

struct ForceToBeLinked
{
    ForceToBeLinked(const void* p) { SetLastError(PtrToInt(p)); }
};