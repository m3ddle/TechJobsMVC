using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        public IActionResult Results(string column, string searchTerm)
        {
            List<Dictionary<string, string>> jobs = JobData.FindAll();
            List<Dictionary<string, string>> subresults = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> viewJobs = new List<Dictionary<string, string>> ();

            // Creating a new list to store the column titles for search results

            List<string> resultsColumns = new List<string>();

            foreach(string val in ListController.columnChoices.Values)
            {
                resultsColumns.Add(val);
                if (val == "All")
                {
                    // Empty braces means "do nothing" similar to pass in Python
                }

            }


            ViewBag.columns = ListController.columnChoices;
            ViewBag.jobs = viewJobs;
            ViewBag.title = "Search Results";
            ViewBag.resultsColumns = resultsColumns;

            if (column == null)
            {
                foreach (Dictionary<string, string> job in jobs)
                {
                    foreach (KeyValuePair<string, string> row in job)
                    {
                        if(searchTerm == null)
                        {
                            return View("Index");
                        }
                        if (row.Value.ToLower().Contains(searchTerm.ToLower()))
                        {

                            viewJobs.Add(job);

                        }
                    }
                }
            }
            else
            {
                foreach (Dictionary<string, string> job in jobs)
                {
                    foreach (KeyValuePair<string, string> row in job)
                    {
                        if (row.Key.ToLower().Contains(column))
                        {
                            subresults.Add(job);
                        }
                    }
                }

                foreach (Dictionary<string, string> job in subresults)
                {
                    foreach (KeyValuePair<string, string> row in job)
                    {
                        if (row.Value.ToLower().Contains(searchTerm.ToLower()))
                        {

                            viewJobs.Add(job);

                        }
                    }

                }
            }
            
            return View();
        }
    }
}
