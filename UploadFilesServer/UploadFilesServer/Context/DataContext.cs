using System;
using Microsoft.EntityFrameworkCore;
using UploadFilesServer.Models;

namespace UploadFilesServer.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Image>? Images { get; set; }
    }
}

