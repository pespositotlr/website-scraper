using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace WebsiteScraper.Helpers
{
    public static class GoogleSearchResultsHelper
    {
        internal static List<string> GetRelatedSearchTerms(string html)
        {
            List<string> relatedSearchTerms = new List<string>();

            var pattern = @"<div[^>]*class=""BNeawe deIvCb AP7Wnd"">(.*?)</div>";
            var matches = Regex.Matches(html, pattern, RegexOptions.Singleline);

            foreach (Match m in matches)
            {
                if (!m.Groups[1].Value.Contains("FCUp0c rQMQod")) //Ignore header
                {
                    relatedSearchTerms.Add(m.Groups[1].Value);
                }
            }

            return relatedSearchTerms;

        }
        internal static Dictionary<int, string> GetSearchResultHtmlBlocks(string html)
        {
            Dictionary<int, string> searchResultHtmlBlocks = new Dictionary<int, string>();
            int blockSequenceNum = 1;

            var pattern = @"<div[^>]*class=""ZINbbc xpd O9g5cc uUPGi"">(.*?)</div></div></div>";
                        
            var matches = Regex.Matches(html, pattern, RegexOptions.Singleline);

            foreach (Match m in matches)
            {
                //Exclude inorganic ads
                if(!m.Groups[1].Value.ToString().Contains("<span class=\"rtDDKc VqFMTc NceN9e\">Ad</span>"))
                {
                    searchResultHtmlBlocks.Add(blockSequenceNum, m.Groups[1].Value + "</div>");
                    blockSequenceNum++;
                }
            }

            return searchResultHtmlBlocks;
        }

        internal static Dictionary<int, string> GetSubResultHtmlBlocks(string mainResultHtml)
        {
            Dictionary<int, string> subSearchBlocks = new Dictionary<int, string>();
            int blockSequenceNum = 1;

            var pattern = @"<div[^>]*class=""v9i61e"">(.*?)</div>";
            var matches = Regex.Matches(mainResultHtml, pattern, RegexOptions.Singleline);

            foreach (Match m in matches)
            {
                subSearchBlocks.Add(blockSequenceNum, m.Groups[1].Value);
                blockSequenceNum++;
            }

            return subSearchBlocks;
        }

        public static string GetPageTitleFromHtml(string resultHtml)
        {
            return GetTextWithinHtmlDivByClass(resultHtml, "BNeawe vvjwJb AP7Wnd");
        }

        public static string GetPageSourceTextFromHtml(string resultHtml)
        {
            return GetTextWithinHtmlDivByClass(resultHtml, "BNeawe UPmit AP7Wnd").Replace("/url?q=", "");
        }

        public static DateTime? GetPagePostDateFromHtml(string resultHtml)
        {
            var postDate = GetTextWithinHtmlSpanByClass(resultHtml, "r0bn4c rQMQod");

            if (!String.IsNullOrEmpty(postDate))
            {
                //Only return value if input string is an actual datetime
                DateTime outputDateTime;
                if (DateTime.TryParse(postDate, out outputDateTime))
                   return Convert.ToDateTime(outputDateTime);
            }

            return null;
        }

        public static string GetPageDescriptionFromHtml(string resultHtml)
        {
            return StripHTML(GetTextWithinHtmlDivByClass(resultHtml, "BNeawe s3v9rd AP7Wnd"))
                .Replace("�", "");
        }

        public static bool GetHasSubSearchResultsFromHtml(string resultHtml)
        {
            return resultHtml.Contains("XLloXe AP7Wnd");
        }

        public static string GetSubResultTitleFromHtml(string resultHtml)
        {
            return GetTextWithinHtmlSpanByClass(resultHtml, "XLloXe AP7Wnd");
        }

        public static string GetSubResultSourceFromHtml(string resultHtml)
        {
            return GetAnchorTagSourceWithinHtml(resultHtml).Replace("/url?q=", "");
        }

        private static string GetTextWithinHtmlDivByClass(string htmlBlock, string className)
        {
            return GetTextWithinHtmlTagByClass(htmlBlock, "div", className);
        }
        private static string GetTextWithinHtmlSpanByClass(string htmlBlock, string className)
        {
            return GetTextWithinHtmlTagByClass(htmlBlock, "span", className);
        }

        private static string GetTextWithinHtmlTagByClass(string htmlBlock, string tagName, string className)
        {
            string innerText = "";
            var pattern = "<" + tagName + "[^>]*class=\"" + className + "\">(.*?)</" + tagName  + ">";
            var matches = Regex.Matches(htmlBlock, pattern, RegexOptions.Singleline);

            if (matches.Count > 0)
            {
                innerText = matches[0].Groups[1].Value;
            }

            return innerText;
        }

        private static string GetAnchorTagSourceWithinHtml(string htmlBlock)
        {
            string innerText = "";
            var pattern = "<a[^>]*href=\"(.*?)\">";
            var matches = Regex.Matches(htmlBlock, pattern, RegexOptions.Singleline);

            if (matches.Count > 0)
            {
                innerText = matches[0].Groups[1].Value;
            }

            return innerText;
        }
        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

    }
}