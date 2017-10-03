using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchPicker.Models
{
    public class ResultsViewModel
    {
        public Restaurant Winner { get; set; }
        public List<VoteResult> Results { get; set; }
        public Restaurant TieBreaker { get; set; }

        public Vote YourVote { get; set; }
    }
}