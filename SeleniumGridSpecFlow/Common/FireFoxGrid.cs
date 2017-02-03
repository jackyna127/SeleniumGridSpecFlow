using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumGridSpecFlow.Common
{
    class FireFoxGrid : RemoteWebDriver
    {
        public FireFoxGrid()
            : base(new Uri("http://localhost:5555/wd/hub"), DesiredCapabilities.Firefox())
        { 
        
        }
    }
}
