using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEnfermosEF.Data;
using MvcCoreEnfermosEF.Models;
using System.Data;
using System.Data.Common;

#region PROCEDIMIENTOS ALMACENADOS

//create procedure SP_TODOS_DOCTORES
//as
//	select * from DOCTOR
//go

//alter procedure SP_DOCTORES_ESPECIALIDAD
//as
//	select DISTINCT(ESPECIALIDAD) FROM DOCTOR
//go

//create procedure SP_INCREMENTO_SALARIOS
//(@ESPECIALIDAD NVARCHAR(50), @INCREMENTO int)
//as
//	update DOCTOR SET SALARIO = SALARIO + @INCREMENTO
//	where ESPECIALIDAD = @ESPECIALIDAD
//go

#endregion

namespace MvcCoreEnfermosEF.Repositories
{
    public class RepositoryDoctores
    {
        private HospitalBBDDContext context;

        public RepositoryDoctores(HospitalBBDDContext context)
        {
            this.context = context;
        }

        public List<Doctor> GetDoctores()
        {
            using(DbCommand command = this.context.Database.GetDbConnection().CreateCommand()) 
            {
                string sql = "SP_TODOS_DOCTORES";
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                DbDataReader reader = command.ExecuteReader();
                List<Doctor> doctores = new List<Doctor>();
                while (reader.Read())
                {
                    Doctor doctor = new Doctor
                    {
                        IdHospital = int.Parse(reader["HOSPITAL_COD"].ToString()),
                        IdDoctor = int.Parse(reader["DOCTOR_NO"].ToString()),
                        Apellido = reader["APELLIDO"].ToString(),
                        Especialidad = reader["ESPECIALIDAD"].ToString(),
                        Salario = int.Parse(reader["SALARIO"].ToString())
                    };
                    doctores.Add(doctor);
                }
                reader.Close();
                command.Connection.Close();
                return doctores;
            }
        }

        public List<string> GetDoctoresEspecialidad()
        {
            using (DbCommand command = this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_DOCTORES_ESPECIALIDAD";
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                DbDataReader reader = command.ExecuteReader();
                List<string> especialidades = new List<string>();
                while (reader.Read())
                {
                    especialidades.Add(reader["ESPECIALIDAD"].ToString());
                }
                reader.Close();
                command.Connection.Close();
                return especialidades;
            }
        }

        public void IncrementoSalarios(string especialidad, int incremento)
        {
            string sql = "SP_INCREMENTO_SALARIOS @ESPECIALIDAD, @INCREMENTO";
            SqlParameter pamEspecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
            SqlParameter pamIncremento = new SqlParameter("@INCREMENTO", incremento);
            this.context.Database.ExecuteSqlRaw(sql, pamEspecialidad, pamIncremento);
        }
    }
}
