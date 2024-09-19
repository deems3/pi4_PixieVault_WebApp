using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi4_PixieVault_DemiBruls.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public required string Name { get; set; }

        public List<CollectionItem> CollectionItems { get; } = [];
    }
}
