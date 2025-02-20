using Microsoft.AspNetCore.WebUtilities;
using System;

namespace Webapi.Core.Helpers
{
    public class UriHelper : IUriHelper
    {
        private readonly string _baseUri;

        public UriHelper(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(int pageNumber, int pageSize, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", pageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
