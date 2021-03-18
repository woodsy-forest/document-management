using DocumentManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace DocumentManagement.Data
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options)
          : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Document Table
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<Document>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Document>().Property(e => e.Location).IsRequired();
        }
    }
}
