namespace Cronofly.Controllers.LinkShortening
{
    public class SuccessResource
    {
        public string ShortenedUrl { get; }

        public SuccessResource(string shortenedUrl)
        {
            ShortenedUrl = shortenedUrl;
        }
    }
}
