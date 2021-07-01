using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shared;

namespace api.Context
{
    public class BlazorFileContext : DbContext
    {
        public DbSet<BlazorFile> BlazorFiles { get; set; }
        public DbSet<BlazorFileType> BlazorFileTypes { get; set; }
        public BlazorFileContext(DbContextOptions<BlazorFileContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BlazorFile>(p=>
            { 
                p.ToTable("File");
                p.HasKey(p=> p.BlazorFileId);
                p.Property(p=> p.Name).IsRequired();
                p.Property(p=> p.Name).HasMaxLength(250);
                p.Property(p=> p.DateCreated).HasDefaultValue(DateTime.Now);
                p.HasOne(p=> p.BlazorFileType).WithMany(p=> p.BlazorFiles);
            });

            List<BlazorFileType> blazorFileTypes = new List<BlazorFileType>();
            blazorFileTypes.Add(new BlazorFileType()
            {
                Name = "File",
                Description = "General File"
            });

            blazorFileTypes.Add(new BlazorFileType()
            {
                Name = "Document",
                Description = "General document"
            });

            blazorFileTypes.Add(new BlazorFileType()
            {
                Name = "Office Document",
                Description = "document word, powerpoint, excel, etc..."
            });

            builder.Entity<BlazorFileType>(p=>
            { 
                p.ToTable("FileType");
                p.HasKey(p=> p.BlazorFileTypeId);
                p.Property(p=> p.Name).IsRequired();
                p.Property(p=> p.Name).HasMaxLength(250);
            });

            base.OnModelCreating(builder);
            
        }
    }
}