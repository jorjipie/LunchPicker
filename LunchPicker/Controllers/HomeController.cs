using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LunchPicker.Models;

namespace LunchPicker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var state = AppStateManager.GetState();
            var VotesToday = state.Votes.Where(i => i.Timestamp >= DateTime.Today);
            //if (VotesToday.Any(i => i.Username == Request["LOGON_USER"]))
            //{
            //    return RedirectToAction("Results");
            //}
            return View(state.Restaurants.OrderBy(i => i.Name));
        }
        
        private Restaurant PickRandom(List<Restaurant> Restaurants)
        {
            Random rand = new Random();
            int toSkip = rand.Next(0, Restaurants.Count());
            return Restaurants.Skip(toSkip).Take(1).First();
        }
        public ActionResult Results()
        {
            var ResultsViewModel = new ResultsViewModel();
            ResultsViewModel.Results = new List<VoteResult>();
            var state = AppStateManager.GetState();
            var Votes = state.Votes.Where(j => j.Timestamp >= DateTime.Today);

            if (Votes.Count() == 0)
            {
                ResultsViewModel.Winner = PickRandom(state.Restaurants);
                ResultsViewModel.TieBreaker = ResultsViewModel.Winner;
                return View(ResultsViewModel);
            }

            //show what current user voted for.  
            if (Votes.Any(i => i.Username == Request["LOGON_USER"]))
            {
                ResultsViewModel.YourVote = Votes.First(i => i.Username == Request["LOGON_USER"]);
            }

            //make list of result tallies
            foreach (var k in state.Restaurants)
                ResultsViewModel.Results.Add(new VoteResult { Count = 0, Restaurant = k });

            //fill with votes
            foreach (var k in Votes)
            {
                var result = ResultsViewModel.Results.First(j => j.Restaurant.ID == k.Restaurant.ID);
                result.Count++;
            }

            //remove tallies with 0 votes. 
            ResultsViewModel.Results.RemoveAll(i => i.Count == 0);

            //if there's a tie for first, figure out how many tied.
            var WinCount = ResultsViewModel.Results.OrderByDescending(j => j.Count).First().Count;
            var WinTies = (from j in ResultsViewModel.Results
                           where j.Count == WinCount
                           select j.Restaurant).ToList();

            //then pick randomly from the ties.
            if (WinTies.Count() > 1)
            {
                ResultsViewModel.Winner = PickRandom(WinTies);
                ResultsViewModel.TieBreaker = ResultsViewModel.Winner;
                return View(ResultsViewModel);
            }
            //If there's a clear winner, return it.
            ResultsViewModel.Winner = WinTies.First();
            ResultsViewModel.TieBreaker = null;
            return View(ResultsViewModel);

        }
        
        public ActionResult Vote(int id)
        { 
            var Username = Request["LOGON_USER"];
            var state = AppStateManager.GetState();
            if (!state.Restaurants.Any(i => i.ID == id))
            {
                throw new Exception("No state found with that ID.");
            }
            if (state.Votes.Any(i => i.Timestamp >= DateTime.Today && i.Username == Username))
            {
                return RedirectToAction("Results");
            }
            state.Vote(id);
            AppStateManager.SaveState(state);
            return RedirectToAction("Results");

        }
    }
}