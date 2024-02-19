using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreEnfermosEF.Models
{
    [Table("ENFERMO")]
    public class Enfermo
    {
        [Key]
        [Column("INSCRIPCION")]
        public int Inscripcion { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }

        [Column("DIRECCION")]
        public string Direccion { get; set; }

        [Column("FECHA_NAC")]
        public DateTime Fecha {  get; set; }

        [Column("S")]
        public string S {  get; set; }

        [Column("NSS")]
        public string NSS { get; set; }

    }
}
