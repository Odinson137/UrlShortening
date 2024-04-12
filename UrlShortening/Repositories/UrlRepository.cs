using Microsoft.EntityFrameworkCore;
using UrlShortening.Data;
using UrlShortening.DTO;
using UrlShortening.Interfaces;
using UrlShortening.Models;

namespace UrlShortening.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly DataContext _dataContext;
    
    public UrlRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<ICollection<UrlDto>> GetUrlsAsync()
    {
        return await _dataContext.Urls.Select(c => new UrlDto
        {
            Id = c.Id,
            Title = c.Title,
            LongUrl = c.LongUrl,
            ShortUrl = c.ShortUrl,
            ClickNumber = c.ClickNumber,
            CreatedDateTime = c.CreatedDateTime,
        }).ToListAsync();
    }

    public async Task ClickUpAsync(string shortUrl)
    {
        await _dataContext.Urls
            .Where(c => c.ShortUrl == shortUrl)
            .ExecuteUpdateAsync(p => 
                p.SetProperty(c => c.ClickNumber, c=> c.ClickNumber + 1));
    }

    public async Task<Url?> GetUrlByIdAsync(int id)
    {
        return await _dataContext.Urls.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<string?> GetLongUrlByShortUrlAsync(string shortUrl)
    {
        return await _dataContext.Urls
            .Where(c => c.ShortUrl == shortUrl)
            .Select(c => c.LongUrl)
            .FirstOrDefaultAsync();
    }

    public async Task<EditUrlDto?> GetEditUrlByIdAsync(int id)
    {
        return await _dataContext.Urls
            .Select(c => new EditUrlDto
            {
                Id = c.Id,
                Title = c.Title,
                LongUrl = c.LongUrl,
            })
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public void DeleteUrlAsync(Url url)
    {
        _dataContext.Urls.Remove(url);
    }

    public async Task AddUrlAsync(Url url)
    {
        await _dataContext.Urls.AddAsync(url);
    }

    public async Task SaveAsync()
    {
        await _dataContext.SaveChangesAsync();
    }
}