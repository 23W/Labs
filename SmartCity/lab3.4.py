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

top5_sums = pandas.Series(sorted_df["SUM"].head(5))
top5_bars = sorted_df[years][0:5].transpose()
ax = top5_bars.plot(figsize=(10,8), marker="o", linestyle="-", title="Top 5 Countries with highest Precopitation")
ax.set_xlabel("Years")
ax.set_ylabel("Preception (million cubic meters)")
