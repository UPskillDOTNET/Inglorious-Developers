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
                new ParkingLot{parkingLotID=1, name="Parque Brito Capelo",owner="InRio",location="Rua Brito Capelo",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44353/"},
                new ParkingLot{parkingLotID=2, name="Parque da República", owner="Matosinhos", location="Avenida da República", capacity=125, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44350/"},

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
                    new User{Id="1", UserName="Mariana", name="Mariana Gomes", PasswordHash="AQAAAAEAACcQAAAAEN8nPp9f0odLuars7cGuvSUur2ZUZ5Z8to6gFZMo0S2vnArvzBWeBsIv6XowV9tLYQ==", Email="marianaribgomes@gmail.com", nif="111111111", paymentMethodID = "1"},
                    new User{Id="2", UserName= "Tiago", name="Tiago Azevedo", PasswordHash="AQAAAAEAACcQAAAAEN8nPp9f0odLuars7cGuvSUur2ZUZ5Z8to6gFZMo0S2vnArvzBWeBsIv6XowV9tLYQ==", Email="tiagomina.azevedo@gmail.com", nif="222222222", paymentMethodID = "1"},
                    new User{Id="3", UserName= "Joao", name="João Martins", PasswordHash="AQAAAAEAACcQAAAAEN8nPp9f0odLuars7cGuvSUur2ZUZ5Z8to6gFZMo0S2vnArvzBWeBsIv6XowV9tLYQ==", Email="joaom.vodafone@gmail.com", nif="333333333", paymentMethodID = "2"},
                    new User{Id="4", UserName= "Caio", name="Caio Reis", PasswordHash="AQAAAAEAACcQAAAAEN8nPp9f0odLuars7cGuvSUur2ZUZ5Z8to6gFZMo0S2vnArvzBWeBsIv6XowV9tLYQ==", Email= "caiocruzeiror@gmail.com",nif= "444444444", paymentMethodID = "1"},
                    new User{Id="5",UserName= "Sergio", name="Sergio Pinto", PasswordHash="AQAAAAEAACcQAAAAEN8nPp9f0odLuars7cGuvSUur2ZUZ5Z8to6gFZMo0S2vnArvzBWeBsIv6XowV9tLYQ==", Email= "sergio.valente.pinto@gmail.com" ,nif= "555555555", paymentMethodID = "1"},
                    new User{Id="6",name="Freddie Mercury",Email= "music@gmail.com",nif= "666666666", paymentMethodID = "1" },
                    new User{Id="7",name="Queen Elizabeth II",Email= "the.queen@gmail.com" ,nif= "777777777", paymentMethodID = "1"},
                    new User{Id="8",name="Afonso Henriques",Email= "afonso.rei@gmail.com",nif= "888888888", paymentMethodID = "1"},
                    new User{Id="9",name="Elon Musk",Email= "spaceX@gmail.com",nif= "999999999", paymentMethodID = "1"},
                    new User{Id="10",name="André André",Email= "vitoriasc@gmail.com" ,nif= "121121121", paymentMethodID = "1"},
                    new User{Id="11",name="Jô Soares",Email= "gordo@gmail.com",nif= "131131131", paymentMethodID = "1"}
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var centralReservations = new CentralReservation[]
            {
                    new CentralReservation{centralReservationID="ABC1",reservationID="ABC1",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-05-22 07:00:00"),endTime= DateTime.Parse("2021-05-22 08:00:00"),finalPrice= 10, hours = 1,parkingLotID=1,userID="1", parkingSpotID="A1"},
                    new CentralReservation{centralReservationID="ABC2",reservationID="ABC2",isCancelled=true, forSublet=true,startTime= DateTime.Parse("2021-01-31 07:00:00"),endTime= DateTime.Parse("2021-02-21 07:00:00"),finalPrice= 10,hours = 1,parkingLotID=2,userID="2", parkingSpotID="A3"},
                    new CentralReservation{centralReservationID="ABC3",reservationID="ABC3",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 19:00:00"),finalPrice= 10,hours = 1,parkingLotID=1,userID="3", parkingSpotID="O1"},
                    new CentralReservation{centralReservationID="ABC4",reservationID="ABC4",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-10-22 07:00:00"),endTime= DateTime.Parse("2021-10-22 10:00:00"),finalPrice= 10,hours = 1,parkingLotID=2,userID="4", parkingSpotID="E1"},
                    new CentralReservation{centralReservationID="ABC5",reservationID="ABC5",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 08:00:00"),finalPrice= 10,hours = 1,parkingLotID=2,userID="5", parkingSpotID="I1"},
                    new CentralReservation{centralReservationID="ABC6",reservationID="ABC6",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-08-22 12:00:00"),endTime= DateTime.Parse("2021-08-22 13:00:00"),finalPrice= 10,hours = 1,parkingLotID=1,userID="6", parkingSpotID="A3"},
                    new CentralReservation{centralReservationID="ABC7",reservationID="ABC7",isCancelled=true, forSublet=false,startTime= DateTime.Parse("2021-07-22 14:00:00"),endTime= DateTime.Parse("2021-07-22 15:00:00"),finalPrice= 10,hours = 1,parkingLotID=2,userID="7", parkingSpotID="A1"},
                    new CentralReservation{centralReservationID="ABC8",reservationID="ABC8",isCancelled=false,forSublet=false,startTime= DateTime.Parse("2021-06-22 18:00:00"),endTime= DateTime.Parse("2021-06-22 19:00:00"),finalPrice= 10,hours = 1,parkingLotID=1,userID="8", parkingSpotID="I1"},
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
