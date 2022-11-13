import pandas
df = pandas.read_csv('precipitation.csv')

#print(df.head(50))
#print(df.tail(5))
#print(df['Country or Area'].values)

df = df.set_index(df['Country or Area'])
df.drop(['Country or Area'], axis=1, inplace = True)
#df.head()

%matplotlib inline
albania = df.loc['Albania']
ax = albania.plot(kind='bar', figsize=(8,5), title="Preception of %s between 1990 and 2009" % albania.name)
ax.set_ylabel("Preception (million cubic meters)")
ax.set_xlabel("Years")