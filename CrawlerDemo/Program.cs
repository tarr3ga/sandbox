using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CrawlerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            StartCrawlerAsync();

            Console.Read();
        }

        private static async Task StartCrawlerAsync()
        {
            var url = "https://www.automobile.tn/neuf/bmw.3/";

            var html = "";

            try
            {
                var httpClient = new HttpClient();
                html = await httpClient.GetStringAsync(url);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }

            
            //Console.WriteLine(html);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Equals("article" +
                "_new_car article_last_modele")).ToList();

            var cars = new List<Car>();

            foreach (var div in divs)
            {
                Car c = new Car
                {
                    Model = div?.Descendants("h2")?.FirstOrDefault()?.InnerText,
                    Price = div?.Descendants("div")?.FirstOrDefault()?.InnerText,
                    Link = div?.Descendants("a")?.FirstOrDefault()?.ChildAttributes("href")?.FirstOrDefault()?.Value,
                    ImageUrl = div.Descendants("img")?.FirstOrDefault()?.ChildAttributes("src")?.FirstOrDefault()?.Value
                };

                cars.Add(c);
            }
        }
    }

    [DebuggerDisplay("{Model}, {Price}, {Link}, {ImageUrl}")]
    public class Car
    {
        public string Model { get; set; }
        public string Price { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
    }
}