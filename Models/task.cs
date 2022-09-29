using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace ET_ASP.Models
{
    //[Table("task")]
    public class task
    {
        //[Key]
        public Guid taskID { get; set; }

        //[ForeignKey("CategoriaId")]
        public Guid categoryID { get; set; }

        //[Required]
        //[MaxLength(200)]
        public string tittle { get; set; }
        public string description { get; set; }
        public Prioridad prioTask { get; set; }
        public DateTime createAt { get; set; }

        public virtual category category { get; set; }

        //[NotMapped]
        public string resume { get; set; } 
    }

    public enum Prioridad
    {
        Baja,
        Media,
        Alta
    }
}
