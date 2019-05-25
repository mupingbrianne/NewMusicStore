using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewMusicStore.Models;

namespace NewMusicStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Music> Music { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Singer> Singer { get; set; }
    }
}
