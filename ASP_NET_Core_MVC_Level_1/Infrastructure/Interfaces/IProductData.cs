using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace ASP_NET_Core_MVC.Infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Brand> GetBrands();

        IEnumerable<Section> GetSections();

        IEnumerable<Product> GetProducts(ProductFilter filter = null);
    }
}
