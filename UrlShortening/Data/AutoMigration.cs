using Microsoft.EntityFrameworkCore;

namespace UrlShortening.Data;

public class AutoMigration 
{
    private readonly DataContext _context;
    
    public AutoMigration(DataContext context)
    {
        _context = context;
    }

    public async Task Migrate()
    {
        var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await _context.Database.MigrateAsync();
        }
    }
}

public interface IAutoMigration
{
    public Task Migrate();
}