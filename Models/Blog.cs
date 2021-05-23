using System.ComponentModel.DataAnnotations;

namespace blogger_cs.Models
{
    public class Blog
    {
        public int Id { get; set; }
        

        // Required tag goes above variable name
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        
        public string Body { get; set; }
        public string ImgUrl { get; set; }
        public bool published { get; set; }
        public string CreatorId { get; set; }
        public Account Creator { get; set; }
    }
}