namespace LearningSystem.Web.Infrastructure.Extensions
{
    using Data;
    using Models.DataModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using static WebConstants;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<LearningSystemDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    
                    var roles = new[]
                    {
                        AdministratorRole,
                        BlogAuthorRole,
                        TrainerRole
                    };

                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role

                            });
                        }

                    }
                    
                    var adminEmail = "admin@learningsystem.com";

                    var adminUser = await userManager.FindByEmailAsync(adminEmail);

                    if (adminUser == null)
                    {
                        adminUser = new User()
                        {
                            Email = adminEmail,
                            UserName = "Admin",
                            Name = "Admin"
                            
                        };
                        await userManager.CreateAsync(adminUser, "adminpassword");

                        await userManager.AddToRoleAsync(adminUser, AdministratorRole);
                    }
                })
                .Wait();
            }

            return app;
        }
    }
}
