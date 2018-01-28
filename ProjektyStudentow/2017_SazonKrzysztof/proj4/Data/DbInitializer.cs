using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proj4.Models;

namespace proj4.Data {
    public class DbInitializer {
        public static void Initialize(MusicContext context) {
            context.Database.EnsureCreated();

            if (context.Listeners.Any())
                return;

            var listeners = new Listener[] {
                new Listener {Name="Ola", DateOfBirth=DateTime.Parse("1999-12-01")},
                new Listener {Name="Malgorzata", DateOfBirth=DateTime.Parse("1999-12-01")},
                new Listener {Name="Jan", DateOfBirth=DateTime.Parse("1999-12-01")},
                new Listener {Name="Julia", DateOfBirth=DateTime.Parse("1999-12-01")},
            };
            foreach (Listener l in listeners)
                context.Listeners.Add(l);
            context.SaveChanges();

            var bands = new Band[] {
                new Band {BandID="Tool", City="New York", FormationDate=DateTime.Parse("2012-01-01"), Genre=Genre.art},
                new Band {BandID="Jamie xx", City="London", FormationDate=DateTime.Parse("2017-01-01"), Genre=Genre.folk},
                new Band {BandID="Janes Addiciton", City="Los Angeles", FormationDate=DateTime.Parse("1989-01-01"), Genre=Genre.pop},
                new Band {BandID="Aesop Rock", City="Los Angeles", Genre=Genre.pop},
            };
            foreach (Band b in bands)
                context.Bands.Add(b);
            context.SaveChanges();

            var bandsListeners = new BandListener[] {
                new BandListener {ListenerID=1, BandID="Tool", Note=4},
                new BandListener {ListenerID=1, BandID="Janes Addiciton", Note=2},
                new BandListener {ListenerID=2, BandID="Aesop Rock", Note=4},
                new BandListener {ListenerID=3, BandID="Jamie xx", Note=5},
                new BandListener {ListenerID=4, BandID="Aesop Rock", Note=2},
            };
            foreach (BandListener bl in bandsListeners)
                context.BandsListeners.Add(bl);
            context.SaveChanges();

            var tours = new Tour[] {
                new Tour {BandID="Tool", City="Gdynia", Date=DateTime.Parse("2017-06-01")},
                new Tour {BandID="Tool", City="Warszawa", Date=DateTime.Parse("2017-07-01")},
                new Tour {BandID="Tool", City="Katowice", Date=DateTime.Parse("2017-01-01")},
                new Tour {BandID="Jamie xx", City="Berlin", Date=DateTime.Parse("2017-02-01")},
                new Tour {BandID="Jamie xx", City="Moscow", Date=DateTime.Parse("2017-03-01")},
                new Tour {BandID="Aesop Rock", City="Peking", Date=DateTime.Parse("2017-04-01")},
                new Tour {BandID="Aesop Rock", City="Oslo", Date=DateTime.Parse("2017-05-01")},
            };

            foreach (var t in tours) {
                context.Tours.Add(t);
            }
            context.SaveChanges();
        }
    }
}
