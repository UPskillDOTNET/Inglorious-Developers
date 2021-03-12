using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAroundE2ETest.Pages
{
    public class HomePage
    {
        public HomePage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        private IWebDriver Driver { get; }

        public IWebElement lnkLogin => Driver.FindElement(By.Id("login_btn1"));

        public IWebElement lnkReservation => Driver.FindElement(By.LinkText("MAKE A RESERVATION"));

        public void ClickLogin()
        {
            lnkLogin.Click();
        }

        public void ClickReservation()
        {
            lnkReservation.Click();
        }
    }
}
