using HR_MVC_ITI.Data;
using HR_MVC_ITI.Mapping;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HR_MVC_ITI.Services;

namespace HR_MVC_ITI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<HRDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("HRConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<HRDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Login";
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IFileSystem, PhysicalFileSystem>();
            builder.Services.AddScoped<IResumeStorage, ResumeStorage>();

            builder.Services.AddAutoMapper(_ => { }, typeof(MappingProfile).Assembly);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var users = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                foreach (var role in new[] { "HR", "Employee" }) if (!await roles.RoleExistsAsync(role)) await roles.CreateAsync(new IdentityRole(role));
                const string email = "hr@company.com";
                var hr = await users.FindByEmailAsync(email);
                if (hr is null)
                {
                    hr = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true, FullName = "HR Administrator", Role = "HR" };
                    var created = await users.CreateAsync(hr, "Hr@12345");
                    if (!created.Succeeded) throw new InvalidOperationException(string.Join(", ", created.Errors.Select(e => e.Description)));
                }
                if (!await users.IsInRoleAsync(hr, "HR")) await users.AddToRoleAsync(hr, "HR");
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
