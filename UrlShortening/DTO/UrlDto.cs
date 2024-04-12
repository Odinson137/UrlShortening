namespace UrlShortening.DTO;

public class UrlDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string LongUrl { get; set; }
    public required string ShortUrl { get; set; }
    public required int ClickNumber { get; set; }
    public required DateTime CreatedDateTime { get; set; }
}