import pandas
import matplotlib.pyplot as plt, numpy as np

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
print(top10)

# Graph: Top 10 driest countries
top10_t = top10.transpose()
plt.figure(figsize=(10,8))
plt.title("Top 10 driest countries")
plt.xlabel("Years")
plt.ylabel("Preception (million cubic meters)")
plt.plot(top10_t, marker="o", linestyle="-")
plt.savefig("top10driest_graph.png")

# Chart: Top 10 driest countries
top10_sums = top10_sums["SUM"]
plt.figure(figsize=(10,8))
plt.axis("equal")
plt.title("Comparison chart", y=1.08)
plt.pie(top10_sums, labels=top10_sums.index, autopct="%1.2f%%", radius=1.25)
plt.savefig("top10driest_chart.png")