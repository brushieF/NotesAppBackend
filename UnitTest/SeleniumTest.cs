using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace UnitTest
{
    static class ValidUser
    {
        public static string Email { get { return "abc@gmail.com"; } }
        public static string Password { get { return "localhost4200"; } }
    }

    static class NonValidUser
    {
        public static string Email { get { return "abcads@gmail.com"; } }
        public static string Password { get { return "locaasdlhost4200"; } }
    }


    [TestFixture]
    class SeleniumTest
    {
        private IWebDriver _driver;
        private string Url = "localhost:4200";

        [SetUp]
        public void SetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.Proxy = null;

            _driver = new ChromeDriver(chromeOptions);
            

            _driver.Url = Url;
            
        }

        [Test]
        public void SuccesfullLogin_ShouldShowLogoutButton()
        {      
            TryToLogin(ValidUser.Email, ValidUser.Password);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-top-bar/div/div[2]/div[2]/i")));

            Assert.That(_driver.FindElement(By.XPath("/html/body/app-root/app-top-bar/div/div[2]/div[2]/i")).Displayed);
        }
        [Test]
        public void UserAddsNote_ShouldSaveNote()
        {
            Random rand = new Random();

           
            string sampleContent = rand.Next(0,10000).ToString();

            TryToLogin(ValidUser.Email, ValidUser.Password);
            AddNote(sampleContent);
 
            System.Threading.Thread.Sleep(2000);
            var noteContents = _driver.FindElements(By.CssSelector("div.content"));


            Func<string, bool> func = element => element == sampleContent;

            bool exist = false;
            foreach (var item in noteContents)
            {
                exist = func(item.Text);
            }
            Assert.That(exist);
        }
        [Test]
        public void UserDeleteNotes_ShouldDeleteAllNotes()
        {
            TryToLogin(ValidUser.Email, ValidUser.Password);
            for(int i = 0; i < 10; i++)
            {
                AddNote("abc");
            }

            System.Threading.Thread.Sleep(2000);
            var deleteButtons = _driver.FindElements(By.ClassName("floatRight"));

            foreach (var deleteButton in deleteButtons)
            {
                deleteButton.Click();
            }

            var element = _driver.FindElements(By.CssSelector("div.noteHeader"));
            

            Assert.That(element.Count == 0);
        }

        private void AddNote(string content)
        {

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-top-bar/div/div[2]/div[2]/i")));

            var noteAddButton = _driver.FindElement(By.XPath("/html/body/app-root/app-top-bar/div/div[2]/div[1]"));
            noteAddButton.Click();

            var noteTextInput = _driver.FindElement(By.XPath("/html/body/app-root/app-note/app-edit-menu/div[2]/textarea"));
            noteTextInput.SendKeys(content);

            var noteSaveButton = _driver.FindElement(By.XPath("/html/body/app-root/app-note/app-edit-menu/div[2]/div/i[2]"));
            noteSaveButton.Click();
        }


        [Test]
        public void LoginUnsuccesfull_ShouldShowError()
        {
            TryToLogin(NonValidUser.Email, NonValidUser.Password);

       

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-main-page/div[2]/div[1]/app-login-form/app-error-shower/div")));

            Assert.That(_driver.FindElement(By.XPath("/html/body/app-root/app-main-page/div[2]/div[1]/app-login-form/app-error-shower/div")).Displayed);
        }






        private void TryToLogin(string email, string password)
        {
            var loginInput = _driver.FindElement(By.XPath("/html/body/app-root/app-main-page/div[2]/div[1]/app-login-form/div[1]/form/div[1]/input"));
            var passwordInput = _driver.FindElement(By.XPath("/html/body/app-root/app-main-page/div[2]/div[1]/app-login-form/div[1]/form/div[2]/input"));

            loginInput.SendKeys(email);
            passwordInput.SendKeys(password);

            var loginButton = _driver.FindElement(By.CssSelector("#login"));


            loginButton.Click();
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null) _driver.Quit();
        }
        

    }
}
