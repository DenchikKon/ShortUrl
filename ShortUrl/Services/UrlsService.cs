using System.Text;

namespace ShortUrl.Services
{
    public class UrlsService
    {
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string ShortenUrl(string longUrl)
        {
            var random = new Random();
            StringBuilder sb = new StringBuilder();
            sb.Append($"https://localhost:7292/");
            for (int i = 0; i < 8; i++)
            {
                sb.Append(Chars[random.Next(Chars.Length)]);
            }
            return sb.ToString();
        }
    }
}
