using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CentralAPI.Models;

namespace CentralAPI.Data {
    
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
        }
    }
}
