from turtle import color, title
import pandas
import matplotlib.pyplot as plt, numpy as np

df = pandas.read_csv('precipitation.csv')

#print(df.head(50))
#print(df.tail(5))
#print(df['Country or Area'].values)

df = df.set_index(df['Country or Area'])
df.drop(['Country or Area'], axis=1, inplace = True)
years = df.axes[1]

df["SUM"] = df.sum(axis=1)

sorted_df = df.sort_values(by="SUM", ascending=False)

# Top 5 fig
top5_bars = sorted_df[years][0:5].transpose()
ax = top5_bars.plot(figsize=(10,8), marker="o", linestyle="-", title="Top 5 Countries with highest Precopitation")
ax.set_xlabel("Years")
ax.set_ylabel("Preception (million cubic meters)")


# Top 5 chart
top5_sums = pandas.Series(sorted_df["SUM"].head(5))
precipitation_sums = top5_sums
other_sums = sorted_df["SUM"][5:].sum()
precipitation_sums["Other"] = other_sums

plt.figure(figsize=(8,5))
plt.axis("equal")
plt.title("Annual precipitation percentage", y=1.08)
plt.pie(precipitation_sums, 
labels=precipitation_sums.index,
colors=["blue", "green", "red", "turquoise", "magenta", "yellow"],
autopct="%1.2f%%",
radius=1.25)
plt.savefig("chart.png")