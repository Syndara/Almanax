using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Almanax.Functions
{
    public class ItemSearch
    {

        private readonly string _weapons = $"https://www.dofus.com/en/search";
        private readonly string _main = $"https://www.dofus.com";
        private Regex _matcher;

        public ItemSearch()
        {
        }

        public void SearchWeapons(string term)
        {
            try
            {
                string html;
                string text = "?text=" + term;
                var builder = new UriBuilder(_weapons)
                {
                    Port = -1
                };
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["q"] = term;
                query["index"] = "1";
                builder.Query = query.ToString();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(builder.ToString());
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                string item = html.Substring(html.IndexOf("/en/mmorpg/encyclopedia/"),
                    html.IndexOf("\n", html.IndexOf("/en/mmorpg/encyclopedia/")) -
                    html.IndexOf("/en/mmorpg/encyclopedia/") - 2);

                string weaponName = String.Empty;
                if (item.Contains(term.Replace(' ', '-').ToLowerInvariant()))
                {
                    weaponName = item.Substring(item.IndexOf(term.Replace(' ', '-').ToLowerInvariant()));
                }
                    
                if (weaponName != null)
                {
                    Console.WriteLine(weaponName);
                }
                    
                request = (HttpWebRequest)WebRequest.Create(_main + item);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                _matcher = new Regex("(<div class=\"ak-title\">\n)(.*?)(</div>)");
                List<string> stats = new List<string>();

                foreach (Match m in _matcher.Matches(html))
                {
                    Console.WriteLine(m.Groups[2]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error finding item.");
            }
        }
    }
}
