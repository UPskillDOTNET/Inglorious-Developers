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
                new ParkingLot{parkingLotID=1, name="Parque Brito Capelo",owner="InRio",location="Rua Brito Capelo, Porto",capacity=250,openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44353/", imageURL="https://upload.wikimedia.org/wikipedia/commons/8/85/Metro_do_Porto_-_Matosinhos_-_Rua_Brito_Capelo_%288253809282%29.jpg"},
                new ParkingLot{parkingLotID=2, name="Parque da República", owner="Município de Matosinhos", location="Avenida da República, Porto", capacity=125, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="https://localhost:44350/", imageURL="https://upload.wikimedia.org/wikipedia/commons/9/97/Av._da_Rep%C3%BAblica%2C_Vila_Nova_de_Gaia_%286167760426%29.jpg"},
                new ParkingLot{parkingLotID=3, name="Parque do Palácio da Justiça", owner="Município de Porto", location="Rua Campo dos Mártires da Pátria, Porto", capacity=235, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="", imageURL="https://upload.wikimedia.org/wikipedia/commons/8/8a/Palacio_Justica_%28Porto%29.JPG"},
                new ParkingLot{parkingLotID=4, name="Parque da Ribeira", owner="Município de Porto", location="Praça Infante D. Henrique, Porto", capacity=155, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="", imageURL="http://unindopontos.com.br/wp/wp-content/uploads/2018/11/Ribeira.jpeg"},
                new ParkingLot{parkingLotID=5, name="Parque do Castelo do Queijo", owner="Município de Porto", location="Praça Gonçalves Zarco, Porto", capacity=103, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="", imageURL="https://www.oportoguide.pt/foto/castelo-do-queijo-21_xl.jpg"},
                new ParkingLot{parkingLotID=6, name="Parque da Estação Ferroviária", owner="Município de Lisboa", location="Estação Santa Apolónia, Lisboa", capacity=95, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="", imageURL="https://viagemparalisboa.com/wp-content/uploads/2018/11/trem-em-Lisboa.jpg"},
                new ParkingLot{parkingLotID=7, name="Parque da Baixa-Chiado", owner="Município de Lisboa", location="Baixa-Chiado, Lisboa", capacity=185, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="", imageURL="https://d2z1ddvkf0gb65.cloudfront.net/wp-content/uploads/Largo-do-Chiado1.jpg"},
                new ParkingLot{parkingLotID=8, name="Parque da Avenida Central", owner="Município de Braga", location="Avenida Central, Braga", capacity=75, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="", imageURL="http://4.bp.blogspot.com/-gRCZaIsXD9w/VZYLHkycMwI/AAAAAAAAHVA/56Od_-yLqBg/s1600/9395_170201539838121_400904376_n.jpg"},
                new ParkingLot{parkingLotID=9, name="Parque do Marquês", owner="Município de Porto", location="Rua de João Pedro Ribeiro, Porto", capacity=109, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="", imageURL="https://photos1.blogger.com/img/291/1346/1024/Marques-1.jpg"},
                new ParkingLot{parkingLotID=10, name="Parque da Trindade", owner="Município de Porto", location="Rua de Fernandes Tomás, Porto", capacity=176, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="", imageURL="https://portaldeportugal.com/wp-content/uploads/2019/04/Capilla_de_las_Almas_Oporto_Portugal_2012-05-09_DD_01.jpg"},
                new ParkingLot{parkingLotID=11, name="Parque do Palácio de Cristal", owner="Município de Porto", location="Rua Jorge de Viterbo Ferreira, Porto", capacity=54, openingTime= DateTime.Parse("2020-02-22 07:00:00"),closingTime= DateTime.Parse("2999-02-22 19:00:00"),myURL="", imageURL="https://media-cdn.tripadvisor.com/media/photo-s/13/98/fe/82/palacio-de-cristal.jpg"},

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
