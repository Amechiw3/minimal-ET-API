using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ET_ASP.Models
{
    //[Table("category")]
    public class category 
    {
        //[Key]
        public Guid categoryID { get; set; }

        //[Required]
        //[MaxLength(150)]
        public string name { get; set; }

        public string description { get; set; }

        [JsonIgnore]
        public virtual ICollection<task> tasks { get; set; }    
    }
}
