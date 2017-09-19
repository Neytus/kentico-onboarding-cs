using System;

namespace TodoList.Contracts.Api
{
    public interface ILocationHelper
    {
        string GetNodeLocation(Guid id);
    }
}