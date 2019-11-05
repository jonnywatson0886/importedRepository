using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aysnch_looping.Classes
{
    internal class AsyncFunctions
    {
        #region setup

        private readonly List<int> _numbers;
        private readonly int _numberOfEnteries;
        private readonly Random _random;

        public AsyncFunctions()
        {
            _numbers = new List<int>();
            _numberOfEnteries = 100;
            _random = new Random();
            PopulateList();
        }

        public void PopulateList()
        {
            for (var i = 0; i < _numberOfEnteries; i++) _numbers.Add(_random.Next(0, 100));
        }

        #endregion
        #region async

        /// <summary>
        ///     method to be awaited
        /// </summary>
        /// <param name="currentNum"></param>
        /// <returns></returns>
        public static Task DoAsync(int currentNum)
        {
            Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine($"current number: {currentNum}" + $"threadID: {Task.CurrentId}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// goes though all the elements
        /// and then fires up a task for all for them
        /// and completes it async
        /// </summary>
        /// <returns></returns>
        public async Task LoopAsync()
        {
            var tasks = new List<Task>();
            for (var i = 0; i < _numberOfEnteries; i++) tasks.Add(DoAsync(_numbers[i]));

            await Task.WhenAll(tasks);
        }

        #endregion
    }
}