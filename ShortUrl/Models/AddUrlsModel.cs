using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Models
{
    public class AddUrlsModel
    {
        [Required(ErrorMessage = "требуется ввести ссылку")]
        public string LongUrl { get; set; }
        public string? ShortUrl { get; set; }
    }
}
