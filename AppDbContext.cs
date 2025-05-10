using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NET_Core_Web_API_HiringTest
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItems> TaskItems { get; set; }
        public DbSet<TaskComments> TaskComments { get; set; }

       
    }

}
