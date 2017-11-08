using System;

namespace TodoList.Contracts.Services
{
    public interface ICurrentTimeService
    {
        DateTime GetCurrentTime();
    }
}