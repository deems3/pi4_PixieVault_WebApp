using System.ComponentModel.DataAnnotations;

namespace pi4_PixieVault_DemiBruls.Models
{
    public class Picture
    {
        public int Id { get; set; }

        [Required]
        public required string FileName { get; set; }

        [Required]
        public required string FilePath { get; set; }
    }
}
