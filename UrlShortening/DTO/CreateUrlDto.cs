using System.ComponentModel.DataAnnotations;

namespace UrlShortening.DTO;

public class CreateUrlDto
{
    [Required(ErrorMessage = "У Url должно быть название")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Должна быть сама ссылка")]
    [MinLength(5)]
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