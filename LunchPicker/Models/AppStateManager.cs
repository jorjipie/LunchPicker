using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace LunchPicker.Models
{
    public class AppStateManager
    {
        public static AppState GetState()
        {
            var state = new AppState();
            if (HttpContext.Current.Application["App"] != null)
            {
                state = (AppState)HttpContext.Current.Application["App"];
            }
            else
            {
                var filepath = HttpContext.Current.Server.MapPath("~/App_Data/app_state.json");
                if (System.IO.File.Exists(filepath))
                    state = Newtonsoft.Json.JsonConvert.DeserializeObject<AppState>(File.ReadAllText(filepath));
            }
            if (state.Restaurants.Count == 0)
            {
                state.Restaurants = SeedRestaurants();
            }
            return state;
        }
        public static void SaveState(AppState state)
        {
            HttpContext.Current.Application["App"] = state;

            var Serializer = new Newtonsoft.Json.JsonSerializer();
            using (TextWriter writer = File.CreateText(HttpContext.Current.Server.MapPath("~/App_Data/app_state.json")))
            {
                Serializer.Serialize(writer, state, typeof(AppState));
            }
        }
        private static List<Restaurant> SeedRestaurants()
        {
            var Restaurants = new List<Restaurant>();
            Restaurants.Add(new Restaurant("Pamela's", 1, "Tasty pancake time!"));
            Restaurants.Add(new Restaurant("Panera", 2, "It's food."));
            Restaurants.Add(new Restaurant("Subway", 3, "Eat fresh!"));
            Restaurants.Add(new Restaurant("Wendy's", 4, "Kelli will be happy!"));
            Restaurants.Add(new Restaurant("China Palace", 5, "Mmm vegetable protein"));
            Restaurants.Add(new Restaurant("Giant Eagle", 6, "Sushi time!"));
            Restaurants.Add(new Restaurant("Shady Grove", 7, "Sammiches"));
            Restaurants.Add(new Restaurant("Tan Izakaya", 8, "Glass marble soda time!"));
            Restaurants.Add(new Restaurant("Sushi Too", 10, "Sushi as well"));
            Restaurants.Add(new Restaurant("Steel Cactus Cantina", 11, "TACOS"));
            return Restaurants;
        }
    }
}