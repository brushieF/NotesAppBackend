
using NUnit.Framework;
using Moq;
using WebApplication4.Repository;
using WebApplication4.Controllers;
using System.Collections.Generic;
using WebApplication4.Models;
using System.Security.Principal;
using System.Data.Entity;
using System.Linq;

namespace UnitTest
{
    public class User
    {
        public ApplicationUser appUser;
        public string userID;

        public User()
        {
            this.appUser = new ApplicationUser();
        }
    }

    [TestFixture]
    public class NoteRepositoryTest
    {


        private NoteRepository noteRepository;

        private List<User> users = new List<User>();

        private List<NoteModel> data;


        Mock<Db> dbMock;
        Mock<DbSet<NoteModel>> dbSetMock;


        public delegate void MockChange();

        [SetUp]
        public void SetUp()
        {
            SetUser();
            SetDb();




            
            //_noteRepository = new Mock<INoteRepository>();

            //_noteRepository.Setup(x => x.GetNotes(It.IsAny<string>())).Returns(GetNotes());

        }

        private void SetUser()
        {
            User user1 = new User();
            user1.userID = "first";
            user1.appUser.Id = user1.userID;
            users.Add(user1);

            User user2 = new User();
            user2.userID = "second";
            user2.appUser.Id = user2.userID;
            users.Add(user2);

            User user3 = new User();
            user3.userID = "third";
            user3.appUser.Id = user3.userID;
            users.Add(user3);

            User user4 = new User();
            user4.userID = "fourth";
            user4.appUser.Id = user4.userID;
            users.Add(user4);
      
           
        }



        private void SetDb()
        {
            dbMock = new Mock<Db>();
            data = GetNotes();
            var dataIQueryable = data.AsQueryable();

            dbSetMock = new Mock<DbSet<NoteModel>>();
            dbSetMock.As<IQueryable<NoteModel>>().Setup(m => m.Provider).Returns(dataIQueryable.Provider);
            dbSetMock.As<IQueryable<NoteModel>>().Setup(m => m.Expression).Returns(dataIQueryable.Expression);
            dbSetMock.As<IQueryable<NoteModel>>().Setup(m => m.ElementType).Returns(dataIQueryable.ElementType);
            dbSetMock.As<IQueryable<NoteModel>>().Setup(m => m.GetEnumerator()).Returns(dataIQueryable.GetEnumerator());


            var userSetMock = new Mock<DbSet<WebApplication4.Models.ApplicationUser>>();


            var appUsers = new List<WebApplication4.Models.ApplicationUser>()
            {
                new WebApplication4.Models.ApplicationUser(){Id = "first"}
            }.AsQueryable();

                        
            

            userSetMock.As<IQueryable<WebApplication4.Models.ApplicationUser>>().Setup(m => m.Provider).Returns(appUsers.Provider);
            userSetMock.As<IQueryable<WebApplication4.Models.ApplicationUser>>().Setup(m => m.Expression).Returns(appUsers.Expression);
            userSetMock.As<IQueryable<WebApplication4.Models.ApplicationUser>>().Setup(m => m.ElementType).Returns(appUsers.ElementType);
            userSetMock.As<IQueryable<WebApplication4.Models.ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(appUsers.GetEnumerator());



            dbMock.Setup(e => e.Notes).Returns(dbSetMock.Object);
            dbMock.Setup(e => e.Users).Returns(userSetMock.Object);



            noteRepository = new NoteRepository(dbMock.Object);

        }


        public void GetNotes_ShouldRetrunAllNotesForFirstUser()
        {
            var identity = new GenericIdentity("fdsf");


            var notes = GetNotes();
            var controller = new NotesController(noteRepository);
            controller.User = new GenericPrincipal(identity, null);

            var d = identity;
            //var result = controller.Get2();
            var result = controller.Get();
            int count = 0;

            
            foreach (var item in result)
            {
                count++;
            }
            

            Assert.AreEqual(notes.Count, count);

        }
        [Test]
        public void GetNotes_ShouldReturnNotesForFirstUser()
        {
            var result = noteRepository.GetNotes("first");

            var firstUserNotesCount = data.Where(x => x.UserID.Id == "first").Count();

            Assert.AreEqual(result.Count(),firstUserNotesCount);
        }

        private void ChangeMock()
        {
            dbMock.Setup(e => e.Notes.Remove(It.IsAny<NoteModel>())).Callback<NoteModel>((entity) => data.Remove(entity));
        }


        [Test]
        public void DeleteNote_ShouldDeleteOneNote()
        {
            var countBefore = dbMock.Object.Notes.Count();
            MockChange d = ChangeMock;
            
            dbMock.Setup(e => e.Notes).Returns(dbSetMock.Object).Callback(d);


            noteRepository.DeleteNote(0, "first");

            dbMock.Object.SaveChanges();

            dbMock.Setup(e => e.Notes).Returns(dbSetMock.Object);


            Assert.That(countBefore == dbMock.Object.Notes.Count() + 1);
        }

        [Test]
        public void GetNote_ShouldReturnFirstNoteForFirstUser()
        {
            var result = noteRepository.GetNote(0, "first");


            Assert.That(data[0] == result);
        }

        [Test]
        public void InsertNote_ShouldInsertNote()
        {
            var countBefore = dbMock.Object.Notes.Count();
            

            NoteModel noteToInsert = new NoteModel() { Content = "lbc", R = 5, G = 10, B = 20, UserID = users[0].appUser };
            dbMock.Setup(e => e.Notes.Add(It.IsAny<NoteModel>())).Callback<NoteModel>((enitiy) => data.Add(enitiy));

            noteRepository.InsertNote(noteToInsert,"first");
            dbMock.Object.SaveChanges();

            dbMock.Setup(e => e.Notes).Returns(dbSetMock.Object);

            var countAfter = dbMock.Object.Notes.Count();

            Assert.That(countAfter == countBefore + 1);

        }

        [Test]
        public void UpdateNote_ShouldUpdateNote()
        {
            var noteBeforeEdit = data[0];

            NoteModel noteEdit = new NoteModel() { Content = "bmxJestFajny", R = 5, G = 10, B = 20, UserID = users[0].appUser };

            noteRepository.UpdateNote(noteEdit, "first");

            Assert.That(noteBeforeEdit.Content == data[0].Content);
        }



        private List<NoteModel> GetNotes()
        {
            var testNotes = new List<NoteModel>();

            testNotes.Add(new NoteModel { Content = "abc", R = 5, G = 10, B = 20,UserID = users[0].appUser, NoteID = 0});
            testNotes.Add(new NoteModel { Content = "def", R = 5, G = 10, B = 20, UserID = users[1].appUser, NoteID = 1 });
            testNotes.Add(new NoteModel { Content = "ghi", R = 5, G = 10, B = 20, UserID = users[2].appUser, NoteID = 2 });
            testNotes.Add(new NoteModel { Content = "jkl", R = 5, G = 10, B = 20, UserID = users[3].appUser, NoteID = 3});
            testNotes.Add(new NoteModel { Content = "mno", R = 5, G = 10, B = 20, UserID = users[0].appUser, NoteID = 4 });

            return testNotes;
        }
   
    }
}
