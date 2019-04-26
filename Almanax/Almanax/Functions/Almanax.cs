using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Almanax.Functions
{
    class Almanax
    {
        private readonly string _almanax = "http://www.krosmoz.com/en/almanax";
        public Almanax()
        {

        }

        public void GetOffering()
        {
            string html;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_almanax);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            string bonus = html.Substring(html.IndexOf("<b>") + 3, html.IndexOf("<div class=\"more-infos\">") - html.IndexOf("<b>") - 3);
            bonus = bonus.Replace("</b>", "");
            Console.WriteLine($"Almanax Bonus For {DateTime.UtcNow.AddHours(2).ToShortDateString()}: {bonus}");

            string offering = html.Substring(html.IndexOf("<p class=\"fleft\">") + 17, 
                html.IndexOf("</p>", html.IndexOf("<p class=\"fleft\">")) - 
                html.IndexOf("<p class=\"fleft\">") - 2);
            offering = offering.Replace("</p>", "").Trim();
            Console.WriteLine($"Almanax Offering: {offering}");
        }
    }
}
