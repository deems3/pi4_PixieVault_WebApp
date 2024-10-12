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

        public string? UserId { get; set; }
        public User? User { get; set; }

        public int CollectionItemId { get; set; }
        public required CollectionItem? CollectionItem { get; set; }

        public int? PictureId { get; set; }
        public Picture? Picture { get; set; }

        public bool IsForSale { get; set; }
        public DateTime ReleaseDate { get; set; }
        public required string State { get; set; }

        [Column(TypeName="decimal(18,2)")]
        public decimal ForSalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
    }
}
