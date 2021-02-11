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
                new ParkingLot{name="Parque da República", owner="NorteShopping", location="Avenida da República", capacity=125, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44350/api"},
                new ParkingLot{name="Parque Brito Capelo",owner="InRio",location="Rua Brito Capelo",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44353/api"},

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
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var centralReservations = new CentralReservation[]
            {
                    new CentralReservation{reservationID="ABC1",isCancelled=false,startTime= DateTime.Parse("2021-05-22 07:00:00"),endTime= DateTime.Parse("2021-05-22 08:00:00"),finalPrice= 10, parkingLotID=1,userID="1"},
                    new CentralReservation{reservationID="ABC2",isCancelled=true,startTime= DateTime.Parse("2021-01-31 07:00:00"),endTime= DateTime.Parse("2021-08-22 09:00:00"),finalPrice= 10,parkingLotID=2,userID="2"},
                    new CentralReservation{reservationID="ABC3",isCancelled=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 19:00:00"),finalPrice= 10,parkingLotID=1,userID="3"},
                    new CentralReservation{reservationID="ABC4",isCancelled=false,startTime= DateTime.Parse("2021-10-22 07:00:00"),endTime= DateTime.Parse("2021-10-22 10:00:00"),finalPrice= 10,parkingLotID=2,userID="4"},
                    new CentralReservation{reservationID="ABC5",isCancelled=false,startTime= DateTime.Parse("2021-09-22 07:00:00"),endTime= DateTime.Parse("2021-09-22 08:00:00"),finalPrice= 10,parkingLotID=2,userID="5"},
                    new CentralReservation{reservationID="ABC6",isCancelled=false,startTime= DateTime.Parse("2021-08-22 12:00:00"),endTime= DateTime.Parse("2021-08-22 13:00:00"),finalPrice= 10,parkingLotID=1,userID="6"},
                    new CentralReservation{reservationID="ABC7",isCancelled=true,startTime= DateTime.Parse("2021-07-22 14:00:00"),endTime= DateTime.Parse("2021-07-22 15:00:00"),finalPrice= 10,parkingLotID=2,userID="7"},
                    new CentralReservation{reservationID="ABC8",isCancelled=false,startTime= DateTime.Parse("2021-06-22 18:00:00"),endTime= DateTime.Parse("2021-06-22 19:00:00"),finalPrice= 10,parkingLotID=1,userID="8"},
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

            if (context.Transactions.Any())
            {
                return;   // DB has been seeded
            }

            var Transactions = new Transaction[]
            {
                    new Transaction {operation="deposit", userID="1",value=24.3m, transactionDate= DateTime.Parse("2020-02-22 19:30:00")},
                    new Transaction {operation="deposit", userID="3",value=14.7m, transactionDate= DateTime.Parse("2020-01-14 19:00:00")},
                    new Transaction {operation="withdraw", userID="2", value=30.3m, transactionDate= DateTime.Parse("2020-02-03 20:00:00")},
                    new Transaction {operation="withdraw", userID="2", value=50.3m, transactionDate= DateTime.Parse("2020-02-08 23:00:00")},
                    new Transaction {operation="deposit", userID="2", value=100.3m, transactionDate= DateTime.Parse("2020-03-08 23:00:00")}
            };

            foreach (Transaction t in Transactions)
            {
                context.Transactions.Add(t);
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


            if (context.ParkingSpots.Any())
            {
                return;
            }

            var ParkingSpots = new ParkingSpot[]
            {
                 new ParkingSpot{parkingSpotID="A1",priceHour=0.250m,isPrivate=true, floor=1, parkingLotID=1},
                 new ParkingSpot{parkingSpotID="E1",priceHour=0.5m,isPrivate=false, floor=2, parkingLotID=2},
                 new ParkingSpot{parkingSpotID="I1",priceHour=0.9m,isPrivate=true, floor=1, parkingLotID=2},
                 new ParkingSpot{parkingSpotID="O1",priceHour=1.00m,isPrivate=false, parkingLotID=3},
                 new ParkingSpot{parkingSpotID="A3",priceHour=0.25m,isPrivate=false, parkingLotID=1},

            };
            context.ParkingSpots.AddRange(ParkingSpots);
            context.SaveChanges();
        }

            if (context.Payments.Any())
            {
                return;   // DB has been seeded
            }
            // var Payments = new Payment[]
            //{
            //         new Payment {},
            //         new Payment {},
            //         new Payment {}

            //};

            // foreach (Payment p in Payments)
            // {
            //     context.Sublets.Add(p);
            // }
            // context.SaveChanges();
        }
}
}
