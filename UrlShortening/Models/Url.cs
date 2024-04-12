namespace UrlShortening.Models;

public class Url
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string LongUrl { get; set; }
    public required string ShortUrl { get; set; }
    public required DateTime CreatedDateTime { get; init; }
    public int ClickNumber { get; set; }

}