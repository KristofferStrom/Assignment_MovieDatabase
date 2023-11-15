using Assignment_MovieDatabase.Console.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment_MovieDatabase.Console.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<ActorEntity> Actors { get; set; }
    public DbSet<AgeLimitEntity> AgeLimits { get; set; }
    public DbSet<DirectorEntity> Directors { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<MovieActorsEntity> MovieActors { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<MovieWritersEntity> MovieWriters { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<WriterEntity> Writers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<MovieWritersEntity>().HasKey(x => new { x.WriterId, x.MovieId });
        //modelBuilder.Entity<MovieActorsEntity>().HasKey(x => new { x.ActorId, x.MovieId });

        modelBuilder.Entity<MovieActorsEntity>()
            .HasKey(x => new { x.MovieId, x.ActorId });
        modelBuilder.Entity<MovieActorsEntity>()
            .HasOne(bc => bc.Movie)
            .WithMany(b => b.MovieActors)
            .HasForeignKey(bc => bc.MovieId);
        modelBuilder.Entity<MovieActorsEntity>()
            .HasOne(bc => bc.Actor)
            .WithMany(c => c.MovieActors)
            .HasForeignKey(bc => bc.ActorId);

        modelBuilder.Entity<MovieWritersEntity>()
            .HasKey(x => new { x.MovieId, x.WriterId });
        modelBuilder.Entity<MovieWritersEntity>()
            .HasOne(bc => bc.Movie)
            .WithMany(b => b.MovieWriters)
            .HasForeignKey(bc => bc.MovieId);
        modelBuilder.Entity<MovieWritersEntity>()
            .HasOne(bc => bc.Writer)
            .WithMany(c => c.MovieWriters)
            .HasForeignKey(bc => bc.WriterId);
    }
}
