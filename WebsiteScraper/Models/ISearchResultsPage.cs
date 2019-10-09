using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteScraper.Models
{
    public interface ISearchResultsPage
    {
        List<PrimarySearchResult> SearchResults { get; set; }
        
        List<string> RelatedSearchTerms { get; set; }
    }
}