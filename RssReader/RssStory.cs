using System;
using System.Text.RegularExpressions;
//Kamal : Class description goes here , summarize whats the main purpose of this class
namespace RssReader
{
    public class RssStory
    {
        //Kamal : expose internal implementation through properties
        //private int _index;
        
        public int Index { get ; set; }

        //public string _title;
        public string Title { get; set; }

        //private string _description;
        public string Description { get; set; }

        //private string _link;
        public string Link { get; set; }

        // DateTime _published;
        public DateTime Published { get; set; }

        Regex regex = new Regex("(?<scheme>[a-z]{3,5})://(?<host>[a-z0-9_-]+(.[a-z0-9_-]+)*)/(?<path>.*)");

        public RssStory(string title, string description, string link)
        {
            //Kamal : use private variable
            Title = title;
            //Kamal : use private variable
            Description = description;
            Link = link;

            //Match match = regex.Match(link);
           // Link = new UriBuilder(match.Groups["scheme"].Value, match.Groups["host"].Value, -1, match.Groups["path"].Value).Uri;

            // 10,000 should be sufficient to avoid collisions.
            //Index = new Random().Next(10000);
            
        }

        public override string ToString()
        {
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Description: " + Description);
            Console.WriteLine("Published On: " + Published);
            Console.WriteLine("Link: " + Link);
            Console.WriteLine();

            return string.Empty;
        }
    }
}
