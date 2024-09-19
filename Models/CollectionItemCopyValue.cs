using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi4_PixieVault_DemiBruls.Models
{
    public class CollectionItemCopyValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required DateTime DateTime { get; set; }

        public int CollectionItemCopyId { get; set; }
        public CollectionItemCopy CollectionItemCopy { get; set; } = null!;
    }
}
