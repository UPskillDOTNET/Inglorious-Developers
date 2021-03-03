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

            if (context.PaymentMethods.Any())
            {
                return;   // DB has been seeded
            }

            var PaymentMethods = new PaymentMethod[]
           {
                     new PaymentMethod{paymentMethodID = "1", name = "Wallet"},
                     new PaymentMethod{paymentMethodID = "2", name = "MockPayment"},
                     new PaymentMethod{paymentMethodID = "3", name = "Paypal"}

           };

            foreach (PaymentMethod p in PaymentMethods)
            {
                context.PaymentMethods.Add(p);
            }
            context.SaveChanges();

            if (context.ParkingLots.Any())
            {
                return; //DB is seeded
            }

            var parkingLots = new ParkingLot[]
            {
                new ParkingLot{name="Parque Brito Capelo",owner="InRio",location="Rua Brito Capelo",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44353/"},
                new ParkingLot{name="Parque da República", owner="NorteShopping", location="Avenida da República", capacity=125, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44350/"},

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
                    new User{userID="1", Email="marianaribgomes@gmail.com",  FirstName="Mariana", LastName="Gomes",  password="1078c9f4efa5678ab69f942e537fbe41", nif="111111111", paymentMethodID = "1"},
                    new User{userID="2", Email="tiagomina.azevedo@gmail.com",  FirstName="Tiago", LastName="Azevedo",  password="1078c9f4efa5678ab69f942e537fbe41", nif="222222222", paymentMethodID = "1"},
                    new User{userID="3", Email="joaom.vodafone@gmail.com",  FirstName="João", LastName="Martins",  password="1078c9f4efa5678ab69f942e537fbe41", nif="333333333", paymentMethodID = "1"},
                    new User{userID="4", Email="caiocruzeiror@gmail.com",  FirstName="Caio", LastName="Reis",  password="1078c9f4efa5678ab69f942e537fbe41", nif="444444444", paymentMethodID = "1"},
                    new User{userID="5", Email="sergio.valente.pinto@gmail.com",  FirstName="Sergio", LastName="Pinto",  password="1078c9f4efa5678ab69f942e537fbe41", nif="555555555", paymentMethodID = "1"},
                    new User{userID="6", Email="gordo@gmail.com",  FirstName="Jô", LastName="Soares",  password="1078c9f4efa5678ab69f942e537fbe41", nif="666666666", paymentMethodID = "1"},

            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var centralReservations = new CentralReservation[]
            {
                    new CentralReservation{centralReservationID="ABC1",reservationID="ABC1",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-05-22 07:00:00"),endTime= DateTime.Parse("2021-05-22 08:00:00"),finalPrice= 10, hours = 1,parkingLotID=1,userID="1", parkingSpotID="A1"},
                    new CentralReservation{centralReservationID="ABC2",reservationID="ABC2",isCancelled=true, forSublet=true,startTime= DateTime.Parse("2021-01-31 07:00:00"),endTime= DateTime.Parse("2021-08-22 09:00:00"),finalPrice= 10,hours = 1,parkingLotID=2,userID="2", parkingSpotID="A3"},
                    new CentralReservation{centralReservationID="ABC3",reservationID="ABC3",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 19:00:00"),finalPrice= 10,hours = 1,parkingLotID=1,userID="3", parkingSpotID="O1"},
                    new CentralReservation{centralReservationID="ABC4",reservationID="ABC4",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-10-22 07:00:00"),endTime= DateTime.Parse("2021-10-22 10:00:00"),finalPrice= 10,hours = 1,parkingLotID=2,userID="4", parkingSpotID="E1"},
                    new CentralReservation{centralReservationID="ABC5",reservationID="ABC5",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 08:00:00"),finalPrice= 10,hours = 1,parkingLotID=2,userID="5", parkingSpotID="I1"},
                    new CentralReservation{centralReservationID="ABC6",reservationID="ABC6",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-08-22 12:00:00"),endTime= DateTime.Parse("2021-08-22 13:00:00"),finalPrice= 10,hours = 1,parkingLotID=1,userID="6", parkingSpotID="A3"},
                    new CentralReservation{centralReservationID="ABC7",reservationID="ABC7",isCancelled=true, forSublet=false,startTime= DateTime.Parse("2021-07-22 14:00:00"),endTime= DateTime.Parse("2021-07-22 15:00:00"),finalPrice= 10,hours = 1,parkingLotID=2,userID="6", parkingSpotID="A1"},
                    new CentralReservation{centralReservationID="ABC8",reservationID="ABC8",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-06-22 18:00:00"),endTime= DateTime.Parse("2021-06-22 19:00:00"),finalPrice= 10,hours = 1,parkingLotID=1,userID="5", parkingSpotID="I1"},
                    new CentralReservation{centralReservationID="ABC9",reservationID="ABC9",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-02-17 10:00:00"),endTime= DateTime.Parse("2021-02-19 19:00:00"),finalPrice= 10,hours = 1,parkingLotID=1,userID="1", parkingSpotID="A1"}
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
                    //new Sublet {subletID = "abc4", reservationID = "wtv", mainUserID="1", subUserID = "2", isCancelled=false},
                    //new Sublet {subletID = "abc5", reservationID = "wtv2", mainUserID="2", subUserID = "3", isCancelled=false},
                    //new Sublet {subletID = "abc6", reservationID = "wtv3", mainUserID="3", subUserID = "1", isCancelled=false}

           };

            foreach (Sublet s in Sublets)
            {
                context.Sublets.Add(s);
            }
            context.SaveChanges();


          
        }
}
}
