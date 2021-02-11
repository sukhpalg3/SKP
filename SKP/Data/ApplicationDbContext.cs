using System;
using System.Collections.Generic;
using System.Text;
using SKP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SKP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<BooksDetail> BooksDetails { get; set; }
        public DbSet<SKP.Models.NotesDetail> NotesDetail { get; set; }
        public DbSet<SKP.Models.VideoLecDetail> VideoLecDetail { get; set; }


    }
}
