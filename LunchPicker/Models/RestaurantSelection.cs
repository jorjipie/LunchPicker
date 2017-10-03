using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchPicker.Models
{
    public class RestaurantSelection
    {
        public Restaurant Restaurant { get; set; }
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
    }
}