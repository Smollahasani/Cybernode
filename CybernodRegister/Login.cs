using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;
using System.Threading;

namespace CybernodRegister
{
    public class login
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test1()
        {
            Actions action = new Actions(driver);

            //Enter to cybernode Login page & Find Elements
            driver.Url = ("https://cybernod.com/auth/login");

            driver.Manage().Window.Maximize();

            IWebElement workmail  = driver.FindElement(By.XPath("//input"));
            IWebElement loginButton   = driver.FindElement(By.XPath("//button"));

            action.Click(workmail).SendKeys("alizarandi@chmail.ir").Perform();
            Thread.Sleep(500);
            action.Click(loginButton).Perform();
            Thread.Sleep(6000);


            //Open new Tab 
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles[1]);

            //Enter to  Mail Site & login
            driver.Url = ("https://accounts.chmail.ir/login");
            Thread.Sleep(5000);
            IWebElement password  = driver.FindElement(By.XPath("//input[@id='password']"));
            IWebElement username  = driver.FindElement(By.XPath("//input[@id='username']"));
            IWebElement Button  = driver.FindElement(By.XPath("//button[@type='submit']"));

            Thread.Sleep(1000);
            action.Click(username).SendKeys("alizarandi@chmail.ir").Perform();
            Thread.Sleep(1000);
            action.Click(password).SendKeys("96312020Ss").Perform();
            Thread.Sleep(1000);
            Button.Click();
            Thread.Sleep(2000);
            driver.Url = ("https://classic.chmail.ir/");
            Thread.Sleep(4000);

            //get Otp Code
            IWebElement r0  = driver.FindElement(By.XPath("//div[@id='R0']"));
            action.Click(r0).Perform();
            Thread.Sleep(3000);

            IWebElement iframe = driver.FindElement(By.XPath("//iframe"));
            driver.SwitchTo().Frame(iframe);

            IWebElement h3 = driver.FindElement(By.XPath("/html/body/div/div/h3"));
            string PassCode = h3.Text;

            driver.SwitchTo().DefaultContent();

            //Back to Cybernode login page to set otp code & login
            driver.SwitchTo().Window(driver.WindowHandles[0]);

            IWebElement otp = driver.FindElement(By.XPath("//input[@id='otp']"));
            action.Click(otp).SendKeys(PassCode).Perform();
            Thread.Sleep(2000);

            IWebElement login = driver.FindElement(By.XPath("//button"));
            action.Click(login).Perform();
            Thread.Sleep(30000);

            //Assert Dashboard Title
            IWebElement welcome = driver.FindElement(By.XPath("//span[text()='Welcome to Dashboard']"));
            string x = welcome.Text;
            Assert.AreEqual(x, "Welcome to Dashboard");





            Assert.Pass();
        }
    }
}