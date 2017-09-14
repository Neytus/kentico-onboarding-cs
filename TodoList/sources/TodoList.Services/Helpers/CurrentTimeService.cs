using System;
using TodoList.Contracts.Services;

namespace TodoList.Services.Helpers
{
    internal class CurrentTimeService : ICurrentTimeService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
