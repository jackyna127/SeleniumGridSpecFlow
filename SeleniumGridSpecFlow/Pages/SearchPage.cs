using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Concurrent;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;


namespace SeleniumGridSpecFlow.Pages
{
    class SearchPage<TDriver> : ISearchPage where TDriver : IWebDriver, new()
    {
        private readonly IWebDriver webDriver;
        private WebDriverWait Wait { get; set; }

        public SearchPage()
        {
            webDriver = new TDriver();
            // any time we wait we will wait for 60 seconds.
            Wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 60));
            var appSettings = ConfigurationManager.AppSettings;
            string baseUrl = appSettings["BaseUrl"];
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(baseUrl);
        }
        #region Elements
        private By searchInput = By.Id("p_lt_ctl02_SmartSearchBox_txtWord");
        private By searchLink = By.XPath("//a[@class='header_search-btn js_header_search-btn']");
        private By searchButton = By.Id("p_lt_ctl02_SmartSearchBox_btnSubmit");
        private string searchResult { get; set; }
        #endregion

        public void WaitForPageElement(By byElement)
        {
            // WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(byElement));
            }
            catch (WebDriverTimeoutException)
            {
                //add throw new exception message
            }
        }

        public void ClickSearchLink()
        {
            WaitForPageElement(searchLink);
            webDriver.FindElement(searchLink).Click();
        }
        public void EnterSearchContent(string searchText)
        {
            ClickSearchLink();
            WaitForPageElement(searchInput);
            webDriver.FindElement(searchInput).Clear();
            webDriver.FindElement(searchInput).SendKeys(searchText);
            searchResult = searchText;
        }
        public void PressSearchButton()
        {
            webDriver.FindElement(searchButton).Click();
        }
        public void VerifySearchResult()
        {
            WaitForPageElement(By.PartialLinkText(searchResult));
            true.Equals(webDriver.FindElement(By.PartialLinkText(searchResult)).Displayed);
        }
        public bool PageContains(string content)
        {
            try
            {
                Wait.Until(x => x.PageSource.Contains(content));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Close()
        {
            webDriver.Dispose();
        }
    }
}
