using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace WebApplication4.Models
{
    public class NoteModel
    {
        [Key]
        public int NoteID { get; set; }
        public string Content { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }


        public ApplicationUser UserID { get; set; }
    }
}