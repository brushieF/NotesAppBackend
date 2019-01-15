using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class NoteModel
    {
        [Key]
        public int NoteID { get; set; }
        public string Contenet { get; set; }


        public ApplicationUser UserID { get; set; }
    }
}