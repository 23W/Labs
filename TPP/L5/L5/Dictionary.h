#pragma once

#include <fstream>
#include <string>
#include <vector>

using TDictionary = std::vector<std::string>;

TDictionary LoadDictionary(const std::string& path)
{
    TDictionary dictionary;

    std::ifstream dictionaryStream(path);

    bool readCount = true;
    std::string key;
    while (std::getline(dictionaryStream, key))
    {
        if (readCount)
        {
            readCount = false;

            const auto count = std::stoi(key);
            dictionary.reserve(count);

            continue;
        }

        if (!key.empty())
        {
            dictionary.emplace_back(key);
        }
    }

    return dictionary;
}
