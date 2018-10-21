using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIAM.Database
{
    [Table("REPAS")]
    public class Repas
    {
        [Key]
        public int Id { get; set; }
        [Column("Plat", TypeName = "VARCHAR(MAX)")]
        public string Plat { get; set; }
        [Column("Date", TypeName = "Date")]
        public System.DateTime Date { get; set; }
    }
}