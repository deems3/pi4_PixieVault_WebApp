using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pi4_PixieVault_DemiBruls.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public required string FileName { get; set; }

        public required string FilePath { get; set; }
    }
}
