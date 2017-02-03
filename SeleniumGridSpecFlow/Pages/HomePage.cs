using SeleniumGridSpecFlow.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SeleniumGridSpecFlow.Pages
{
    class HomePage
    {
        private ISearchPage _page;
        public HomePage()
        {
            var pages = new ConcurrentStack<ISearchPage>();
            Parallel.Invoke(
                //  () => pages.Push(new SearchPage<InternetExplorerGrid>()),
                () => pages.Push(new SearchPage<ChromeGrid>()),
                () => pages.Push(new SearchPage<FireFoxGrid>())
                );
            var parallelPage = new ParallelModel<ISearchPage>(pages.ToArray());
            _page = parallelPage.Cast();
        }
      
    }
}
