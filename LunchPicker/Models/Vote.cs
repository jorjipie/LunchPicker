using System;

namespace LunchPicker.Models
{
    public class Vote
    {
        public Restaurant Restaurant { get; set; }
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }

        public Vote(Restaurant Restaurant, string Username)
        {
            this.Restaurant = Restaurant;
            this.Username = Username;
            Timestamp = DateTime.Now;
        }
    }
}