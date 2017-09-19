using System;

namespace TodoList.Contracts.Api
{
    public interface ILocationHelper
    {
        Uri GetNodeLocation(Guid id);
    }
}