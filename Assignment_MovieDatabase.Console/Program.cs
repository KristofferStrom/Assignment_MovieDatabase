using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Interfaces;
using Assignment_MovieDatabase.Console.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
           .ConfigureServices(services =>
           {
               services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\CMS23\Databasteknik\Assignment_MovieDatabase\Assignment_MovieDatabase.Console\Contexts\movie_database_db.mdf;Integrated Security=True;Connect Timeout=30"));
               services.AddTransient<UserInterfaceService>();
               services.AddTransient<IMainMenuService, MainMenuService>();
               services.AddTransient<IActorMenuService, ActorMenuService>();
               services.AddTransient<IMovieMenuService, MovieMenuService>();
           })
           .Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var menuService = services.GetRequiredService<IMainMenuService>();

    menuService.MainMenu();
}

await host.RunAsync();