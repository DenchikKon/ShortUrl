using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Models
{
    public class UrlModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "заполните поле")]
        public string LongUrl { get; set; }

        [Required(ErrorMessage = "заполните поле")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "требуется 8 символов")]
        public string ShortUrl { get; set; }
        public DateTime dateCreated { get; set; }

        [Required(ErrorMessage = "заполните поле")]
        public int CountOfClick { get; set; }
    }
}
