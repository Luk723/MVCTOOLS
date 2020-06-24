using ASM_Tools.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ASM_Tools.DAL
{
    public class ToolContext : DbContext
    {
        public ToolContext() : base("ToolContext")
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<ToolToEmployee> ToolToEmployees { get; set; }
        public DbSet<Tool> Tools { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}