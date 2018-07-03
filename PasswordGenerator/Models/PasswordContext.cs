using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordGenerator.Models
{
    public class PasswordContext : DbContext
    {
        public PasswordContext(DbContextOptions<PasswordContext> options)
            : base(options)
        {

        }

        public DbSet<PasswordRequirement> PasswordRequirements { get; set; }
    }
}
