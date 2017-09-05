using System;

namespace TodoList.Api.Helpers
{
    public interface ILocationHelper
    {
        string GetLocation(Guid id);
    }
}