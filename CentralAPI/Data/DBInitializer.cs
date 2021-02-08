using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Data
{
    public class DBInitializer
    {
        public static void Initialize (CentralParkContext context)
        {
            context.Database.EnsureCreated();

            if (context.ParkingLots.Any())
            {
                return; //DB is seeded
            }

            var parkingLots = new ParkingLot[]
            {
                new ParkingLot{parkingLotID= 1, name="Parque da República", owner="NorteShopping", location="Avenida da República", capacity=125, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                new ParkingLot{parkingLotID= 2, name="Parque Brito Capelo",owner="InRio",location="Rua Brito Capelo",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                new ParkingLot{parkingLotID= 3, name="Parque da Liberdade", owner="CasinoEstoril",location="Avenida da Liberdade",capacity=423,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                new ParkingLot{parkingLotID= 4, name="Parque dos Congregados", owner="Braga",location="Rua dos Congregados",capacity=588,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                new ParkingLot{parkingLotID= 5, name="Parque Carlos Alberto", owner="Porto",location="Praça Carlos Alberto",capacity=365,openingTime= DateTime.Parse("2020-02-22 12:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
            };
            foreach(ParkingLot p in parkingLots)
            {
                context.ParkingLots.Add(p);
            }
            context.SaveChanges();

            //if (context.Reservations.Any())
            //{
            //    return;
            //}
        }
    }
}
