using System;
using ConvertionToWordsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConvertionToWordsApp.Data
{
	public class AppDbContext : DbContext
	{

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Input> inputs { get; set; }
    }
}

