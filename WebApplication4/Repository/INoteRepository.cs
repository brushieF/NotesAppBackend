﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace WebApplication4.Repository
{
	public interface INoteRepository : IDisposable
	{
        IEnumerable<NoteModel> GetNotes(string userId);
        NoteModel GetNote(int noteID, string userID);
        void InsertNote(NoteModel note, string userID);
        void DeleteNote(int noteID, string userID);
        void UpdateNote(NoteModel note, string userID);
        void Save();
	}
}