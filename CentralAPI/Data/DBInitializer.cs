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
                new ParkingLot{name="Parque da República", owner="NorteShopping", location="Avenida da República", capacity=125, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                new ParkingLot{name="Parque Brito Capelo",owner="InRio",location="Rua Brito Capelo",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                new ParkingLot{name="Parque da Liberdade", owner="CasinoEstoril",location="Avenida da Liberdade",capacity=423,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                new ParkingLot{name="Parque dos Congregados", owner="Braga",location="Rua dos Congregados",capacity=588,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
                new ParkingLot{name="Parque Carlos Alberto", owner="Porto",location="Praça Carlos Alberto",capacity=365,openingTime= DateTime.Parse("2020-02-22 12:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00")},
            };
            foreach(ParkingLot p in parkingLots)
            {
                context.ParkingLots.Add(p);
            }
            context.SaveChanges();

            var centralReservations = new CentralReservation[]
            {
                    new CentralReservation{reservationID="ABC1",isCancelled=false,startTime= DateTime.Parse("2021-05-22 07:00:00"),endTime= DateTime.Parse("2021-05-22 08:00:00"),parkingSpotID="A1",userID="1"},
                    new CentralReservation{reservationID="ABC2",isCancelled=true,startTime= DateTime.Parse("2021-01-31 07:00:00"),endTime= DateTime.Parse("2021-08-22 09:00:00"),parkingSpotID="E1",userID="2"},
                    new CentralReservation{reservationID="ABC3",isCancelled=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 19:00:00"),parkingSpotID="I1",userID="3"},
                    new CentralReservation{reservationID="ABC4",isCancelled=false,startTime= DateTime.Parse("2021-10-22 07:00:00"),endTime= DateTime.Parse("2021-10-22 10:00:00"),parkingSpotID="O1",userID="4"},
                    new CentralReservation{reservationID="ABC5",isCancelled=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 08:00:00"),parkingSpotID="A3",userID="5"},
                    new CentralReservation{reservationID="ABC6",isCancelled=false,startTime= DateTime.Parse("2021-08-22 12:00:00"),endTime= DateTime.Parse("2021-08-22 13:00:00"),parkingSpotID="A1",userID="6"},
                    new CentralReservation{reservationID="ABC7",isCancelled=true,startTime= DateTime.Parse("2021-07-22 14:00:00"),endTime= DateTime.Parse("2021-07-22 15:00:00"),parkingSpotID="A1",userID="7"},
                    new CentralReservation{reservationID="ABC8",isCancelled=false,startTime= DateTime.Parse("2021-06-22 18:00:00"),endTime= DateTime.Parse("2021-06-22 19:00:00"),parkingSpotID="I1",userID="8"},
            };
            foreach (CentralReservation r in centralReservations) {
                context.CentralReservations.Add(r);
            }
            context.SaveChanges();


            if (context.Users.Any()) {
                return; //DB is seeded
            }

            var users = new User[]
            {
                    new User{userID="1", name="Mariana Gomes", email="marianagomes@gmail.com", nif="221133486"},
                    new User{userID="2", name="Tiago Azevedo", email="tiagoazevedo@gmail.com", nif="113333986"},
                    new User{userID="3", name="João Martins", email="joaomartins@gmail.com", nif="231163886"},
                    new User{userID="4",name="Diego Maradona",email= "dieguito@gmail.com",nif= "554112892"},
                    new User{userID="5",name="Michael Jordan",email= "air.jordan@gmail.com" ,nif= "554112870"},
                    new User{userID="6",name="Freddie Mercury",email= "music@gmail.com",nif= "553112870" },
                    new User{userID="7",name="Queen Elizabeth II",email= "the.queen@gmail.com" ,nif= "554112881"},
                    new User{userID="8",name="Afonso Henriques",email= "afonso.rei@gmail.com",nif= "554112894"},
                    new User{userID="9",name="Elon Musk",email= "spaceX@gmail.com",nif= "583112870"},
                    new User{userID="10",name="André André",email= "vitoriasc@gmail.com" ,nif= "554112881"},
                    new User{userID="11",name="Jô Soares",email= "gordo@gmail.com",nif= "154112894"},
            };
            foreach (User u in users) {
                context.Users.Add(u);
            }
            context.SaveChanges();


            //if (context.Reservations.Any())
            //{
            //    return;
            //}
        }
    }
}
