using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBitly.Models;
using Microsoft.EntityFrameworkCore.Migrations;


namespace MyBitly.Controllers
{
    public class UrlContext : DbContext
    {
        public DbSet<UrlData> UrlDatas { get; set; }

        public UrlContext(DbContextOptions<UrlContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
