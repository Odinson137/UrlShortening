namespace UrlShortening.DTO;

public class CreateUrlDto
{
    public string Title { get; set; }
    public string LongUrl { get; set; }

    public CreateUrlDto(string title, string longUrl)
    {
        Title = title;
        LongUrl = longUrl;
    }

    public CreateUrlDto()
    {
        Title = "";
        LongUrl = "";  
    }
}