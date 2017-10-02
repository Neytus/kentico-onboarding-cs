using System;
using TodoList.Contracts.Services;

namespace TodoList.Services.Static_Wrappers
{
    internal class CurrentTimeService : ICurrentTimeService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
