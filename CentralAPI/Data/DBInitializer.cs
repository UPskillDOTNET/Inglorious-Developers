using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CentralAPI.Models;

namespace CentralAPI.Data
{

    public class DBInitializer
    {
        public static void Initialize(CentralAPIContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; //DB is seeded
            }

            // Look for any Users.
            var Users = new User[]
            {
                    new User{userID="1", name="Mariana Gomes", email="marianagomes@gmail.com", nif="221133486"},
                    new User{userID="2", name="Tiago Azevedo", email="tiagoazevedo@gmail.com", nif="113333986"},
                    new User{userID="3", name="João Martins", email="joaomartins@gmail.com", nif="231163886"},
            };

            foreach (User u in Users)
            {
                context.Users.Add(u);
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
                    new Transaction {transactionID="1", operation="deposit", userID="1",value=24.3m, transactionDate= DateTime.Parse("2020-02-22 19:30:00")},
                    new Transaction {transactionID="3", operation="deposit", userID="3",value=14.7m, transactionDate= DateTime.Parse("2020-01-14 19:00:00")},
                    new Transaction {transactionID="2", operation="withdraw", userID="2", value=30.3m, transactionDate= DateTime.Parse("2020-02-03 20:00:00")},
                    new Transaction {transactionID="4", operation="withdraw", userID="2", value=50.3m, transactionDate= DateTime.Parse("2020-02-08 23:00:00")}
            };

            foreach (Transaction t in Transactions)
            {
                context.Transactions.Add(t);
            }
            context.SaveChanges();
        }
    }
}
