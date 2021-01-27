using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Models;
using testProject;
using PrivateParkAPI.Controllers;

namespace testPrivateParkAPI
{
    public class ReservationsControllerTest
    {

        [Fact]
        public async Task GetAllReservationsAsync_ShouldReturnAllReservationsAsync()
        {
            // Arrange 
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            //Arrange
            var TestContext = TodoContextMocker.GetPrivateParkContext(dbName);
            var theController = new ReservationsController(TestContext);
            //Act
            var result = await theController.GetReservations();

            //Assert
            var items = Assert.IsType<List<Reservation>>(result.Value);
            Assert.Equal(8, items.Count);
        }
    }
}
