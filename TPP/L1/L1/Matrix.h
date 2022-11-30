#pragma once

#include <algorithm>
#include <cassert>
#include <cmath>
#include <initializer_list>
#include <iostream>
#include <memory>

// A[NxN] * B[NxN] = C[NxN]
template<typename T>
void MatrixMult(const T* a, const T* b, T* c, size_t n)
{
    std::fill(c, &c[n * n], T());

    for (size_t i = 0; i < n; i++)
    {
        for (size_t l = 0; l < n; l++)
        {
            const T a_item = a[i * n + l];

            for (size_t j = 0; j < n; j++)
            {
                c[i * n + j] += a_item * b[l * n + j];
            }
        }
    }
}


template<typename T>
void MatrixOutput(const T* a, size_t n, std::string prefix = std::string())
{
    for (size_t i = 0; i < n; i++)
    {
        std::cout << prefix;

        for (size_t j = 0; j < n; j++)
        {
            std::cout << a[i * n + j] << " ";
        }

        std::cout << std::endl;
    }
}


template<typename T>
class Matrix
{
public:

    Matrix()
        : m_size(0)
    {
    }

    Matrix(size_t n)
    {
        Init(n);
    }

    Matrix(std::initializer_list<T> list)
    {
        Init(list);
    }

    Matrix(Matrix&& src)
        : m_size(src.m_size)
        , m_data(std::move(src.m_data))
    {
    }

    size_t GetSize() const { return m_size; }
    const T* GetData() const { return m_data.get(); }

    const T& GetAt(size_t i, size_t j) { assert(i < m_size&& j < m_size); return m_data[i * m_size + j]; }
    void SetAt(size_t i, size_t j, const T& value) { assert(i < m_size&& j < m_size); m_data[i * m_size + j] = value; }

    void Mult(const Matrix<T>& a, const Matrix<T>& b)
    {
        assert(a.GetSize() == b.GetSize());

        Init(a.GetSize());
        MatrixMult(a.GetData(), b.GetData(), m_data.get(), GetSize());
    }

    void Output(std::string prefix = std::string())
    {
        MatrixOutput(m_data.get(), GetSize(), prefix);
    }

private:

    void Init(size_t n)
    {
        m_size = n;
        m_data = std::make_unique<T[]>(n * n);
        std::fill(m_data.get(), std::next(m_data.get(), n * n), T());
    }

    void Init(std::initializer_list<T> list)
    {
        auto n = static_cast<size_t>(std::ceil(std::sqrt(std::distance(std::begin(list), std::end(list)))));
        m_size = n;
        m_data = std::make_unique<T[]>(n * n);
        std::copy(std::begin(list), std::next(std::begin(list), n * n), m_data.get());
    }

    std::unique_ptr<T[]> m_data;
    size_t m_size;
};
