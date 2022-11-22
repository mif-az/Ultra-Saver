using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;

namespace Ultra_Saver;

public class StatisticsProcessor<T> where T : class
{

    private DbSet<T> model;
    private readonly int _interval;
    private Tuple<int, DateTime> count = Tuple.Create(-1, DateTime.MinValue);
    private ConcurrentDictionary<Selector, Tuple<double, DateTime>> averages;

    public delegate T Selector(T model);

    public StatisticsProcessor(DbSet<T> model, int interval)
    {
        this.model = model;
        _interval = interval;
        averages = new ConcurrentDictionary<Selector, Tuple<double, DateTime>>();
    }

    public int GetCount(Selector selector)
    {
        if (count.Item1 < 0 || DateTime.Compare(count.Item2.AddSeconds(_interval), DateTime.Now) < 0)
        {
            count = Tuple.Create(model.Select(selector.Invoke).Count(), DateTime.Now);
        }
        return count.Item1;
    }

    public async Task<double> GetAverageCollAsync(Selector selector, Func<T, double> elems)
    {
        Tuple<double, DateTime> avg;
        if (averages.TryGetValue(selector, out avg) && DateTime.Compare(avg.Item2.AddSeconds(_interval), DateTime.Now) >= 0)
        {
            return avg.Item1;
        }

        double average = await Task<double>.Run(() => model.Select(selector.Invoke).Average(elems));

        averages.TryAdd(selector, Tuple.Create(average, DateTime.Now));

        return average;
    }

    public StatisticsProcessor<T> UpdateDbContext(DbSet<T> newContext)
    {
        this.model = newContext;
        return this;
    }
}

public interface IStatisticsProcessorFactory
{
    StatisticsProcessor<T> GetStatisticsProcessor<T>(DbSet<T> modelSet, int interval = 60) where T : class;
}

public class StatisticsProcessorFactory : IStatisticsProcessorFactory
{

    private ConcurrentDictionary<Tuple<Type, int>, object> availableProcessors = new ConcurrentDictionary<Tuple<Type, int>, object>();

    private void AddProcessor<T>(Tuple<Type, int> key, StatisticsProcessor<T> value) where T : class
    {
        availableProcessors.TryAdd(key, value);
    }

    private StatisticsProcessor<T>? GetProcessor<T>(Tuple<Type, int> type) where T : class
    {
        object obj;
        if (availableProcessors.TryGetValue(type, out obj))
        {
            return (StatisticsProcessor<T>)obj;
        }
        return null;
    }

    public StatisticsProcessor<T> GetStatisticsProcessor<T>(DbSet<T> modelSet, int interval = 60) where T : class
    {
        var sp = GetProcessor<T>(Tuple.Create(modelSet.GetType(), interval));
        if (sp == null)
        {
            sp = new StatisticsProcessor<T>(modelSet, interval);
            AddProcessor<T>(Tuple.Create(modelSet.GetType(), interval), sp);
        }
        return sp.UpdateDbContext(modelSet);
    }

}