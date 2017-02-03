using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumGridSpecFlow.Pages
{
    class ContactUsPage<TDriver>:IContactUpPage where TDriver : IWebDriver, new()
    {
        private readonly IWebDriver webDriver;
        private WebDriverWait Wait { get; set; }

        public ContactUsPage()
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

        private By firstnameInput = By.Id("p_lt_ctl06_pageplaceholder_p_lt_ctl01_ContactForm_viewBiz_FirstName_txtText");
        private By lastnameInput = By.Id("p_lt_ctl06_pageplaceholder_p_lt_ctl01_ContactForm_viewBiz_LastName_txtText");
        private By jobtitleInput = By.Id("p_lt_ctl06_pageplaceholder_p_lt_ctl01_ContactForm_viewBiz_JobTitle_txtText");
        private By organisationInput = By.Id("p_lt_ctl06_pageplaceholder_p_lt_ctl01_ContactForm_viewBiz_Organisation_txtText");
        private By locationInput = By.Id("p_lt_ctl06_pageplaceholder_p_lt_ctl01_ContactForm_viewBiz_State_txtText");
        private By phoneInput = By.Id("p_lt_ctl06_pageplaceholder_p_lt_ctl01_ContactForm_viewBiz_Phone_txtText");
        private By emailInput = By.Id("p_lt_ctl06_pageplaceholder_p_lt_ctl01_ContactForm_viewBiz_Email_txtText");
        private By submitButton = By.Id("p_lt_ctl06_pageplaceholder_p_lt_ctl01_ContactForm_viewBiz_btnOK");
        private By locationErrorMessage = By.XPath("//span[@class='EditingFormErrorLabel']");
        private By contactUsMessage = By.XPath("//span[@id='p_lt_ctl06_pageplaceholder_p_lt_ctl01_ContactForm_viewBiz_pM_lS']/h4");
        private By contactUs = By.LinkText("Contact Us");
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
        public void FillInFirstName(string firstName)
        {
            WaitForPageElement(firstnameInput); 
            webDriver.FindElement(firstnameInput).SendKeys(firstName);
        }
        public void FillInLastName(string lastName)
        {
            webDriver.FindElement(lastnameInput).SendKeys(lastName);
        }
        public void FillInJobTitle(string jobTitle)
        {
            webDriver.FindElement(jobtitleInput).SendKeys(jobTitle);
        }
        public void FillInOrganisation(string organisation)
        {
            webDriver.FindElement(organisationInput).SendKeys(organisation);
        }
        public void FillInPhone(int phoneNumber)
        {
            webDriver.FindElement(phoneInput).SendKeys(phoneNumber.ToString());
        }
        public void FillInEmail(string emailAddress)
        {
            webDriver.FindElement(emailInput).SendKeys(emailAddress);
        }
        public void FillInLocation(string location)
        {
            webDriver.FindElement(locationInput).SendKeys(location);
        }
        public void PressSubmitButton()
        {
            webDriver.FindElement(submitButton).Click();
        }
        public void VerifyLocationErrorMessage()
        {
            WaitForPageElement(locationErrorMessage); 
            true.Equals(webDriver.FindElement(locationErrorMessage).Displayed);
        }
        public void VerifyContactUsMessageDisplay()
        {
            WaitForPageElement(contactUsMessage); 
            true.Equals(webDriver.FindElement(contactUsMessage).Displayed);
        }
        public void GotoContactUsPage()
        {
            webDriver.FindElement(contactUs).Click();
        }
        public void Close()
        {
            webDriver.Dispose();
        }
    }
}
