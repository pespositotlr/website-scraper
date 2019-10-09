using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebsiteScraper.Builders;
using WebsiteScraper.Helpers;
using WebsiteScraper.Models;
using WebsiteScraper.ViewModels;

namespace WebsiteScraper.Controllers
{
    /*
    Description: This was originally part of a coding challenge to create an over-designed application using .NET MVC for seeing where a company name appeared in the top 100 google search results for certain keywords.
    Part of the requirements were to avoid using HTMLAgilityPack or a similar service to scrape the data on the website, but to still follow clean coding practices.
    Also part of the requirements were to make it a single-page application using KnockoutJS to handle the front-end.
    Created By: Peter Esposito
    Date: 10/5/2019
    */
    public class HomeController : Controller
    {
        public ActionResult Index(string searchKeywords = "online title search", string textToFind = "courtHouseDirect", int resultsToCheck = 100)
        {
            ViewBag.Title = "Google Search Result Scraper";

            return View(GetScraperResultViewModel(searchKeywords, textToFind, resultsToCheck));
        }

        [HttpPost]
        public JsonResult GetSearchResults(string SearchKeywords = "online title search", string TextToFind = "www.infotrack.com.au", int ResultsToCheck = 100)
        {
            return Json(GetScraperResultViewModel(SearchKeywords, TextToFind, ResultsToCheck), JsonRequestBehavior.AllowGet);
        }

        public ScraperResultViewModel GetScraperResultViewModel(string searchKeywords, string textToFind, int resultsToCheck)
        {
            var resultViewModel = new ScraperResultViewModel();

            resultViewModel.SearchKeywords = searchKeywords;
            resultViewModel.TextToFind = textToFind;
            resultViewModel.ResultsToCheck = resultsToCheck;

            var searchResultsPageBuiler = new SearchResultsPageBuilder(new SearchResultsPage());

            searchResultsPageBuiler.BuildSearchResultsPage(searchKeywords, resultsToCheck);

            resultViewModel.MatchingResults = SearchResultsPageHelper.GetSearchResultsContainingString((SearchResultsPage)searchResultsPageBuiler.Page, textToFind);

            return resultViewModel;
        }

    }
}