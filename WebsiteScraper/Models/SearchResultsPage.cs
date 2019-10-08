using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteScraper.Models
{
    public class SearchResultsPage
    {
        public List<PrimarySearchResult> SearchResults { get; set; }
        
        public List<string> RelatedSearchTerms { get; set; }
    }
}