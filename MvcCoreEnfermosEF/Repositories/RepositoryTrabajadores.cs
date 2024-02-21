using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEnfermosEF.Data;
using MvcCoreEnfermosEF.Models;
using System.Data;

namespace MvcCoreEnfermosEF.Repositories
{
    public class RepositoryTrabajadores
    {
        private HospitalBBDDContext context;

        public RepositoryTrabajadores(HospitalBBDDContext context)
        {
            this.context = context;
        }

        public async Task<List<Trabajador>> GetTrabajadoresAsync()
        {
            var consulta = from datos in this.context.Trabajadores
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            var consulta = (from datos in this.context.Trabajadores
                            select datos.Oficio).Distinct();
            return await consulta.ToListAsync();
        }

        public async Task<TrabajadoresModel> GetTrabajadoresModelAsync(string oficio)
        {
            string sql = "SP_TRABAJADORES_OFICIO @oficio, @personas OUT, @media OUT, @suma OUT";
            SqlParameter pamOficio = new SqlParameter("@oficio", oficio);
            SqlParameter pamPersonas = new SqlParameter("@personas", -1);
            SqlParameter pamMedia = new SqlParameter("@media", -1);
            SqlParameter pamSuma = new SqlParameter("@suma", -1);
            pamPersonas.Direction = ParameterDirection.Output;
            pamMedia.Direction = ParameterDirection.Output;
            pamSuma.Direction = ParameterDirection.Output;
            var consulta = this.context.Trabajadores.FromSqlRaw(sql, pamOficio, pamPersonas, pamMedia, pamSuma);
            TrabajadoresModel model = new TrabajadoresModel();
            model.Trabajadores = await consulta.ToListAsync();
            model.Personas = int.Parse(pamPersonas.Value.ToString());
            model.MediaSalarial = int.Parse(pamMedia.Value.ToString());
            model.SumaSalarial = int.Parse(pamSuma.Value.ToString());
            return model;
        }
    }
}
