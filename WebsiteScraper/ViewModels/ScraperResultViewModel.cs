using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebsiteScraper.Models;

namespace WebsiteScraper.ViewModels
{
    public class ScraperResultViewModel
    {
        public string SearchKeywords { get; set; }
        public string TextToFind { get; set; }
        public int ResultsToCheck { get; set; }
        public List<PrimarySearchResult> MatchingResults { get; set; }

        public string SearchResultRankings
        {
            get
            {
                if (MatchingResults.Count == 0)
                    return "0";
                                               
                return string.Join(",", MatchingResults.Select(x => x.SequenceNumber));

            }
        }
    }
}