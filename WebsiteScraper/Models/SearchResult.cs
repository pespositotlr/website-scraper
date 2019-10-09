using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteScraper.Models
{
    public abstract class SearchResult : ISearchResult
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public int SequenceNumber { get; set; }
    }
}