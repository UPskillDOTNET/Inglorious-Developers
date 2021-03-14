﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ParkingAroundE2ETest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAroundE2ETest.Test
{
    class Test
    {
        IWebDriver webDriver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
            //Open the browser
            webDriver.Navigate().GoToUrl("https://localhost:44312/");
        }

        [Test]
        public void Login()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.ClickLogin();

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.Login("JoaoM", "Wtv123!");

            UserPage userPage = new UserPage(webDriver);

            Assert.That(userPage.ISDepositButtonExist, Is.True);             
        }

        [Test]
        public void Reservation()
        {
            UserPage userPage = new UserPage(webDriver);
            userPage.Click("Home");

            HomePage homePage = new HomePage(webDriver);
            homePage.ClickReservation();
            

            ReservationPage reservationPage = new ReservationPage(webDriver);
            reservationPage.Click("Now");
            reservationPage.ParkingSpotClick();
            reservationPage.SendEndTime("03/18/2021");
            reservationPage.SendEndTime(Keys.Tab);
            reservationPage.SendEndTime("0533PM");
            reservationPage.ConfirmClick();
            reservationPage.ConfirmClick();

            Assert.That(reservationPage.iSReservationExists, Is.True);
        }

        [Test]
        public void ReservationCancel()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.lnkUser.Click();

            UserPage userPage = new UserPage(webDriver);
            userPage.lnkBook.Click();

            ReservationPage reservationPage = new ReservationPage(webDriver);
            reservationPage.parkingSpot.Click();            
            reservationPage.btnCancelReservation.Click();
            reservationPage.btnCancel.Click();
            reservationPage.lnkHome.Click();
            
            homePage.ClickReservation();

            reservationPage.Click("Now");

            Assert.That(reservationPage.isParkingSpotFree, Is.True);
        }

        [OneTimeTearDown]
        public void Teardown() 
        {
            Database.DeleteReservation();
            Database.ResetWallet();
            webDriver.Quit();
        }
    }
}
