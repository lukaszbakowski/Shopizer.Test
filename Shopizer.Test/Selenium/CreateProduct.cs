using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using System.Net;


namespace Shopizer.Test.Selenium
{
    public class CreateProduct : IClassFixture<TestFixture>
    {
        private readonly IWebDriver _driver;
        private int _productOrder = 0; 

        public CreateProduct(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }
        private void CreateProductCategory()
        {

        }
        private void CreateProductType()
        {

        }


        [Theory]
        [JsonInlineData("sampleProducts.json")]
        public async Task TestB_AddSingleProduct(string productNameParameter)
        {
            _productOrder++;
            string erroMsg = string.Empty;
            bool ok;
            int error = 0;
            try
            {
                await Task.Delay(200);
                _driver.Navigate().GoToUrl($"{TestFixture.UrlBase}/#/pages/catalogue/products/create-product");
                await Task.Delay(200);
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until(driver => driver.FindElement(By.Id("identifier")));

                #region Product definition
                IWebElement visible = _driver.FindElement(By.CssSelector($"[formcontrolname='visible']")).FindElement(By.TagName("label"));
                visible.Click();

                _driver.FindElement(By.XPath($"//nb-select[@name='selectedLanguage']")).FindElement(By.TagName("button"))
                    .Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//nb-option")))
                    .Click();

                _driver.FindElement(By.XPath($"//p-dropdown[@formcontrolname='manufacturer']"))
                    .FindElement(By.CssSelector(".ui-dropdown-trigger"))
                    .Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p-dropdown[@formcontrolname='manufacturer']//li[@aria-label='DEFAULT']")))
                    .Click();

                _driver.FindElement(By.XPath($"//p-dropdown[@formcontrolname='type']"))
                    .FindElement(By.CssSelector(".ui-dropdown-trigger"))
                    .Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p-dropdown[@formcontrolname='type']//li[@aria-label='1']")))
                    .Click();

                await Task.Delay(2000);

                var guid = Guid.NewGuid().ToString("N");
                IWebElement uniqueIdentifier = _driver.FindElement(By.Id("identifier"));
                uniqueIdentifier.SendKeys(guid);

                IWebElement dateAvailable = _driver.FindElement(By.CssSelector($"[formcontrolname='dateAvailable']"));
                dateAvailable.SendKeys(DateTime.Today.ToString());

                IWebElement order = _driver.FindElement(By.Id("order"));
                order.SendKeys(_productOrder.ToString());

                #endregion

                #region Seo details
                wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                IWebElement seoDetailsContainer = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@formarrayname='descriptions']")));
                IList<IWebElement> inputElements = seoDetailsContainer.FindElements(By.XPath(".//input[@formcontrolname]"));

                IWebElement productName = _driver.FindElement(By.XPath("//div[contains(@formarrayname,'descriptions')]//input[contains(@formcontrolname,'name')]"));
                productName.SendKeys(productNameParameter);

                IWebElement producHighlight = _driver.FindElement(By.XPath("//div[contains(@formarrayname,'descriptions')]//input[contains(@formcontrolname,'highlight')]"));
                producHighlight.SendKeys(productNameParameter);

                IWebElement producKeyWords = _driver.FindElement(By.XPath("//div[contains(@formarrayname,'descriptions')]//input[contains(@formcontrolname,'keyWords')]"));
                producKeyWords.SendKeys(productNameParameter);

                IWebElement producMetaDescription = _driver.FindElement(By.XPath("//div[contains(@formarrayname,'descriptions')]//input[contains(@formcontrolname,'metaDescription')]"));
                producMetaDescription.SendKeys(productNameParameter);

                IWebElement productFriendlyUrl = _driver.FindElement(By.XPath("//div[contains(@formarrayname,'descriptions')]//input[contains(@formcontrolname,'friendlyUrl')]"));
                productFriendlyUrl.Clear();
                productFriendlyUrl.SendKeys(guid);
                #endregion

                #region TextArea

                IWebElement textArea = _driver.FindElement(By.XPath("//div[contains(@class,'note-editable')]"));
                textArea.SendKeys("lorem ipsum");

                #endregion

                #region Specifications
                Random random = new Random();

                IWebElement producWidth = _driver.FindElement(By.Id("width"));
                producWidth.SendKeys(random.Next(1, 11).ToString());

                IWebElement producHeight = _driver.FindElement(By.Id("height"));
                producHeight.SendKeys(random.Next(1, 11).ToString());

                IWebElement producLength = _driver.FindElement(By.Id("length"));
                producLength.SendKeys(random.Next(1, 11).ToString());

                IWebElement producWeight = _driver.FindElement(By.Id("weight"));
                producWeight.SendKeys(random.Next(1, 11).ToString());
                #endregion

                #region Inventory
                IWebElement datePrice = _driver.FindElement(By.CssSelector($"[formcontrolname='price']"));
                datePrice.SendKeys(random.Next(1, 11).ToString());

                IWebElement dateQuantity = _driver.FindElement(By.CssSelector($"[formcontrolname='quantity']"));
                dateQuantity.SendKeys(random.Next(1, 11).ToString());
                #endregion

                #region Submit
                _driver.FindElements(By.XPath("//nb-card-header[contains(@class,'main_header')]"))[1]
                    .FindElement(By.CssSelector("button[type='button']"))
                    .Click();
                await Task.Delay(1000);
                #endregion

                error++;
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[contains(@placeholder,'Sku')]")))
                    .SendKeys(guid);
                await Task.Delay(1000);

                error++;
                for (int i = 0; i <= 10; i++)
                {
                    IList<IWebElement> tr = _driver.FindElements(By.XPath("//tbody//tr"));

                    if (tr.Count() == 1)
                        break;

                    if (i == 10 && (tr.Count() > 1 || tr.Count() == 0))
                        throw new Exception("couldnt find created product");

                    await Task.Delay(1000);
                }

                error++;
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a//i[contains(@class,'nb-edit')]")))
                    .Click();


                wait.Until(ExpectedConditions.ElementExists(By.TagName("body")))
                    .SendKeys(Keys.PageDown);
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript("window.scrollBy(0, 900);");

                var path = await DownloadRandomPicture(guid);

                await Task.Delay(2000);

                error++;
                wait.Until(ExpectedConditions.ElementExists(By.Id("fileInput")))
                    .SendKeys(path);

                await Task.Delay(1000);

                error++;
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@cdkdroplistgroup]//div[@cdkdroplist]")));

                error++;
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".route-tabset li:nth-child(2)")))
                    .Click();

                error++;
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("ng-multiselect-dropdown")))
                    .Click();

                error++;
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//ul[contains(@class, 'item2')]//li[1]")))
                    .Click();


                await Task.Delay(5555);

                ok = true;
                erroMsg += "success";
            }
            catch (Exception ex)
            {
                erroMsg += $"{error} -> {ex.Message}";
                ok = false;
            }

            Assert.True(ok, erroMsg);
        }
        private async Task<string> DownloadRandomPicture(string guid)
        {
            string basePath = $"{AppDomain.CurrentDomain.BaseDirectory}temp";
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            string localFilePath = $"{basePath}\\{guid}.jpg";
            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri("https://picsum.photos/800/600");
                client.DownloadFileAsync(uri, localFilePath);
            }
            return await Task.FromResult(localFilePath);
        }
    }
}
