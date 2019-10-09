using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteScraper.Models;

namespace WebsiteScraper.Helpers
{
    public class SearchResultsPageHelper
    {        
        public static List<PrimarySearchResult> GetSearchResultsContainingString(SearchResultsPage page, string stringToFind)
        {
            var foundSearchResults = new List<PrimarySearchResult>();
            var stringToFindLowercase = stringToFind.ToLower();

            foreach (PrimarySearchResult result in page.SearchResults)
            {

                if ((result.Title.ToLower().Contains(stringToFindLowercase) ||
                    result.Source.ToLower().Contains(stringToFindLowercase))
                    && !foundSearchResults.Any(x => x.SequenceNumber == result.SequenceNumber))
                {
                    foundSearchResults.Add(result);
                }

                foreach(SubSearchResult subResult in result.SubResults)
                {
                    if ((subResult.Title.ToLower().Contains(stringToFindLowercase) ||
                        subResult.Source.ToLower().Contains(stringToFindLowercase))
                        && !foundSearchResults.Any(x => x.SequenceNumber == result.SequenceNumber))
                    {
                        foundSearchResults.Add(result);
                    }
                }

            }
            
            return foundSearchResults;

        }
    }
}