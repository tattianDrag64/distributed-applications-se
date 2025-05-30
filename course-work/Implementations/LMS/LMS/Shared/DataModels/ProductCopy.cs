using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekBoklusen.Shared
{
    public class ProductCopy
    {

        public int Id { get; set; }

        public int CopyId { get; set; }


        [ForeignKey(nameof(product))]
        public int ProductId { get; set; }
        public Product product { get; set; }

        public bool IsLoaned { get; set; }



    }
}
