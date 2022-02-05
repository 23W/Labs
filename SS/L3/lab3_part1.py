import os
import pandas
import matplotlib.pyplot as plt, numpy as np

if not os.path.exists('output'):
    os.makedirs('output')

# read data
df = pandas.read_csv('precipitation.csv')

# rename indices
df = df.set_index(df['Country or Area'])
df.drop(['Country or Area'], axis="columns", inplace = True)
years = df.axes[1]

# add SUM column and sort by ascending order
df["SUM"] = df.sum(axis=1)
sorted_df = df.sort_values(by="SUM", ascending=True)

# Top 10 driest countries
top10_sums = sorted_df[0:10]
top10 = sorted_df[years][0:10]

# Output: Top 10 driest countries
with open("./output/top10driest.txt", 'w') as f:
    print(top10, file=f)

# Graph: Top 10 driest countries
top10_t = top10.transpose()
plt.figure(figsize=(10,8))
plt.title("Top 10 driest countries")
plt.xlabel("Years")
plt.ylabel("Precipitation (million cubic meters)")
plt.plot(top10_t, marker="o", linestyle="-")
plt.savefig("./output/top10driest_graph.png")

# Chart: Top 10 driest countries
top10_sums = top10_sums["SUM"]
plt.figure(figsize=(10,8))
plt.axis("equal")
plt.title("Comparison chart", y=1.08)
plt.pie(top10_sums, labels=top10_sums.index, autopct="%1.2f%%", radius=1.25)
plt.savefig("./output/top10driest_chart.png")