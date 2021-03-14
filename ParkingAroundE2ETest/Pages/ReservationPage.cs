using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAroundE2ETest.Pages
{
    class ReservationPage
    {
        public ReservationPage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        private IWebDriver Driver { get; }

        public IWebElement lnkHome => Driver.FindElement(By.LinkText("HOME"));
        public IWebElement lnkNow => Driver.FindElement(By.XPath("//a[@href='/MakeReservation/Now/1']"));
        public IWebElement lnkPSpot => Driver.FindElement(By.Id("park-spot_O2"));
        public IWebElement txtEndTime => Driver.FindElement(By.Name("endTime"));
        public IWebElement btnConfirm => Driver.FindElement(By.XPath("//input[@value='Confirm']"));
        public IWebElement parkingSpot => Driver.FindElement(By.Id("pSpot_1_O2"));
        public IWebElement btnCancel => Driver.FindElement(By.Id("confirmCancel"));
        public IWebElement btnCancelReservation => Driver.FindElement(By.Id("cancelReservation"));

        
        public void Click(string method)
        {
            switch(method)
            {
                case "Now":
                    lnkNow.Click();
                    break;
                case "Later":
                    break;
                default:
                    break;
            }
        }

        public void ParkingSpotClick()
        {
            lnkPSpot.Click();
        }
        public void ConfirmClick()
        {
            btnConfirm.Click();
        }
        public void SendEndTime(string date)
        {
            txtEndTime.SendKeys(date);
        }

        public bool iSReservationExists() => parkingSpot.Displayed;
        public bool isParkingSpotFree() => lnkPSpot.Displayed;
    }
}
