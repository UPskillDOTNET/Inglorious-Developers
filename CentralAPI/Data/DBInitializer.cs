using CentralAPI.Models;
using System;
using System.Linq;

namespace CentralAPI.Data
{
    public class DBInitializer
    {
        public static void Initialize(CentralAPIContext context)
        {
            context.Database.EnsureCreated();

            if (context.ParkingLots.Any())
            {
                return; //DB is seeded
            }

            var parkingLots = new ParkingLot[]
            {
                new ParkingLot{name="Parque da República", owner="NorteShopping", location="Avenida da República", capacity=125, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44350/"},
                new ParkingLot{name="Parque Brito Capelo",owner="InRio",location="Rua Brito Capelo",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44353/"},

            };
            foreach (ParkingLot p in parkingLots)
            {
                context.ParkingLots.Add(p);
            }
            context.SaveChanges();

            if (context.Users.Any())
            {
                return; //DB is seeded
            }

            var users = new User[]
            {
                    new User{userID="1", name="Mariana Gomes", email="marianaribgomes@gmail.com", nif="111111111"},
                    new User{userID="2", name="Tiago Azevedo", email="tiagomina.azevedo@gmail.com", nif="222222222"},
                    new User{userID="3", name="João Martins", email="joaom.vodafone@gmail.com", nif="333333333"},
                    new User{userID="4",name="Diego Maradona",email= "caiocruzeiror@gmail.com",nif= "444444444"},
                    new User{userID="5",name="Michael Jordan",email= "sergio.valente.pinto@gmail.com" ,nif= "555555555"},
                    new User{userID="6",name="Freddie Mercury",email= "music@gmail.com",nif= "666666666" },
                    new User{userID="7",name="Queen Elizabeth II",email= "the.queen@gmail.com" ,nif= "777777777"},
                    new User{userID="8",name="Afonso Henriques",email= "afonso.rei@gmail.com",nif= "888888888"},
                    new User{userID="9",name="Elon Musk",email= "spaceX@gmail.com",nif= "999999999"},
                    new User{userID="10",name="André André",email= "vitoriasc@gmail.com" ,nif= "121121121"},
                    new User{userID="11",name="Jô Soares",email= "gordo@gmail.com",nif= "131131131"},
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var centralReservations = new CentralReservation[]
            {
                    new CentralReservation{reservationID="ABC1",isCancelled=false,startTime= DateTime.Parse("2021-05-22 07:00:00"),endTime= DateTime.Parse("2021-05-22 08:00:00"),finalPrice= 10, parkingLotID=1,userID="1", parkingSpotID="A1"},
                    new CentralReservation{reservationID="ABC2",isCancelled=true,startTime= DateTime.Parse("2021-01-31 07:00:00"),endTime= DateTime.Parse("2021-08-22 09:00:00"),finalPrice= 10,parkingLotID=2,userID="2", parkingSpotID="B2"},
                    new CentralReservation{reservationID="ABC3",isCancelled=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 19:00:00"),finalPrice= 10,parkingLotID=1,userID="3", parkingSpotID="V4"},
                    new CentralReservation{reservationID="ABC4",isCancelled=false,startTime= DateTime.Parse("2021-10-22 07:00:00"),endTime= DateTime.Parse("2021-10-22 10:00:00"),finalPrice= 10,parkingLotID=2,userID="4", parkingSpotID="D3"},
                    new CentralReservation{reservationID="ABC5",isCancelled=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 08:00:00"),finalPrice= 10,parkingLotID=2,userID="5", parkingSpotID="E2"},
                    new CentralReservation{reservationID="ABC6",isCancelled=false,startTime= DateTime.Parse("2021-08-22 12:00:00"),endTime= DateTime.Parse("2021-08-22 13:00:00"),finalPrice= 10,parkingLotID=1,userID="6", parkingSpotID="B3"},
                    new CentralReservation{reservationID="ABC7",isCancelled=true,startTime= DateTime.Parse("2021-07-22 14:00:00"),endTime= DateTime.Parse("2021-07-22 15:00:00"),finalPrice= 10,parkingLotID=2,userID="7", parkingSpotID="P2"},
                    new CentralReservation{reservationID="ABC8",isCancelled=false,startTime= DateTime.Parse("2021-06-22 18:00:00"),endTime= DateTime.Parse("2021-06-22 19:00:00"),finalPrice= 10,parkingLotID=1,userID="8", parkingSpotID="C9"},
            };
            foreach (CentralReservation r in centralReservations)
            {
                context.CentralReservations.Add(r);
            }
            context.SaveChanges();

            if (context.Wallets.Any())
            {
                return;   // DB has been seeded
            }

            var Wallets = new Wallet[]
            {
                    new Wallet {walletID="1",totalAmount=250,currency="euro",userID="1"},
                    new Wallet {walletID="2",totalAmount=120,currency="USD",userID="2"},
                    new Wallet {walletID="3",totalAmount=25,currency="euro",userID="3"},
            };

            foreach (Wallet w in Wallets)
            {
                context.Wallets.Add(w);
            }
            context.SaveChanges();


            if (context.Sublets.Any())
            {
                return;   // DB has been seeded
            }

            var Sublets = new Sublet[]
           {
                    new Sublet {subletID = "abc4", reservationID = "wtv", mainUserID="1", subUserID = "2", letPrice = 3.0m, startDate= DateTime.Parse("2020-01-14 19:00:00"), endDate = DateTime.Parse("2020-01-15 19:00:00"), isCancelled=false},
                    new Sublet {subletID = "abc5", reservationID = "wtv2", mainUserID="2", subUserID = "3", letPrice = 3.0m, startDate= DateTime.Parse("2020-01-14 19:00:00"), endDate = DateTime.Parse("2020-01-15 19:00:00"), isCancelled=false},
                    new Sublet {subletID = "abc6", reservationID = "wtv3", mainUserID="3", subUserID = "1", letPrice = 3.0m, startDate= DateTime.Parse("2020-01-14 19:00:00"), endDate = DateTime.Parse("2020-01-15 19:00:00"), isCancelled=false}

           };

            foreach (Sublet s in Sublets)
            {
                context.Sublets.Add(s);
            }
            context.SaveChanges();


            if (context.PaymentMethods.Any())
            {
                return;   // DB has been seeded
            }

            var PaymentMethods = new PaymentMethod[]
           {
                     new PaymentMethod{paymentMethodID = "1", name = "Wallet", userID = "1"},
                     new PaymentMethod{paymentMethodID = "2", name = "MockPayment", userID = "2", myUrl="https://localhost:44327/"},
                     new PaymentMethod{paymentMethodID = "3", name = "Paypal", userID = "3"}

           };

            foreach (PaymentMethod p in PaymentMethods)
            {
                context.PaymentMethods.Add(p);
            }
            context.SaveChanges();
        }
}
}
