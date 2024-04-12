using UrlShortening.DTO;
using UrlShortening.Models;

namespace UrlShortening.Interfaces;

public interface IUrlRepository
{
    public Task<ICollection<UrlDto>> GetUrlsAsync();
    public Task ClickUpAsync(string shortUrl);
    public Task<Url?> GetUrlByIdAsync(int id);
    public Task<string?> GetLongUrlByShortUrlAsync(string shortUrl);
    public Task<EditUrlDto?> GetEditUrlByIdAsync(int id);
    public void DeleteUrlAsync(Url url);
    public Task AddUrlAsync(Url url);
    public Task SaveAsync();
}