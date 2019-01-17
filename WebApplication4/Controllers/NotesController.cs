
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication4.Models;
using WebApplication4.Repository;
using System.Web.Http.ExceptionHandling;

using Microsoft.AspNet.Identity;
using System;

namespace WebApplication4.Controllers
{
    public class NotesController : ApiController
    {
        private INoteRepository _noteRepository;
        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
       
        public IEnumerable<NoteModel> Get()
        {
            return _noteRepository.GetNotes(User.Identity.GetUserId()).Select(x=>x);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]NoteModel value)
        {
            _noteRepository.InsertNote(value, User.Identity.GetUserId());
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]NoteModel note)
        {
            _noteRepository.InsertNote(note,User.Identity.GetUserId());
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _noteRepository.DeleteNote(id);
        }
    }
}
