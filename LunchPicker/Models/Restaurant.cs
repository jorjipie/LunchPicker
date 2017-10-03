using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchPicker.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public Restaurant(string Name, int ID, string Slogan)
        {
            this.Name = Name;
            this.ID = ID;
            this.Slogan = Slogan;
        }
    }
}