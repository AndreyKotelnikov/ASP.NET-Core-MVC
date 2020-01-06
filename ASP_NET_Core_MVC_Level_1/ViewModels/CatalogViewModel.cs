using System.Collections.Generic;

namespace ASP_NET_Core_MVC.ViewModels
{
    public class CatalogViewModel
    {
        public int? BrandId { get; set; }

        public int? SectionId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}