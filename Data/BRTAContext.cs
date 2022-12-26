using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VIMS.DataModel;

namespace VIMS.Data
{
    public class BRTAContext : DbContext
    {
        public BRTAContext (DbContextOptions<BRTAContext> options)
            : base(options)
        {
        }

        public DbSet<VIMS.DataModel.User> User { get; set; } = default!;

        public DbSet<VIMS.DataModel.Admin> BRTA { get; set; } = default!;
        public DbSet<VIMS.DataModel.TrafficPolice> Trafficpolice { get; set; } = default!;
        public DbSet<VIMS.DataModel.RulesViolationForm> Rulesviolations { get; set; } = default!;
        public DbSet<VIMS.DataModel.AccidentCase> AccidentCases { get; set; } = default!;
        public DbSet<VIMS.DataModel.Renewapply> Renewapplies{ get; set; } = default!;
        public DbSet<VIMS.DataModel.Notice> Notices { get; set; } = default!;
        public DbSet<VIMS.DataModel.Vehicle> Vehicles { get; set; } = default!;
        public DbSet<VIMS.DataModel.Service> Services { get; set; } = default!;
    }
}
