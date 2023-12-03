using Microsoft.EntityFrameworkCore;
using ShortUrl;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<Context>(options =>
//{
//    string connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//});
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/{shortUrl}", async context =>
    {
        string shortUrl = context.Request.RouteValues["shortUrl"] as string;
        using (Context _context = new Context())
        {
            string longUrl = _context.url.Where(p => p.ShortUrl.Substring(p.ShortUrl.Length - 8) == shortUrl).FirstOrDefault()?.LongUrl;
            if(longUrl != null)
            {
                _context.url.Where(p => p.ShortUrl.Substring(p.ShortUrl.Length - 8) == shortUrl).FirstOrDefault().CountOfClick++;
                _context.SaveChanges();
                context.Response.Redirect(longUrl);
            }
        }

    });
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
