using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Configuration;
using SeleniumGridSpecFlow.Pages;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using SeleniumGridSpecFlow.Common;

namespace SeleniumGridSpecFlow.StepDefinitions
{
    [Binding]
    public sealed class SearchStepDefinition
    {

        private ISearchPage _page;
        [BeforeScenario]
        public void Setup()
        {
            System.Console.Write("Scenarios Setup.\r\n");
                 
            var appSettings = ConfigurationManager.AppSettings;
            string[] browsers = appSettings["Browsers"].Split(';');

            var pages = new ConcurrentStack<ISearchPage>();
            foreach (string browser in browsers)
            {
               if(browser == "Chrome")
                    Parallel.Invoke(() => pages.Push(new SearchPage<ChromeGrid>()));
               if(browser == "Firefox")
                    Parallel.Invoke(() => pages.Push(new SearchPage<FireFoxGrid>()));
               if(browser == "IE")
                    Parallel.Invoke(() => pages.Push(new SearchPage<InternetExplorerGrid>()));
            }
            var parallelPage = new ParallelModel<ISearchPage>(pages.ToArray());
            _page = parallelPage.Cast();

        }
        [AfterScenario]
        public void Teardown()
        {
            System.Console.Write("Scenarios tear down.\r\n");
            _page.Close();
        }
    

        [Given(@"I have entered search content")]
        public void GivenIHaveEnteredSearchContent(Table table)
        {
            _page.EnterSearchContent(table.Rows[0]["searchcontent"]);
        }

        [Given(@"I have entered (.*)")]
        public void GivenIHaveEntered(string search)
        {
            _page.EnterSearchContent(search);
        }


        [When(@"I press search button")]
        public void WhenIPressSearchButton()
        {
            _page.PressSearchButton();
        }

        [Then(@"The result should be displayed on the screen")]
        public void ThenTheResultShouldBeDisplayedOnTheScreen()
        {
            _page.VerifySearchResult();
        }

    }
}
