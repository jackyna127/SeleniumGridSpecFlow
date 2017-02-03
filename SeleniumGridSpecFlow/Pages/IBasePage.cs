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
    public interface ISearchPage
    {
        bool PageContains(string content);
        void Close();
        void ClickSearchLink();
        void EnterSearchContent(string searchText);
        void PressSearchButton();
        void VerifySearchResult();
    }
    public interface IContactUpPage
    {
       void FillInFirstName(string firstName);

        void FillInLastName(string lastName);

        void FillInJobTitle(string jobTitle);

        void FillInOrganisation(string organisation);

        void FillInPhone(int phoneNumber);

        void FillInEmail(string emailAddress);

        void FillInLocation(string location);

        void PressSubmitButton();

        void VerifyLocationErrorMessage();

        void VerifyContactUsMessageDisplay();

        void GotoContactUsPage();
        void Close();
      
    }
    public class BasePage
    {
        protected IWebDriver webDriver;
        protected int secondTimeOut;
        public BasePage()
        {
           

        }
        public void Init()
        {
            var appSettings = ConfigurationManager.AppSettings;
            secondTimeOut = Convert.ToInt32(appSettings["TimeOut"]);
        
        }
        public void WaitForPageElement(By byElement)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(secondTimeOut));

            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(byElement));
            }
            catch (WebDriverTimeoutException)
            {
                //add throw new exception message
            }

        }
        
        
    }
    
  }
