using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MidiMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiMarket.DAL
{
    class MarketInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MarketContext>
    {
        protected override void Seed(MarketContext context)
        {
            addAdminAccount(context);

            #region doefiniowane produktow
            var japko = new Product { Name = "jablko", Description = "...", Price = 1.0, Picture = "jablko.png", Category = Category.owoce };
            var bananek = new Product { Name = "bananek", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Crooked_Banana_2730385.png", Category = Category.owoce };
            var arbuz = new Product { Name = "arbuz", Description = "...", Price = 4.0, Picture = "watermellon.png", Category = Category.owoce };
            var orange = new Product { Name = "orange", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Juicy_2730373.png", Category = Category.owoce };
            var pomidor = new Product { Name = "pomidor", Description = "...", Price = 4.0, Picture = "14986_Tomato.png", Category = Category.owoce };

            var rogal = new Product { Name = "rogal", Description = "...", Price = 4.0, Picture = "Croissant.png", Category = Category.wypieki };
            var chleb = new Product { Name = "chleb", Description = "...", Price = 4.0, Picture = "Bread.png", Category = Category.wypieki };
            var ciasto = new Product { Name = "ciasto", Description = "...", Price = 4.0, Picture = "Pie.png", Category = Category.wypieki };
            var babeczka = new Product { Name = "ciasto", Description = "...", Price = 4.0, Picture = "Cupcake.png", Category = Category.wypieki };

            var whiskey = new Product { Name = "whiskey", Description = "...", Price = 4.0, Picture = "whiskey.png", Category = Category.napoje };
            var vodka = new Product { Name = "vodka", Description = "...", Price = 4.0, Picture = "vodka.png", Category = Category.napoje };
            var piwerko = new Product { Name = "piwerko", Description = "...", Price = 4.0, Picture = "piwerko.png", Category = Category.napoje };

            var chomik = new Product { Name = "chomik", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Squeak_2730365.png", Category = Category.zwierzeta };
            var rekin = new Product { Name = "rekin", Description = "...", Price = 4.0, Picture = "1496956910_wireshark.png", Category = Category.zwierzeta };
            var lisek = new Product { Name = "lisek", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Filthy_FOx_2730380.png", Category = Category.zwierzeta };
            var zyrafa = new Product { Name = "żyrafa", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_GIrrafe_2730376.png", Category = Category.zwierzeta };
            var wilk = new Product { Name = "wilk", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Fierce_2730381.png", Category = Category.zwierzeta };
            var psajdak = new Product { Name = "psajdak", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Screech_Psyduck_2730368.png", Category = Category.zwierzeta };
            var rozgwiazda = new Product { Name = "rozgwiazda", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Starfish_2730364.png", Category = Category.zwierzeta };
            var meduza = new Product { Name = "meduza", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Jellyfish_2730374.png", Category = Category.zwierzeta };

            var miecz = new Product { Name = "miecz", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Sword_2730363.png", Category = Category.zabawki };
            var kostka = new Product { Name = "kostka", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Teeming_2730362.png", Category = Category.zabawki };
            var totem = new Product { Name = "totem", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Climb_2730387.png", Category = Category.zabawki };
            var camaro = new Product { Name = "camaro", Description = "...", Price = 4.0, Picture = "14970_camaro_64.png", Category = Category.zabawki };
            var paletka = new Product { Name = "paletka", Description = "...", Price = 4.0, Picture = "149695_Table_tennis.png", Category = Category.zabawki };
            var lodka = new Product { Name = "łodeczka", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Ship_2730366.png", Category = Category.zabawki };
            var ufo = new Product { Name = "ufo", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Misterious_2730371.png", Category = Category.zabawki };
            var kapsula = new Product { Name = "kapsuła", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Deep_Dive_Suit_2730384.png", Category = Category.zabawki };
            var rower = new Product { Name = "Bike", Description = "...", Price = 4.0, Picture = "Bike.png", Category = Category.zabawki };
            var kaczuszka = new Product { Name = "kaczuszka", Description = "...", Price = 4.0, Picture = "149041_Duckling.png", Category = Category.zabawki };
            var maska = new Product { Name = "maska", Description = "...", Price = 4.0, Picture = "iconfinder_Inkcontober_Mask_Juggernaut_2730372.png", Category = Category.zabawki };
            #endregion

            var listaProduktow = new List<Product> {
                japko, bananek, arbuz, orange, rogal, chleb, ciasto, babeczka, whiskey , vodka, piwerko, chomik, rekin, lisek, zyrafa, wilk, psajdak, rozgwiazda, miecz, kostka, totem, camaro, paletka, lodka, ufo
            };
            listaProduktow.ForEach(pdkt => context.Products.Add(pdkt));

            // testowi uzytkownicy
            var user1 = new ApplicationUser { UserName = "user1", Email = "mail@user1.pl" };
            var user2 = new ApplicationUser { UserName = "user2", Email = "mail@user2.pl" };

            // spreparowane zamowienia
            var order1 = new Order { ApplicationUser = user1, OrderDate = DateTime.Parse("2002-09-01") };
            var order2 = new Order { ApplicationUser = user2, OrderDate = DateTime.Parse("2010-09-01") };

            var listaZamowien = new List<Order>
            {
                order1, order2
            };
            listaZamowien.ForEach(ordr => context.Orders.Add(ordr));

            // doodawanie pozycji do zamówień
            var zamowieniePozycje = new List<OrderItem>
            {
                new OrderItem { Order = order1, Product = japko, Amount = 3 },
                new OrderItem { Order = order1, Product = chomik, Amount = 2 },
                new OrderItem { Order = order2, Product = rekin, Amount = 1 },
                new OrderItem { Order = order2, Product = bananek, Amount = 22 }
            };
            zamowieniePozycje.ForEach(poz => context.OrderItems.Add(poz));

            // zapisz zmiany
            context.SaveChanges();
        }

        /// <summary>
        /// dodawanie i konfigurowanie konta administratora
        /// </summary>
        private void addAdminAccount(MarketContext context)
        {
            // dodawanie roli
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            role.Name = "Admin";
            roleManager.Create(role);

            // dodawanie konta admin + rola administracyjna
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var admin = new ApplicationUser { UserName = "admin@admin.pl", Email = "admin@admin.pl", };
            UserManager.Create(admin, "Haslo123!");
            UserManager.AddToRole(admin.Id, "Admin");
        }
    }
}
