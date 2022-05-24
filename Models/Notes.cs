using System.ComponentModel.DataAnnotations;

namespace PSD_Project
{
    public class Notes
    {
        [Required]
        public int ID { get; set; } = 1;
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public string Creator { get; set; } = string.Empty;
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}