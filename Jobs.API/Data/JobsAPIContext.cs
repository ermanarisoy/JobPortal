using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Jobs.API.Models;

namespace Jobs.API.Data
{
    public class JobsAPIContext : DbContext
    {
        public JobsAPIContext (DbContextOptions<JobsAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Jobs.API.Models.Job>? Job { get; set; }

        public DbSet<Jobs.API.Models.UserModel>? User { get; set; }
    }
}
