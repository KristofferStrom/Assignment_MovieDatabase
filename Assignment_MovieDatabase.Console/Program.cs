using Assignment_MovieDatabase.Console.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
           .ConfigureServices(services =>
           {
               services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\CMS23\Databasteknik\Assignment_MovieDatabase\Assignment_MovieDatabase.Console\Contexts\movie_database_db.mdf;Integrated Security=True;Connect Timeout=30"));

               //services.AddScoped<CategoryService>();
               //services.AddScoped<ProductService>();
               //services.AddScoped<MenuService>();
           })
           .Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //var menuService = services.GetRequiredService<MenuService>();

    //menuService.Hej();
}

await host.RunAsync();