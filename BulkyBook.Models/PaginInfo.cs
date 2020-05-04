using System;

namespace BulkyBook.Models
{
    public class PaginInfo
    {
        public int TotalItem { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string UrlParam { get; set; }

        public int TotalPage => (int)Math.Ceiling((decimal)TotalItem / ItemPerPage);
    }
}
