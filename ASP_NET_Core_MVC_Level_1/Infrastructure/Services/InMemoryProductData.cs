using System.Collections.Generic;
using System.Linq;
using ASP_NET_Core_MVC.Data;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using WebStore.Domain.Entities;

namespace ASP_NET_Core_MVC.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;
       
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            var query = TestData.Products;

            if (filter?.BrandId != null)
            {
                query = query.Where(p => p.BrandId == filter.BrandId);
            }

            if (filter?.SectionId != null)
            {
                query = query.Where(p => p.SectionId == filter.SectionId);
            }

            if (filter?.ProductIdList != null)
            {
                query = query.Join(
                    filter.ProductIdList, 
                    p => p.Id, 
                    id => id, 
                    (p, i) => p);
            }

            return query;
        }

        public Product GetProductById(int id) => TestData.Products.SingleOrDefault(p => p.Id == id);
    }
}
