using System.ComponentModel.DataAnnotations;

namespace UrlShortening.DTO;

public class EditUrlDto
{
    // должна быть по умолчанию
    [Required]
    public required int Id { get; set; }
    [Required(ErrorMessage = "У Url должно быть название")]
    public required string Title { get; set; }
    [Required(ErrorMessage = "Должна быть сама ссылка")]
    [MinLength(5)]
    public required string LongUrl { get; set; }
}