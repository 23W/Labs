from turtle import color
import pandas
import matplotlib.pyplot as plt, numpy as np

df = pandas.read_csv('precipitation.csv')

#print(df.head(50))
#print(df.tail(5))
#print(df['Country or Area'].values)

df = df.set_index(df['Country or Area'])
df.drop(['Country or Area'], axis=1, inplace = True)
#df.head()

algeria = df.loc['Algeria']
years = algeria.index
index = np.arange(len(years))

plt.figure(figsize=(8,5))
bar_width = 0.5
plt.bar(index, algeria, bar_width, color="b")
plt.xlabel("Years")
plt.ylabel("Preception (million cubic meters)")
plt.title("Preception of %s between 1990 and 2009" % algeria.name, y=1.08)
plt.show()
