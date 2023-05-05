using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBanK.Models;

namespace MyBanK.Data
{
    public class MyBanKContext : DbContext
    {
        public MyBanKContext (DbContextOptions<MyBanKContext> options)
            : base(options)
        {
        }

        public DbSet<MyBanK.Models.ContaCorrente> ContaCorrente { get; set; } = default!;
        public DbSet<MyBanK.Models.Movimento> Movimentos { get; set; } = default!;
    }
}
