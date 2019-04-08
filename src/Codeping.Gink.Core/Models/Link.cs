using System.ComponentModel.DataAnnotations;

namespace Codeping.Gink.Core
{
    public class Link
    {
        [Key]
        public string Id { get; set; }
        public int Total { get; set; }
        public string LongUrl { get; set; }
    }
}
