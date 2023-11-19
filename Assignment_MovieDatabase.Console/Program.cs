using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Interfaces;
using Assignment_MovieDatabase.Console.Repositories;
using Assignment_MovieDatabase.Console.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
           .ConfigureServices(services =>
           {

               services.AddLogging(builder =>
               {
                   builder.AddConsole().SetMinimumLevel(LogLevel.None);
               });

               services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\CMS23\Databasteknik\Assignment_MovieDatabase\Assignment_MovieDatabase.Console\Contexts\movie_database_db.mdf;Integrated Security=True;Connect Timeout=30"));
               services.AddTransient<UserInterfaceService>();
               services.AddTransient<IMainMenuService, MainMenuService>();
               services.AddTransient<IActorMenuService, ActorMenuService>();
               services.AddTransient<IMovieMenuService, MovieMenuService>();
               services.AddTransient<IMovieActorService, MovieActorService>();
               services.AddTransient<IMovieService, MovieService>();
               services.AddTransient<IActorService, ActorService>();
               services.AddTransient<IWriterService, WriterService>();
               services.AddTransient<IMovieWriterService, MovieWriterService>();
               services.AddTransient<ActorRepository>();
               services.AddTransient<MovieActorRepository>();
               services.AddTransient<MovieRepository>();
               services.AddTransient<MovieWriterRepository>();
               services.AddTransient<GenreRepository>();
               services.AddTransient<LanguageRepository>();
               services.AddTransient<AgeLimitRepository>();
               services.AddTransient<DirectorRepository>();
               services.AddTransient<WriterRepository>();

           })
           .Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var menuService = services.GetRequiredService<IMainMenuService>();

    menuService.MainMenu();
}

await host.RunAsync();