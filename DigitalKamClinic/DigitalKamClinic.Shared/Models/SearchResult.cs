using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DigitalKamClinic.Shared.Models
{
    public class SearchResult<T>
    {
        public List<T> ResultData { get; set; }
        public List<T> ResultDataUnFiltered { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public List<SelectListItem> PageSizes { get; set; }
        public string PageSize { get; set; }
        public string SearchText { get; set; }
        public string OrderBy { get; set; }

        public int TotalRecords { get; set; }
    }
}
