using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperSample.Syncing
{
    public class SyncTest
    {
        [Fact]
        public async Task CanInitializeCollectionAsync()
        {
            var debug = new SyncDebug();
            var items = new List<string> { "one", "two" };
            var result = await debug.InitializeListAsync(items);
            Assert.Equal(items.Count, result.Count);
        }

        [Fact]
        public void ItemsOnlyInitializeOnce()
        {
            var debug = new SyncDebug();
            var count = 0;
            var dictionary = debug.InitializeDictionary(i =>
            {
                Thread.Sleep(1);
                Interlocked.Increment(ref count);
                return i.ToString();
            });
            
            Assert.Equal(100, count);

            Assert.Equal(100, dictionary.Count);
            Assert.True(dictionary.Keys.All(k => k >= 0 && k < 100));
        }
    }
}
