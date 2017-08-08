using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Api.Tests.Util
{
    public sealed class HttpResponseMessageEqualityComparer : IEqualityComparer<HttpResponseMessage>
    {
        private static readonly Lazy<HttpResponseMessageEqualityComparer> Lazy = new Lazy<HttpResponseMessageEqualityComparer>(
            () => new HttpResponseMessageEqualityComparer()
        );

        public static HttpResponseMessageEqualityComparer Instance => Lazy.Value;

        private HttpResponseMessageEqualityComparer()
        {
        }

        public bool Equals(HttpResponseMessage x, HttpResponseMessage y)
        {
            if ((x == null) || (y == null) || x.GetType() != y.GetType()) return false;

            return x.StatusCode.Equals(y.StatusCode) && x.Version.Equals(y.Version) && x.ReasonPhrase.Equals(y.ReasonPhrase);
        }

        public int GetHashCode(HttpResponseMessage obj)
        {
            return obj.GetHashCode();
        }
    }
}
