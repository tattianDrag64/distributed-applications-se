using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Responses
{
    public class PagedResponse<T> : ServiceResponse<List<T>>
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
