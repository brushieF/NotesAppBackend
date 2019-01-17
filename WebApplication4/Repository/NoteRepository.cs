using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;


namespace WebApplication4.Repository
{
    public class NoteRepository : INoteRepository, IDisposable
    {
        public ApplicationDbContext _dbContext;
    

        public NoteRepository()
        {
        }


        public NoteRepository(ApplicationDbContext context)
        {
            this._dbContext = context;
        }

        public void DeleteNote(int noteID)
        {
            NoteModel noteToRemove = _dbContext.Notes.SingleOrDefault(x => x.NoteID == noteID);
            if (noteToRemove == null) return;
            _dbContext.Notes.Remove(noteToRemove);
            Save();
        }

        public void Dispose()
        {
           
        }

        public NoteModel GetNote(int noteID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoteModel> GetNotes(string userID)
        {
            IEnumerable<NoteModel> filteredNotes = _dbContext.Notes.Where(x => x.UserID.Id == userID);
         
            return filteredNotes.Select(x => x);
        }

        public void InsertNote(NoteModel note, string userID)
        {

            note.UserID = _dbContext.Users.FirstOrDefault(x => x.Id == userID);
            _dbContext.Notes.Add(note);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateNote(NoteModel note)
        {
            throw new NotImplementedException();
        }
    }
}