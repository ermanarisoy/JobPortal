using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobPortal.API.Model;

namespace JobPortal.API.Data
{
    public class JobPortalAPIContext : DbContext
    {
        public JobPortalAPIContext (DbContextOptions<JobPortalAPIContext> options)
            : base(options)
        {
        }

        public DbSet<JobPortal.API.Model.Job>? Job { get; set; }
    }
}
