using Microsoft.EntityFrameworkCore;
using MvcCoreEnfermosEF.Data;
using MvcCoreEnfermosEF.Models;

namespace MvcCoreEnfermosEF.Repositories
{
    public class RepositoryViewEmpleados
    {
        private HospitalBBDDContext context;

        public RepositoryViewEmpleados(HospitalBBDDContext context)
        {
            this.context = context;
        }

        public async Task<List<ViewEmpleado>> GetEmpleadosAsync()
        {
            var consulta = from datos in this.context.ViewEmpleados
                           select datos;
            return await consulta.ToListAsync();
        }
    }
}
