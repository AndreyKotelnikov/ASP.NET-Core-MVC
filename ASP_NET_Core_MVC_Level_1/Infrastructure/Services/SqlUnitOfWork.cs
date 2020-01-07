using System.Collections.Generic;
using System.Linq;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;

namespace ASP_NET_Core_MVC.Infrastructure.Services
{
    public class SqlUnitOfWork : IProductData
    {
        private readonly WebStoreDbContext _dbContext;

        public SqlUnitOfWork(WebStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Brand> GetBrands() => _dbContext.Brands
            .Include(brand => brand.Products)
            .AsEnumerable();

        public IEnumerable<Section> GetSections() => _dbContext.Sections
            .Include(section => section.Products)
            .AsEnumerable();

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> query = _dbContext.Products;

            if (filter?.BrandId != null)
            {
                query = query.Where(p => p.BrandId == filter.BrandId);
            }

            if (filter?.SectionId != null)
            {
                query = query.Where(p => p.SectionId == filter.SectionId);
            }

            return query.AsEnumerable();
        }
    }
}
