using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepository;
using Repositories.Repository;
using Services.IService;
using Services.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Cấu hình các dịch vụ
        builder.Services.AddDbContext<BookingManagementContext>(options => options.UseSqlServer("DefaultConnectionString"));
        builder.Services.AddRazorPages();
        builder.Services.AddSession();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IEmailService, EmailService>();

        // Cấu hình Authentication (Cookie Authentication)
        builder.Services.AddAuthentication("Cookies")
            .AddCookie(options =>
            {
                options.LoginPath = "/Index";  // Trang đăng nhập
                options.LogoutPath = "/Index"; // Trang đăng xuất
                options.AccessDeniedPath = "/Error";
            });

        // Cấu hình Authorization
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Manager", policy => policy.RequireRole("1"));  // Phân quyền cho Manager (RoleId = 1)
            options.AddPolicy("Role2", policy => policy.RequireRole("2"));
            options.AddPolicy("Teacher", policy => policy.RequireRole("3"));
        });

        var app = builder.Build();

        // Cấu hình middlewares
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseSession();
        app.UseStaticFiles();

        // Cấu hình Routing
        app.UseRouting();

        // Cấu hình Authentication và Authorization
        app.UseAuthentication(); // Thêm middleware này
        app.UseAuthorization();  // Thêm middleware này

        // Cấu hình các endpoints
        app.MapRazorPages();

 

        app.Run();
    }
}
