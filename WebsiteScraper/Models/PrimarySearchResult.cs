using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteScraper.Models
{
    public class PrimarySearchResult : SearchResult
    {
        public DateTime? PostDate { get; set; }
        public string Description { get; set; }

        public List<SubSearchResult> SubResults = new List<SubSearchResult>();
    }
}