using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace ASP_NET_Core_MVC.Data
{
    public class WebStoreDbContextInitializer
    {
        private readonly WebStoreDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public WebStoreDbContextInitializer(WebStoreDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            var db = _dbContext.Database;

            await db.MigrateAsync();

            await IdentityInitializeAsync();

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

        private async Task IdentityInitializeAsync()
        {
            await CreateRoleIfNotExistsAsync(Role.Administrator);
            await CreateRoleIfNotExistsAsync(Role.User);

            if (await _userManager.FindByNameAsync(User.Administrator) is null)
            {
                var user = new User
                {
                    UserName = User.Administrator,
                    Email = "admin@server.com"
                };

                var creationResult = await _userManager.CreateAsync(user, User.AdminPasswordDefault);

                if (creationResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Role.Administrator);
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Ошибка при создании администратора в БД: { string.Join(", ", creationResult.Errors.Select(err => err.Description)) }");
                }
            }
        }

        private async Task CreateRoleIfNotExistsAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new Role { Name = roleName });
            }
        }
    }
}
