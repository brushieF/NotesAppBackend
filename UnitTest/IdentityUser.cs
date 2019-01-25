using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Models;
using System.Web.Http.Controllers;
using System.Security.Principal;
using System.Data.Entity;



namespace UnitTest
{

 
    public  interface Identity
    {
         string GetUserId();
    }
    public class ApplcationUser
    {
        public Identity Identity { get; set; }
       
    }
    public interface IdentityUser
    {
        ApplcationUser User { get; set; }
    }
  
    
    
  public class Db : ApplicationDbContext
    {
        public override DbSet<NoteModel> Notes { get; set; }

        private List<NoteModel> notes = new List<NoteModel>();

        public Db()
        {

        }


   


     

      
        public List<NoteModel> GetNotes2()
        {
            var testNotes = new List<NoteModel>();

            testNotes.Add(new NoteModel { Content = "abc", R = 5, G = 10, B = 20 });
            testNotes.Add(new NoteModel { Content = "def", R = 5, G = 10, B = 20 });
            testNotes.Add(new NoteModel { Content = "ghi", R = 5, G = 10, B = 20 });
            testNotes.Add(new NoteModel { Content = "jkl", R = 5, G = 10, B = 20 });



            return testNotes;

        }
    }


}
