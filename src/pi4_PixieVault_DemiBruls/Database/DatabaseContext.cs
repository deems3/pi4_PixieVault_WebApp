using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pi4_PixieVault_DemiBruls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi4_PixieVault_DemiBruls.Database
{
    public class DatabaseContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CollectionItem> CollectionItems { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=. ;Initial Catalog=pi4_pixie_vault_demi_bruls;Integrated Security=True; TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connection);
        }
    }
}
