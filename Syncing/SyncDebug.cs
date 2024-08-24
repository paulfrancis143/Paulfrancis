using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        // asynchronous initialization and Here we try to Replace Parallel.ForEach with Task.WhenAll to handle asynchronous operations correctly. 
//As Parallel(Threading) will consume lot of cores and memory but Async will help to use resource optmically
        public async Task<List<string>> InitializeListAsync(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();
            var tasks = items.Select(async i =>
            {
                var r = await Task.Run(() => i).ConfigureAwait(false);
                bag.Add(r);
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);
            var list = bag.ToList();
            return list;
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();

            var concurrentDictionary = new ConcurrentDictionary<int, string>();
            var tasks = Enumerable.Range(0, 3)
                .Select(_ => Task.Run(() =>
                {
                    foreach (var item in itemsToInitialize)
                    {
                        concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
                    }
                }))
                .ToArray();

            Task.WaitAll(tasks);

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
