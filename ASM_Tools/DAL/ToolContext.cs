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
            Database.SetInitializer<ToolContext>(null);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<ToolToEmployee> ToolToEmployees { get; set; }
        public DbSet<Tool> Tools { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<ASM_Tools.Models.ToolViewModel> ToolViewModels { get; set; }

        //public DbSet<ASM_Tools.Models.EmployeeViewModel> EmployeeViewModels { get; set; }

        //public System.Data.Entity.DbSet<ASM_Tools.Models.ToolViewModel> ToolViewModels { get; set; }

        //public System.Data.Entity.DbSet<ASM_Tools.Models.CheckBoxToolViewModel> CheckBoxToolViewModels { get; set; }
    }
}