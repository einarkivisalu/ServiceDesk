using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiceDesk.Models
{
    public class ServiceDeskContext : DbContext
    {
        public ServiceDeskContext (DbContextOptions<ServiceDeskContext> options)
            : base(options)
        {
        }

        public DbSet<ServiceDesk.Models.Ticket> Ticket { get; set; }
    }
}
