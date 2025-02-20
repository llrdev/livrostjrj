using System;

namespace Webapi.Core.Helpers
{
    public interface IUriHelper
    {
        public Uri GetPageUri(int pageNumber, int pageSize, string route);
    }
}
