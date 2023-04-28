
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Diagnostics;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
// dùng để kết nối với database
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
//builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseSession();


//app.MapGet("/", () => "Hello World!"); => mặc định ( ko tính tiền )

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    // gõ tên controller mà muốn mặc định chạy vào sau controller= và view mặc định sau action=
    pattern: "{controller}/{action}");

app.Run();

