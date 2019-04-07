using System.ComponentModel.DataAnnotations;

namespace Codeping.GoLink.Core
{
    public class Link
    {
        public Link(string shortId, string longUrl)
        {
            this.Id = shortId;
            this.LongUrl = longUrl;
        }

        [Key]
        public string Id { get; set; }
        public int Total { get; set; }
        public string LongUrl { get; set; }
    }
}
