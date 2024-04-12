using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortening.DTO;
using UrlShortening.Interfaces;
using UrlShortening.Models;

namespace UrlShortening.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUrlRepository _urlRepository;

    public HomeController(ILogger<HomeController> logger, IUrlRepository urlRepository)
    {
        _logger = logger;
        _urlRepository = urlRepository;
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Index page");

        var urls = await _urlRepository.GetUrlsAsync();
        
        _logger.LogError("Open home page");
        return View(urls);
    }

    private string GetUrl(string id)
        => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Home/Look?shortUrl={id}";
    
    public async Task<IActionResult> Look(string shortUrl)
    {
        _logger.LogInformation("Look page");

        var url = GetUrl(shortUrl);
        var longUrl = await _urlRepository.GetLongUrlByShortUrlAsync(url);

        if (longUrl == null)
        {
            _logger.LogError("This url is not found");
            return NotFound("This url is not found");
        } 
        
        _logger.LogInformation("Click up");
        await _urlRepository.ClickUpAsync(url);
        
        return Redirect(longUrl);
    }

    public IActionResult Privacy()
    {
        _logger.LogInformation("Privacy page");
        return View();
    }
    
    public IActionResult Create()
    {
        _logger.LogInformation("Creating page");
        return View(new CreateUrlDto());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateUrlDto urlDto)
    {
        _logger.LogInformation("Post creating page");

        var url = new Url
        {
            Title = urlDto.Title,
            LongUrl = urlDto.LongUrl,
            ShortUrl = GetUrl(Guid.NewGuid().ToString()),
            CreatedDateTime = DateTime.Now,
        };

        await _urlRepository.AddUrlAsync(url);
        await _urlRepository.SaveAsync();
        
        _logger.LogError("Redirect to home page");

        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Edit(int urlId)
    {
        _logger.LogInformation("Edit page");

        var url = await _urlRepository.GetEditUrlByIdAsync(urlId);

        if (url == null)
        {
            _logger.LogError("This url is not found");
            return NotFound("This url is not found");
        }
        
        _logger.LogError("Successfully getting edit page");
        return View(url);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(EditUrlDto urlDto)
    {
        _logger.LogInformation("Post edit page");

        var url = await _urlRepository.GetUrlByIdAsync(urlDto.Id);

        if (url == null)
        {
            _logger.LogError("This url is not found");
            return NotFound("This url is not found");
        }
        
        url.LongUrl = urlDto.LongUrl;
        url.Title = urlDto.Title;

        await _urlRepository.SaveAsync();
        
        _logger.LogError("Redirect to edit page");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int urlId)
    {
        _logger.LogInformation("Post delete page");

        var url = await _urlRepository.GetUrlByIdAsync(urlId);

        if (url == null)
        {
            _logger.LogError("This url is not found");
            return NotFound("This url is not found");
        }

        _urlRepository.DeleteUrlAsync(url);
        
        await _urlRepository.SaveAsync();
        
        _logger.LogError("Redirect to home page");
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}