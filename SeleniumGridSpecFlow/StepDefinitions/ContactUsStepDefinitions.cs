using SeleniumGridSpecFlow.Common;
using SeleniumGridSpecFlow.Pages;
using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumGridSpecFlow.StepDefinitions
{
    [Binding]
    public class ContactUsSteps: Steps
    {
        private IContactUpPage _page;
        [BeforeScenario]
        public void SetUp()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string[] browsers = appSettings["Browsers"].Split(';');

            var pages = new ConcurrentStack<IContactUpPage>();
            foreach (string browser in browsers)
            {
                if (browser == "Chrome")
                    Parallel.Invoke(() => pages.Push(new ContactUsPage<ChromeGrid>()));
                if (browser == "Firefox")
                    Parallel.Invoke(() => pages.Push(new ContactUsPage<FireFoxGrid>()));
                if (browser == "IE")
                    Parallel.Invoke(() => pages.Push(new ContactUsPage<InternetExplorerGrid>()));
            }
            var parallelPage = new ParallelModel<IContactUpPage>(pages.ToArray());
            _page = parallelPage.Cast();

        
        }
        [AfterScenario]
        public void TearDown()
        {
            _page.Close();
        }
        [Given(@"I am at the Home Page")]
        public void GivenIAmAtTheHomePage()
        {
            System.Console.Write("I am at homepage\r\n");
        }
        [Given(@"I navigate to ContactUs Page")]
        public void GivenINavigateToContactUsPage()
        {
            _page.GotoContactUsPage();
        }
        [When(@"I fill in First Name as (.*)")]
        public void WhenIFillInFirstNameAs(string p0)
        {

            _page.FillInFirstName(p0);
        }
        [When(@"I fill in Last Name as (.*)")]
        public void WhenIFillInLastNameAs(string p0)
        {
            _page.FillInLastName(p0);
        }
        [When(@"I fill in Job Title as (.*)")]
        public void WhenIFillInJobTitleAs(string p0)
        {
            _page.FillInJobTitle(p0);
        }
        [When(@"I fill in Organisation as (.*)")]
        public void WhenIFillInOrganisationAs(string p0)
        {
            _page.FillInOrganisation(p0);
        }
        [When(@"I fill in Phone as (.*)")]
        public void WhenIFillInPhoneAs(int p0)
        {
            _page.FillInPhone(p0);
        }
        [When(@"I fill in Email as (.*)")]
        public void WhenIFillInEmailAs(string p0)
        {
            _page.FillInEmail(p0);
        }
        [When(@"I press Submit Button")]
        public void WhenIClickSubmitButton()
        {
            _page.PressSubmitButton();
        }
        [Then(@"I can see Please enter your Location message display")]
        public void ThenICanSeePleaseEnterYourLocationMessageDisplay()
        {
            _page.VerifyLocationErrorMessage();
        }
        [Then(@"I can see error message display")]
        public void ThenICanSeeErrorMessageDisplay()
        {
            Then("I can see Please enter your Location message display");
        }
        [When(@"I Fill the contact information form with correct information")]
        public void WhenIFillTheContactInformationFormWithCorrectInformation()
        {
            When(string.Format("I fill in First Name as {0}", "Helen"));
            When(string.Format("I fill in Last Name as {0}", "West"));
            When(string.Format("I fill in Job Title as {0}", "Test Analyst"));
            When(string.Format("I fill in Organisation as {0}", "Planit"));
            When(string.Format("I fill in Phone as {0}", "0211569987"));
            When(string.Format("I fill in Email as {0}", "test@test.com"));
        }

        [When(@"I Fill the contact information form with")]
        public void WhenIFillTheContactInformationFormWith(Table table)
        {

            _page.FillInFirstName(table.Rows[0]["firstname"]);
            _page.FillInLastName(table.Rows[0]["lastname"]);
            _page.FillInJobTitle(table.Rows[0]["jobtitle"]);
            _page.FillInOrganisation(table.Rows[0]["organisation"]);
            _page.FillInPhone(Convert.ToInt32(table.Rows[0]["phone"]));
            _page.FillInEmail(table.Rows[0]["email"]);
        }
        [Then(@"I can see it successfully submit the contact information")]
        public void ThenICanSeeItSuccessfullySubmitTheContactInformation()
        {
            _page.VerifyContactUsMessageDisplay();
            //  ScenarioContext.Current.Pending(); 
        }


        [When(@"I want to try pending status in test report")]
        public void WhenIWantToTryPendingStatusInTestReport()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I can see the pending result")]
        public void ThenICanSeeThePendingResult()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
