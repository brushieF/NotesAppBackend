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
            var email = "abc@gmail.com";
            var password = "localhost4200";

            TryToLogin(email, password);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-top-bar/div/div[2]/div[2]/i")));

            Assert.That(_driver.FindElement(By.XPath("/html/body/app-root/app-top-bar/div/div[2]/div[2]/i")).Displayed);
        }

        [Test]
        public void LoginUnsuccesfull_ShouldShowError()
        {
            var email = "ab32314c@gmail.com";
            var password = "localhost4200";

            TryToLogin(email, password);

       

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
