
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;

namespace SageProject
{
    [TestFixture]
    public class TestClass
    {
        IWebDriver driver = null;

        [SetUp]
        public void SetUp()
        {
            try
            {
                driver = new ChromeDriver();
                driver.Url = "https://app.es.sageone.com/login";
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                Console.WriteLine("Open Browser");
            }
            catch (Exception e)
            {
                Console.WriteLine("Open Browser had an exception: " + e);
            }
            
        }

        [Test]
        public void LogIn()
        {
            try
            {
                driver.FindElement(By.Id("sso_Email")).SendKeys("automatic.test@yopmail.com");
                driver.FindElement(By.Id("sso_Password")).SendKeys("automatic.test" + Keys.Enter);
                Assert.IsTrue(driver.FindElement(By.Id("main-menu")).Displayed, "The login has failed");
                Console.WriteLine("LogIn has been completed");
            }
            catch (Exception e)
            {
                Console.WriteLine("LogIn had an exception");
                Assert.IsFalse(true, "LogIn had an exception: " + e);
            }

        }
        [Test]
        public void RegisterAccountWithoutName()
        {
            try
            {
                driver.FindElement(By.Id("sso_Email")).SendKeys("automatic.test@yopmail.com");
                driver.FindElement(By.Id("sso_Password")).SendKeys("automatic.test" + Keys.Enter);
                Console.WriteLine("LogIn has been completed");
                driver.FindElement(By.XPath("//*[@id='main-menu']/ul/li[4]")).Click();
                driver.FindElement(By.CssSelector("#ui-main > div > div.UIGridDialogPattern > div.UIContainer > div > div")).Click();
                driver.FindElement(By.CssSelector("#ui-main > div > div.UIGridDialogPattern > div.UIContainer > div > div > ul > li.item_1.last")).Click();
                driver.FindElement(By.Id("bank_account_account_type_id")).Click();
                driver.FindElement(By.CssSelector("#bank_account_account_type_id > option:nth-child(3)")).Click();
                driver.FindElement(By.Id("bank_account_account_number")).SendKeys("1111 1111 40 1111111111");
                driver.FindElement(By.CssSelector("#new_bank_account > div.options > div > span:nth-child(2) > button")).Click();

                Assert.IsTrue(driver.FindElement(By.CssSelector("#new_bank_account > div.options > div > span.UIButton.validations > span > i")).Displayed, "The account can be created");
                Console.WriteLine("I couldn't register the account without a name");
            }
            catch (Exception e)
            {
                Console.WriteLine("Account register without name had an exception");
                Assert.IsFalse(true, "Account register without name had an exception: " + e);
            }

        }
        [Test]
        public void RegisterAccount()
        {
            string numberPage;
            string actualNumber;
            string name = "Bank Account Test " + DateTime.Now.ToLongTimeString();

            try
            {
                driver.FindElement(By.Id("sso_Email")).SendKeys("automatic.test@yopmail.com");
                driver.FindElement(By.Id("sso_Password")).SendKeys("automatic.test" + Keys.Enter);
                Console.WriteLine("LogIn has been completed");
                driver.FindElement(By.XPath("//*[@id='main-menu']/ul/li[4]")).Click();
                driver.FindElement(By.CssSelector("#ui-main > div > div.UIGridDialogPattern > div.UIContainer > div > div")).Click();
                driver.FindElement(By.CssSelector("#ui-main > div > div.UIGridDialogPattern > div.UIContainer > div > div > ul > li.item_1.last")).Click();
                driver.FindElement(By.Id("bank_account_account_name")).Click();
                driver.FindElement(By.Id("bank_account_account_name")).SendKeys(name);
                driver.FindElement(By.Id("bank_account_account_type_id")).Click();
                driver.FindElement(By.CssSelector("#bank_account_account_type_id > option:nth-child(3)")).Click();
                driver.FindElement(By.Id("bank_account_account_number")).Clear();
                driver.FindElement(By.Id("bank_account_account_number")).SendKeys("1111 1111 30 1111111111");
                driver.FindElement(By.CssSelector("#new_bank_account > div.options > div > span:nth-child(2) > button")).Click();
                driver.FindElement(By.CssSelector("#bank_account_grid > div.UIPager > ul.navigation > li:nth-child(3) > span"));
                System.Threading.Thread.Sleep(5000);

                Utilities.scrollUp(driver, "#bank_account_grid > div.UIPager > ul.navigation > li:nth-child(3) > span");

                numberPage = driver.FindElement(By.CssSelector("#bank_account_grid > div.UIPager > ul.navigation > li:nth-child(3) > span")).Text.ToString();
                actualNumber = driver.FindElement(By.CssSelector("#current_page")).GetAttribute("value");

                while (numberPage != actualNumber)
                {
                    driver.FindElement(By.Id("next")).Click();
                    actualNumber = driver.FindElement(By.CssSelector("#current_page")).GetAttribute("value");
                }

                for (int i = 1; i < 11; i++)
                {
                    string nameActual = driver.FindElement(By.XPath("//*[@id='bank_account_grid']/div[1]/table/tbody/tr[" + i + "]/td[2]")).Text.ToString();
                    if (nameActual.CompareTo(name) == 1)
                    {
                        Assert.IsTrue(true, "The account do not exist");
                        break;
                    }
                    i++;
                }
                Console.WriteLine("Account register has been checked");
            }
            catch (Exception e)
            {
                Console.WriteLine("Account register  had an axception");
                Assert.IsFalse(true, "Account register had an axception: " + e);
            }
        }
        [TearDown]
        public  void CloseBrowser()
        {
            try
            {
                driver.Close();
                driver.Dispose();
                Console.WriteLine("Close browser");
            }
            catch (Exception e)
            {
                Console.WriteLine("Close browser had an exception: "+ e);
            }
           
        }
    }
}
