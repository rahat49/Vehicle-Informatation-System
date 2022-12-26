using Microsoft.EntityFrameworkCore;

using VIMS.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddDbContext<BRTAContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BRTAContext") ?? throw new InvalidOperationException("Connection string 'BRTAContext' not found."));
     
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Master}/{action=Index}/{id?}");

app.Run();

