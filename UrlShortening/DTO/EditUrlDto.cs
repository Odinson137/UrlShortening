namespace UrlShortening.DTO;

public class EditUrlDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string LongUrl { get; set; }
}