using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.DTO
{
    public class Pagination
    {
        //Page Size
        public int PageSize { get; set; }
        //Total Count of Records
        public int TotalCount { get; set; }
        //Total Count of Pages
        public int TotalPages { get; set; }
        //Previous Page API Url
        public string PrevPageUrl { get; set; }
        //Next Page API Url
        public string NextPageUrl { get; set; }
        //Current Page Number
        public int CurrentPage { get; set; }
        //Description, such as "Page 1 of 24"
        public string Desc { get; set; }
    }
}
