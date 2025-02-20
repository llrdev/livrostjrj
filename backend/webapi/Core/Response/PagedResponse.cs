using System;

namespace Webapi.Core.Response
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, string success)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Success = success ?? Mensagens.SucessoDefault;
        }
    }
}
