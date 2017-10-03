using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchPicker.Models
{
    public class AppState
    {
        public List<Restaurant> Restaurants { get; set; }
        public List<RestaurantSelection> PreviousSelections { get; set; }

        public List<Vote> Votes { get; set; }

        public AppState()
        {
            Restaurants = new List<Restaurant>();
            Votes = new List<Vote>();
        }
        public Restaurant PickRandom()
        {
            Random rand = new Random();
            int toSkip = rand.Next(0, this.Restaurants.Count());
            return Restaurants.Skip(toSkip).Take(1).First();
        }
        public RestaurantSelection Pick()
        {
            if (PreviousSelections.Any(i => i.Timestamp > DateTime.Today))
            {
                return PreviousSelections
                    .Where(i => i.Timestamp > DateTime.Today)
                    .OrderBy(i => i.Timestamp)
                    .First();
            }
            var Selection = new RestaurantSelection
            {
                Restaurant = PickRandom(),
                Timestamp = DateTime.Now,
                Username = HttpContext.Current.Request["LOGON_USER"]
            };
            PreviousSelections.Add(Selection);
            return Selection;
        }
        public Vote Vote(int id)
        {
            string Username = HttpContext.Current.Request["LOGON_USER"];
            if (!Restaurants.Any(i => i.ID == id))
            {
                throw new Exception("No restaurant exists with that id.");
            }
            Restaurant Restaurant = Restaurants.First(i => i.ID == id);
            var Vote = new Vote(Restaurant, Username);
            if (this.Votes.Any(i => i.Username == Username && i.Timestamp >= DateTime.Today))
            {
                throw new Exception("You cannot vote twice per day.");
            }
            Votes.Add(Vote);
            return Vote;

        }
    }
}