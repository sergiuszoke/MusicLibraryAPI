using Microsoft.EntityFrameworkCore;
using MusicLibraryDAL.Models;

namespace MusicLibraryDAL
{
    public class DataContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
