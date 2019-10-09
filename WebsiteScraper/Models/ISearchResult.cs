using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteScraper.Models
{
    public interface ISearchResult
    {
        string Title { get; set; }
        string Source { get; set; }
        int SequenceNumber { get; set; }
    }
}