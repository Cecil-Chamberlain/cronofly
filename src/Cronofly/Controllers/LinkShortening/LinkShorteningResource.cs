using System.ComponentModel.DataAnnotations;

namespace Cronofly.Controllers.LinkShortening
{
    public class LinkShorteningResource
    {
        [Required]
        [Url(ErrorMessage = "Must be a valid url")]
        public string UrlToShorten { get; set; }
    }
}
