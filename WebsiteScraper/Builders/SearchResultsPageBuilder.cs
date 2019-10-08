using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebsiteScraper.Helpers;
using WebsiteScraper.Models;

namespace WebsiteScraper.Builders
{
    public class SearchResultsPageBuilder
    {

        public SearchResultsPage Page = new SearchResultsPage();

        private string SearchResultsSearchUrl = "";

        private string SearchResultsRawHtml = "";

        private Dictionary<int, string> SearchResultHtmlBlocks = new Dictionary<int, string>();

        public void BuildSearchResultsPage(string searchTerms, int pageSize = 100)
        {
            if (String.IsNullOrEmpty(searchTerms))
                throw new ArgumentNullException();

            BuildSearchResultsSearchUrl(searchTerms, pageSize);

            BuildSearchResultsRawHtml();
                        
            BuildSearchResultItems();

            BuildRelatedSearchTerms();

        }

        public void BuildSearchResultsSearchUrl(string searchTerms, int pageSize)
        {
            SearchResultsSearchUrl = HtmlScraperHelper.GetGoogleSearchUrl(searchTerms, pageSize);
        }

        public void BuildSearchResultsRawHtml()
        {
            SearchResultsRawHtml = HtmlScraperHelper.GetHtmlOfTargetUrl(SearchResultsSearchUrl);
        }

        public void BuildSearchResultItems()
        {

            if (String.IsNullOrEmpty(SearchResultsRawHtml))
                throw new ObjectNotFoundException("No raw HTML found from search website");

            //Break page HTML into blocks for each search result
            SearchResultHtmlBlocks = GoogleSearchResultsHelper.GetSearchResultHtmlBlocks(SearchResultsRawHtml);

            if (SearchResultHtmlBlocks.Count > 0)
                Page.SearchResults = new List<PrimarySearchResult>();

            foreach (KeyValuePair<int, string> kvp in SearchResultHtmlBlocks)
            {
                PrimarySearchResult result = new PrimarySearchResult();

                string resultHtml = kvp.Value;
                string title = GoogleSearchResultsHelper.GetPageTitleFromHtml(resultHtml);

                //If the title is blank, it's not a search result
                if (String.IsNullOrEmpty(title))
                    continue;

                result.SequenceNumber = kvp.Key;
                result.Title = title;
                result.Source = GoogleSearchResultsHelper.GetPageSourceTextFromHtml(resultHtml);
                result.PostDate = GoogleSearchResultsHelper.GetPagePostDateFromHtml(resultHtml);
                result.Description = GoogleSearchResultsHelper.GetPageDescriptionFromHtml(resultHtml);

                if (GoogleSearchResultsHelper.GetHasSubSearchResultsFromHtml(resultHtml))
                {
                    BuildSubResults(result, resultHtml);
                }
                               
                Page.SearchResults.Add(result);

            }
        }

        public void BuildSubResults(PrimarySearchResult primaryResult, string primaryResultHtml)
        {            
            var subResultsBlock = GoogleSearchResultsHelper.GetSubResultHtmlBlocks(primaryResultHtml);

            foreach (KeyValuePair<int, string> kvp in subResultsBlock)
            {
                SubSearchResult subResult = new SubSearchResult();

                string resultHtml = kvp.Value;

                subResult.SequenceNumber = kvp.Key;
                subResult.Title = GoogleSearchResultsHelper.GetSubResultTitleFromHtml(resultHtml);
                subResult.Source = GoogleSearchResultsHelper.GetSubResultSourceFromHtml(resultHtml);

                primaryResult.SubResults.Add(subResult);

            }

        }

        public void BuildRelatedSearchTerms()
        {
            foreach (KeyValuePair<int, string> kvp in SearchResultHtmlBlocks)
            {
                string resultHtml = kvp.Value;
                if (resultHtml.Contains("<span class=\"FCUp0c rQMQod\">Related searches</span>"))
                {
                    Page.RelatedSearchTerms = GoogleSearchResultsHelper.GetRelatedSearchTerms(resultHtml);
                }

            }

        }


    }
}