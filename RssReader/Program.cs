using System;
using System.Collections.Generic;
using System.Linq;
//Kamal : delete references those are not in use
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

namespace RssReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
        //var reader = new RssReader("http://rss.cnn.com/rss/edition.rss");

        
            var reader = new RssReader("http://feeds.bbci.co.uk/news/rss.xml");

            reader.LoadStories();
            var stories = reader.GetTopStories();

            Searcher searcher = new Searcher(args);

            foreach (var story in stories.Where(r =>
                {
                    try
                    {
                       return searcher.SearchItHard(r.Title);
                        //return true;
                    }
                    catch(Exception)
                    { return false; }
                }))
            {
                story.ToString();
            }

            Console.ReadLine();
        }
    }

}
