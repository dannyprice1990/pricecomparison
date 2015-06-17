using Chetail.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Chetail.API.Helpers
{
    public class PaginationHelper
    {
        public Pagination getPaginationObject(HttpRequestMessage request, int totalCount, int totalPages, 
                                                    int pageSize, int page, string prevUrl, string nextUrl )
        {
            //Creates a Data Navigation Object for paging data
            var pagination = new Pagination();
            pagination.PageSize = pageSize;
            pagination.TotalCount = totalCount;
            pagination.TotalPages = totalPages;
            pagination.PrevPageUrl = prevUrl;
            pagination.NextPageUrl = nextUrl;
            pagination.Desc = "Page " + (page + 1) + " of " + totalPages;
            pagination.CurrentPage = page;

            return pagination;
        }
    }
}