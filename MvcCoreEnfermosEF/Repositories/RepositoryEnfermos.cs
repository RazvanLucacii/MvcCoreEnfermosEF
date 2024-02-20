using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEnfermosEF.Data;
using MvcCoreEnfermosEF.Models;
using System.Data;
using System.Data.Common;

#region PROCEDIMIENTOS ALMACENADOS

//create procedure SP_TODOS_ENFERMOS
//as
//	select * from ENFERMO
//go

//create procedure SP_FIND_ENFERMO
//(@inscripcion int)
//as
//	select * from ENFERMO
//	where INSCRIPCION = @inscripcion
//go

//create procedure SP_DELETE_ENFERMO
//(@inscripcion int)
//as
//	delete from ENFERMO
//	where INSCRIPCION = @inscripcion
//go

//create procedure SP_INSERT_ENFERMO
//(@APELLIDO NVARCHAR(50), @DIRECCION NVARCHAR(50), @FECHA_NAC DATETIME, @S NVARCHAR(50), @NSS NVARCHAR(50))
//as
//	declare @NEXTINSCRIPCION int
//	select @NEXTINSCRIPCION = MAX(INSCRIPCION) + 1 from ENFERMO
//	insert into ENFERMO values(@NEXTINSCRIPCION, @APELLIDO, @DIRECCION, @FECHA_NAC, @S, @NSS)
//go

#endregion

namespace MvcCoreEnfermosEF.Repositories
{
    public class RepositoryEnfermos
    {
        private HospitalBBDDContext context;

        public RepositoryEnfermos(HospitalBBDDContext context) 
        {
            this.context = context;
        }

        public List<Enfermo> GetEnfermos()
        {
            using (DbCommand command = this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_TODOS_ENFERMOS";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sql;
                command.Connection.Open();
                DbDataReader reader = command.ExecuteReader();
                List<Enfermo> enfermos = new List<Enfermo>();
                while (reader.Read())
                {
                    Enfermo enfermo = new Enfermo
                    {
                        Inscripcion = int.Parse(reader["INSCRIPCION"].ToString()),
                        Apellido = reader["APELLIDO"].ToString(),
                        Direccion = reader["DIRECCION"].ToString(),
                        Fecha = DateTime.Parse(reader["FECHA_NAC"].ToString()),
                        S = reader["S"].ToString(),
                        NSS = reader["NSS"].ToString()
                    };
                    enfermos.Add(enfermo);
                }
                reader.Close();
                command.Connection.Close();
                return enfermos;
            }
        }

        public Enfermo FindEnfermo(int inscripcion)
        {
            string sql = "SP_FIND_ENFERMO @inscripcion";
            SqlParameter pamInscripcion = new SqlParameter("@inscripcion", inscripcion);
            var consulta = this.context.Enfermos.FromSqlRaw(sql, pamInscripcion);
            Enfermo enfermo = consulta.AsEnumerable().FirstOrDefault();
            return enfermo;
        }

        public void DeleteEnfermo(int inscripcion)
        {
            string sql = "SP_DELETE_ENFERMO @inscripcion";
            SqlParameter pamInscripcion = new SqlParameter("@inscripcion", inscripcion);
            this.context.Database.ExecuteSqlRaw(sql, pamInscripcion);
        }

        public void InsertEnfermo(string apellido, string direccion, DateTime fecha, string s, string nss)
        {
            string sql = "SP_INSERT_ENFERMO @apellido, @direccion, @fecha, @s, @nss";
            SqlParameter pamApellido = new SqlParameter("@apellido", apellido);
            SqlParameter pamDireccion = new SqlParameter("@direccion", direccion);
            SqlParameter pamFecha = new SqlParameter("@fecha", fecha);
            SqlParameter pamS = new SqlParameter("@s", s);
            SqlParameter pamNSS = new SqlParameter("@nss", nss);
            this.context.Database.ExecuteSqlRaw(sql, pamApellido, pamDireccion, pamFecha, pamS, pamNSS);
        }
    }
}
