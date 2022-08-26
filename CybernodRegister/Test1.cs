using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;
using System.Threading;

namespace CybernodRegister
{
    public class Tests
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

            //Enter to cybernode register page 
            driver.Url = ("https://cybernod.com/auth/register");

            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles[1]);

            //Enter to Fack Mail Site & Get Mail
            driver.Url = ("https://mytemp.email/2/");
            Thread.Sleep(2000);
            IWebElement email = driver.FindElement(By.XPath("//input"));
            Thread.Sleep(1000);
            email.Click();
            Thread.Sleep(1000);

            driver.Manage().Window.Maximize();

            //Back to cybernode register page
            driver.SwitchTo().Window(driver.WindowHandles[0]);


            //Find Elements of Page
            IWebElement fullName = driver.FindElement(By.XPath("//input[@id='fullName']"));
            IWebElement email1 = driver.FindElement(By.XPath("//input[@id='email']"));
            IWebElement businessName = driver.FindElement(By.XPath("//input[@id='businessName']"));
            IWebElement website = driver.FindElement(By.XPath("//input[@id='website']"));
            IWebElement term = driver.FindElement(By.XPath("//input[@type='checkbox']"));
            IWebElement register = driver.FindElement(By.XPath("//button"));

            //enter data in Elements
            action.Click(fullName).SendKeys("Test1").Perform();
            Thread.Sleep(500);
            email1.Click();
            email1.SendKeys(Keys.Control + "v");
            Thread.Sleep(500);
            action.Click(businessName).SendKeys("Rayan").Perform();
            Thread.Sleep(500);
            website.Click();
            website.SendKeys(Keys.Control + "v");
            Thread.Sleep(500);
            action.Click(term).Perform();
            Thread.Sleep(500);
            action.SendKeys(Keys.PageDown)
                .Perform();
            Thread.Sleep(500);
            action.Click(register).Perform();
            Thread.Sleep(10000);

            //Enter to Fack Mail Site To Get Otp Code
            driver.SwitchTo().Window(driver.WindowHandles[1]);

            Thread.Sleep(59000);

            IWebElement open  = driver.FindElement(By.XPath("//span[text()=' <no-reply@cybernod.com>']"));
            action.Click(open).Perform();
            Thread.Sleep(10000);

            IWebElement iframe = driver.FindElement(By.XPath("//iframe[@id]"));
            driver.SwitchTo().Frame(iframe);

            IWebElement h3 = driver.FindElement(By.XPath("/html/body/h3"));
            string PassCode = h3.Text;
            Console.WriteLine(PassCode);

            driver.SwitchTo().DefaultContent();

            //Enter to cybernode register page to Set Otp Code & login
            driver.SwitchTo().Window(driver.WindowHandles[0]);

            IWebElement otp =  driver.FindElement(By.XPath("//input[@id='otp']"));
            action.Click(otp).SendKeys(PassCode).Perform();
            Thread.Sleep(2000);

            IWebElement login  = driver.FindElement(By.XPath("//button"));
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