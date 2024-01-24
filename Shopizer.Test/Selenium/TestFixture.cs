using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Shopizer.Test.Selenium
{
    public class TestFixture : IDisposable
    {
        public IWebDriver Driver { get; }
        public static string UrlBase => "http://admin.eab5a7achph3g7b9.polandcentral.azurecontainer.io";

        public TestFixture()
        {
            var options = new ChromeOptions();
            //options.AddArgument("--headless"); // Optional: Run Chrome in headless mode
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-web-security");
            options.AddArgument("--allow-running-insecure-content");

            Driver = new ChromeDriver(options);

            //Driver.Navigate().GoToUrl("http://localhost:4200/#/auth");
            Driver.Navigate().GoToUrl($"{UrlBase}/#/auth");

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

            IWebElement loginField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("username")));
            IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("password")));
            IWebElement submitButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[type='submit']")));

            loginField.SendKeys("admin@shopizer.com");
            passwordField.SendKeys("password");

            submitButton.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".page_title")));

            wait.Until(driver => driver.FindElement(By.XPath("//nb-action[contains(@nbcontextmenutag,'language')]"))).Click();
            IList<IWebElement> elements = wait.Until(driver => driver.FindElement(By.XPath("//div[contains(@class,'cdk-overlay-pane')]//ul[contains(@class,'menu-items')]"))).FindElements(By.TagName("li"));

            if (elements.Count != 2)
                throw new Exception("no count");

            elements[1].Click();
        }
        public void Dispose() { Driver.Close(); Driver.Dispose(); }
    }
}
