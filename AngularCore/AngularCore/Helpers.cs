using Core.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCore
{
    public class Helpers
    {
        private static Random rand = new Random();

        private static string GetRandom(IList<string> items)
        {
            return items[rand.Next(items.Count)];
        }

        internal static string GenerateCustomerName()
        {
            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bizSuffix);
            return prefix + " " + suffix;
        }

        internal static string GenerateCustomerState()
        {
            var state = GetRandom(states);
            return state;
        }

        internal static string GenerateCustomerEmail(string name)
        {
            return (name.Replace(" ", "").Replace("'", "") + "@gmail.com").ToLower();
        }

        internal static decimal GenerateRandomOrderTotal()
        {
            return rand.Next(100, 5000);
        }

        internal static DateTime GenerateRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return start + newSpan;
        }

        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if(timePassed < minLeadTime)
            {
                return null;
            }
            return orderPlaced.AddDays(rand.Next(7,14));
        }

        internal static bool GenerateRandomBool()
        {
            return rand.NextDouble() >= 0.5 ? true : false;
        }

        internal static string GenerateRandomServerName()
        {
            var prefix = GetRandom(envNames);
            var suffix = GetRandom(envs);
            return prefix + " " + suffix;
        }

        private static readonly List<string> bizPrefix = new List<string>()
        {
            "ABC",
            "Mike's",
            "Bolshevik",
            "National",
            "Free Range",
            "Polished",
            "Fast",
            "Wall",
            "Cheap",
            "Great",
            "America's",
            "Refurbished",
            "Clean",
            "Pretend",
            "Cool"
        };

        private static readonly List<string> bizSuffix = new List<string>()
        {
            "Tools",
            "Corporation",
            "Social",
            "LLC",
            "Parts",
            "Services",
            "Hosting",
            "Printing",
            "Fuel",
            "Goods",
            "Ammo",
            "Farms",
            "Devices",
            "Fishing",
            "Realestate"
        };

        private static readonly List<string> states = new List<string>()
        {
            "FL",
            "AL",
            "NY",
            "CA",
            "WA",
            "MA",
            "TX",
            "AZ",
            "GA",
            "VA",
            "OR",
            "UT",
            "NC",
            "SC",
            "OH"
        };

        private static readonly List<string> envNames = new List<string>()
        {
            "Customer Portal",
            "Admin Portal",
            "Analytics App",
            "Mobile App"
        };

        private static readonly List<string> envs = new List<string>()
        {
            "Prod",
            "Dev",
            "Test",
            "Ver"
        };
    }
}
