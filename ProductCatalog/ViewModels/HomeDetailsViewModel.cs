using ProductCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Product Product { get; set; }
        public string PageHeader { get; set; }
    }
}
