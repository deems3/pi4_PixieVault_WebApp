﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi4_PixieVault_DemiBruls.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required, MaxLength(255)]
        public required string Name { get; set; }

        // ICollection indicates that a User can have more than one CollectionItem
        public ICollection<CollectionItemCopy> CollectionItemCopies { get; } = [];
    }
}
