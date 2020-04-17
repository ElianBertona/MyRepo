using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageProject
{
    public class Utilities
    {
        public static void scrollUp(IWebDriver driver,string locator)
        {
            IWebElement s = driver.FindElement(By.CssSelector(locator));
            IJavaScriptExecutor je = (IJavaScriptExecutor)driver;
            je.ExecuteScript("arguments[0].scrollIntoView(false);", s);
        }
    }
}
