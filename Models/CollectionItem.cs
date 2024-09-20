using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi4_PixieVault_DemiBruls.Models
{
    public class CollectionItem
    {
        [Key]
        public int Id { get; set; }

        // Define relations
        public int CategoryId { get; set; }
        public Category? Category { get; set; } 

        [Required, MaxLength(255)]
        public required string Name { get; set; }

        [Required, MaxLength(255)]
        public required string Description { get; set; }

        [Required, MaxLength(255)]
        public required string Color { get; set; }

        [Required, MaxLength(255)]
        public required string Material { get; set; }

        [Required]
        public required decimal Price { get; set; }

        //public int? CollectionItemPictureId { get; set; }
        //public CollectionItemPicture? CollectionItemPicture { get; set; } = null!;

        public ICollection<CollectionItemCopy> CollectionItemCopies { get; } = [];
    }
}
