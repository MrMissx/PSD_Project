using System.ComponentModel.DataAnnotations;

namespace PSD_Project
{
    public class NotesDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public string Creator { get; set; } = string.Empty;
    }
}