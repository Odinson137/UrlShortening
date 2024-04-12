using Microsoft.EntityFrameworkCore;
using UrlShortening.Models;

namespace UrlShortening.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Url> Urls { get; set; }
}