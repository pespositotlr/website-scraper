using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebsiteScraper.Helpers;
using WebsiteScraper.Models;

namespace WebsiteScraper.Builders
{
    public interface ISearchResultsPageBuilder
    {

        SearchResultsPage Page { get; set; }
        
        void BuildSearchResultsPage(string searchTerms, int pageSize = 100);

        void BuildSearchResultsSearchUrl(string searchTerms, int pageSize);

        void BuildSearchResultsRawHtml();

        void BuildSearchResultItems();

        void BuildSubResults(PrimarySearchResult primaryResult, string primaryResultHtml);

        void BuildRelatedSearchTerms();

    }
}