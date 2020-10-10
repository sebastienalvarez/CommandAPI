using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Data
{
    public class CommandContext : DbContext
    {
        public DbSet<Command> Commands { get; set; }

        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {

        }
    }
}
