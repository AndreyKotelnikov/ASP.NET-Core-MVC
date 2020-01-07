using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;

namespace ASP_NET_Core_MVC.Data
{
    public class WebStoreDbContextInitializer
    {
        private readonly WebStoreDbContext _dbContext;

        public WebStoreDbContextInitializer(WebStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            var db = _dbContext.Database;

            await db.MigrateAsync();

            if (await _dbContext.Products.AnyAsync())
            {
                return;
            }

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _dbContext.Brands.AddRangeAsync(TestData.Brands);

                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                await _dbContext.SaveChangesAsync();
                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                await transaction.CommitAsync();
            }

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _dbContext.Sections.AddRangeAsync(TestData.Sections);

                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await _dbContext.SaveChangesAsync();
                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                await transaction.CommitAsync();
            }

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _dbContext.Products.AddRangeAsync(TestData.Products);

                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await _dbContext.SaveChangesAsync();
                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                await transaction.CommitAsync();
            }
        }
    }
}
