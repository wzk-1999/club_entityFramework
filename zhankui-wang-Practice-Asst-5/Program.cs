using Microsoft.EntityFrameworkCore;
using zhankui_wang_Practice_Asst_5.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PracticeAsst5DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PracticeAsst5Db") ?? throw new InvalidOperationException("Connection string 'PracticeAsst5DbContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");

app.Run();
