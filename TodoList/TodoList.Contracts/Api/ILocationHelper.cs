using System;
using System.Net.Http;

namespace TodoList.Contracts.Api
{
    public interface ILocationHelper
    {
        string GetLocation(HttpRequestMessage httpRequestMessage, Guid id);
    }
}