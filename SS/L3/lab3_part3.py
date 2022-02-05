import os
import pandas
import matplotlib.pyplot as plt, numpy as np

def countryPrecipitation(country, years):
    # read data
    df = pandas.read_csv('precipitation.csv')

    # rename indices
    df = df.set_index(df['Country or Area'])
    df.drop(['Country or Area'], axis="columns", inplace=True)

    #slice by country and years
    precipitation = df.loc[country, years]

    # Output: country precipitation
    years_title =  "[{}..{}]".format(years[0], years[-1]) if len(years)>1 else str(years[0])
    title = "Precipitation in {} during {}".format(country, years_title)
    with open("./output/precipitation_{}_{}_data.txt".format(country, years_title), 'w') as f:
        print(title, file=f)
        print(precipitation, file=f)

    # Graph: country precipitation
    if len(years)>1:
        plt.figure(figsize=(10,8))
        plt.title(title)
        plt.xlabel("Years")
        plt.ylabel("Precipitation (million cubic meters)")
        plt.bar(precipitation.index, precipitation, width=0.5)
        plt.savefig("./output/precipitation_{}_{}_bar.png".format(country, years_title))

    # Chart: Top wettest countries
    if len(years)>1:
        title = "Comparision chart of precipitation in {} during {}".format(country, years_title)
        plt.figure(figsize=(10,8))
        plt.axis("equal")
        plt.title(title, y=1.08)
        plt.pie(precipitation, labels=precipitation.index, autopct="%1.2f%%", radius=1.25)
        plt.savefig("./output/precipitation_{}_{}_chart.png".format(country, years_title))

# Main
if not os.path.exists('output'):
    os.makedirs('output')

country = "Israel"
range1 = ["1990"] # range [1990...1990]
range2 = ["{:d}".format(x) for x in range(1995, 2010)] # range [1995...2010)
countryPrecipitation(country, range1)
countryPrecipitation(country, range2)