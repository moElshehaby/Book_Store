using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using book_store.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<book_storeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("book_storeContext") ?? throw new InvalidOperationException("Connection string 'book_storeContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(1); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//UseSession
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=books}/{action=landing}/{id?}");

app.Run();
