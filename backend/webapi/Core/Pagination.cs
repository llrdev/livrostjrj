namespace Webapi.Core
{
    public class Pagination
    {
        private readonly int _pageNumberDefault = 1;
        private readonly int _pageSizeDefault = 10;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Pagination()
        {
            PageNumber = _pageNumberDefault;
            PageSize = _pageSizeDefault;
        }

        public Pagination(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < _pageNumberDefault ? _pageNumberDefault : pageNumber;
            PageSize = pageSize > _pageSizeDefault ? _pageSizeDefault : pageSize;
        }

        public void Validar()
        {
            PageNumber = PageNumber < _pageNumberDefault ? _pageNumberDefault : PageNumber;
            PageSize = PageSize < _pageSizeDefault ? _pageSizeDefault : PageSize;
        }
    }
}
