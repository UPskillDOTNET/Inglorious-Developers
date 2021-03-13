using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAroundE2ETest.Pages
{
    class UserPage
    {
        public UserPage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        private IWebDriver Driver { get; }

        public IWebElement lnkDeposit => Driver.FindElement(By.LinkText("Deposit"));

        public IWebElement lnkHome => Driver.FindElement(By.LinkText("HOME"));

        public bool ISDepositButtonExist() => lnkDeposit.Displayed;

        public void Click(string method)
        {
            switch (method)
            {
                case "Home":
                    lnkHome.Click();
                    break;
                case "Deposit":
                    lnkDeposit.Click();
                    break;
                default:
                    break;
            }
        }
    }
}
