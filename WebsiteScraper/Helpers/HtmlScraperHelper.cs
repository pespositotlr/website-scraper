using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace WebsiteScraper.Helpers
{
    public class HtmlScraperHelper
    {

        public static string GetGoogleSearchUrl(string keywords, int itemsPerPage = 100)
        {
            return "https://www.google.com/search?num=" + itemsPerPage + "&q=" + HttpUtility.UrlEncode(keywords);
        }

        public static string GetHtmlOfTargetUrl(string urlToScrape)
        {

            string result = null;
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToScrape);
                request.Method = "GET";
                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();
            }

            return result;

        }

    }

}