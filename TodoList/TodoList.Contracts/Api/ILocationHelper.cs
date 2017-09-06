using System;

namespace TodoList.Contracts.Api
{
    public interface ILocationHelper
    {
        string GetLocation(Guid id);
    }
}