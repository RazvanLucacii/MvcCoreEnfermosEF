using Microsoft.EntityFrameworkCore;
using MvcCoreEnfermosEF.Models;

namespace MvcCoreEnfermosEF.Data
{
    public class HospitalBBDDContext : DbContext
    {
        public HospitalBBDDContext(DbContextOptions<HospitalBBDDContext> options):base(options)
        { }

        public DbSet<Enfermo> Enfermos { get; set; }

        public DbSet<Doctor> Doctores { get; set; }

        public DbSet<ViewEmpleado> ViewEmpleados { get; set; }

        public DbSet<Trabajador> Trabajadores { get; set; }
    }
}
