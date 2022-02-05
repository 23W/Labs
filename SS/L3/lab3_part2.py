import os
import pandas
import matplotlib.pyplot as plt, numpy as np

def wetCounties(years, top_limit):
    # read data
    df = pandas.read_csv('precipitation.csv')

    # rename indices
    df = df.set_index(df['Country or Area'])
    df.drop(['Country or Area'], axis="columns", inplace=True)

    #split years
    df = df[years]

    # add SUM column and sort by descending order    
    df["SUM"] = df.sum(axis=1)
    sorted_df = df.sort_values(by="SUM", ascending=False)

    # Wettest countries
    tops_sum = sorted_df[0:top_limit]
    tops = sorted_df[years][0:top_limit]

    # Output: Wettest countries
    years_title =  "[{}..{}]".format(years[0], years[-1]) if len(years)>1 else str(years[0])
    title = "Top {} wettest countries in {}".format(top_limit, years_title)
    with open("./output/top{}wettest{}_graph.txt".format(top_limit, years_title), 'w') as f:
        print(title, file=f)
        print(tops, file=f)

    # Graph: Top wettest countries
    if len(years)>1:
        tops_t = tops.transpose()
        plt.figure(figsize=(10,8))
        plt.title(title)
        plt.xlabel("Years")
        plt.ylabel("Preception (million cubic meters)")
        plt.plot(tops_t, marker="o", linestyle="-")
        plt.savefig("./output/top{}wettest{}_graph.png".format(top_limit, years_title))
    
    # Chart: Top wettest countries
    if top_limit>1:
        title = "Comparision chart of top {} wettest countries in {}".format(top_limit, years_title)
        tops_sums = tops_sum["SUM"]
        plt.figure(figsize=(10,8))
        plt.axis("equal")
        plt.title(title, y=1.08)
        plt.pie(tops_sums, labels=tops_sums.index, autopct="%1.2f%%", radius=1.25)
        plt.savefig("./output/top{}wettest{}_comp_chart.png".format(top_limit, years_title))

        title = "Full chart of top {} wettest countries in {}".format(top_limit, years_title)
        tops_sums["Other"] = sorted_df["SUM"][top_limit:].sum()
        plt.figure(figsize=(10,8))
        plt.axis("equal")
        plt.title(title, y=1.08)
        plt.pie(tops_sums, labels=tops_sums.index, autopct="%1.2f%%", radius=1.25)
        plt.savefig("./output/top{}wettest{}_other_chart.png".format(top_limit, years_title))

# Main
if not os.path.exists('output'):
    os.makedirs('output')

range1 = ["1990"] # range [1990...1990]
range2 = ["{:d}".format(x) for x in range(1995, 2010)] # range [1995...2010)
wetCounties(range1, 5)
wetCounties(range2, 5)