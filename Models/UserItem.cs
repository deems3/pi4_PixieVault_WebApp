using pi4_PixieVault_DemiBruls.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi4_PixieVault_DemiBruls.Models
{
    public class UserItem
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int CollectionItemId { get; set; }
        public CollectionItem CollectionItem { get; set; } = null!;

        public int? CollectionItemPictureId { get; set; }
        public Picture? CollectionItemPicture { get; set; } = null!;

        public bool IsForSale { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string State { get; set; }

        [Column(TypeName="decimal(18,2)")]
        public decimal ForSalePrice { get; set; }
    }
}
