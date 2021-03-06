using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Support.UI;

namespace IntegrationTests
{
    public class MessageTests
    {
        private IWebDriver _driver;
        public string _homeURL;

        [SetUp]
        public void Setup()
        {
            _homeURL = "https://localhost:44316/";
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        private void LoadHome()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_homeURL);
        }

        private void AcceptCooky()
        {
            _driver.FindElement(By.Id("AcceptCookie")).Click();
        }

        private void LoginAsPatient()
        {
            //If test fails make sure user is registered
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("DemoPatient@gmail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("123");
            _driver.FindElement(By.Id("LoginUser")).Click();
        }
        private void LoginAsDoctor()
        {
            //If test fails make sure user is registered
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("DemoDoctor@gmail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("123");
            _driver.FindElement(By.Id("LoginUser")).Click();
        }


        private void SendMessage(DateTime currentDateTime)
        {
            _driver.FindElement(By.Id("Title")).SendKeys("title " + currentDateTime);
            _driver.FindElement(By.Id("Content")).SendKeys("content " + currentDateTime);
            _driver.FindElement(By.Id("SendMessage")).Click();
        }

        public void WaitForPageLoad()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(webDriver => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        [Test]
        public void LoadPatientMessage()
        {
            LoadHome();

            AcceptCooky();
            _driver.FindElement(By.Id("Login")).Click();


            LoginAsPatient();

            _driver.FindElement(By.Id("SeeChat")).Click();

            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }

        [Test]
        public void PatientSendMessage()
        {
            LoadHome();
            AcceptCooky();
            _driver.FindElement(By.Id("Login")).Click();

            LoginAsPatient();

            _driver.FindElement(By.Id("SeeChat")).Click();
            
            DateTime currentDateTime = DateTime.Now;

            SendMessage(currentDateTime);

            Assert.True(_driver.PageSource.Contains("title " + currentDateTime));
            Assert.True(_driver.PageSource.Contains("content " + currentDateTime));
            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }

        [Test]
        public void PatientSendDoctorReadMessage()
        {
            LoadHome();
            AcceptCooky();
            _driver.FindElement(By.Id("Login")).Click();

            LoginAsPatient();
            _driver.FindElement(By.Id("SeeChat")).Click();
            DateTime currentDateTime = DateTime.Now;
            SendMessage(currentDateTime);

            _driver.FindElement(By.Id("Logout")).Click();
            _driver.FindElement(By.Id("Login")).Click();

            LoginAsDoctor();
            _driver.Navigate().GoToUrl("https://localhost:44316/Message/ViewMessage/1112");

            WaitForPageLoad();

            Assert.True(_driver.PageSource.Contains("title " + currentDateTime));
            Assert.True(_driver.PageSource.Contains("content " + currentDateTime));
            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }

        [Test]
        public void LoadDoctorMessage()
        {
            LoadHome();
            AcceptCooky();
            _driver.FindElement(By.Id("Login")).Click();

            LoginAsDoctor();
            _driver.Navigate().GoToUrl("https://localhost:44316/Message/ViewMessage/1112");

            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }

        [Test]
        public void DoctorSendMessage()
        {
            LoadHome();
            AcceptCooky();
            _driver.FindElement(By.Id("Login")).Click();

            LoginAsDoctor();
            _driver.Navigate().GoToUrl("https://localhost:44316/Message/ViewMessage/1112");
            DateTime currentDateTime = DateTime.Now;

            SendMessage(currentDateTime);

            Assert.True(_driver.PageSource.Contains("title " + currentDateTime));
            Assert.True(_driver.PageSource.Contains("content " + currentDateTime));
            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }

        [Test]
        public void DoctorSendPatientReadMessage()
        {
            LoadHome();
            AcceptCooky();
            _driver.FindElement(By.Id("Login")).Click();

            LoginAsDoctor();
            _driver.Navigate().GoToUrl("https://localhost:44316/Message/ViewMessage/1112");
            DateTime currentDateTime = DateTime.Now;
            SendMessage(currentDateTime);

            _driver.FindElement(By.Id("Logout")).Click();
            _driver.FindElement(By.Id("Login")).Click();

            LoginAsPatient();
            _driver.FindElement(By.Id("SeeChat")).Click();

            WaitForPageLoad();

            Assert.True(_driver.PageSource.Contains("title " + currentDateTime));
            Assert.True(_driver.PageSource.Contains("content " + currentDateTime));
            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }


        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}