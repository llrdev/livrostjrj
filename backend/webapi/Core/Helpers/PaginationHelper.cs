using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Webapi.Core.Response;

namespace Webapi.Core.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(HttpRequest request, IUriHelper uriHelper, Pagination validFilter, List<T> pagedData, int totalRecords, string success)
        {
            var route = request.PathBase.Value + request.Path.Value;
            var query = GetQueryString(request);
            if (query != null) route += query;

            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize, success);
            var totalPages = totalRecords / (double)validFilter.PageSize;
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriHelper.GetPageUri(validFilter.PageNumber + 1, validFilter.PageSize, route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriHelper.GetPageUri(validFilter.PageNumber - 1, validFilter.PageSize, route)
                : null;
            respose.FirstPage = uriHelper.GetPageUri(1, validFilter.PageSize, route);
            respose.LastPage = uriHelper.GetPageUri(roundedTotalPages, validFilter.PageSize, route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        /// <summary>
        /// Recupera query string
        /// </summary>
        /// <returns></returns>
        private static string GetQueryString(HttpRequest request)
        {
            if (request.QueryString.HasValue)
            {
                var keys = request.Query.Keys.Where(x => !x.ToLower().Equals("pagenumber") && !x.ToLower().Equals("pagesize")).ToList();
                string queryString = "?";
                int count = keys.Count - 1;
                var values = keys.ToArray();

                for (int index = 0; index <= count; index++)
                {
                    var key = values[index];
                    var continua = index != count ? "&" : "";
                    queryString += $"{key}={request.Query[key]}{continua}";
                }

                return queryString.Length > 1 ? queryString : null;
            }

            return null;
        }
    }
}
