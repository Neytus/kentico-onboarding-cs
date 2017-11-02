using System;

namespace TodoList.Contracts.Api
{
    public interface ILocator
    {
        Uri GetNodeLocation(Guid id);
    }
}