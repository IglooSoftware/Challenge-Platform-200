using System;
using System.Collections.Generic;
using System.Linq;
//Kamal : delete references that are not being used
//using System.Text;
//using System.Threading.Tasks;
using System.Xml.Linq;
//Kamal : Class description goes here , summarize whats the main purpose of this class

namespace RssReader
{ // Kamal : use interface
    public class RssReader: IRssReader
    {
        private const string FEED_ADDRESSS = "http://feeds.bbci.co.uk/news/rss.xml";

        //Kamal : add comments to descibe their function
        private Dictionary<int, RssStory> _storyLookup;
        private ThreadSafeQueue _storyIndex;
        private List<RssStory> _stories;
        private XElement _feed;

        //Kamal: declare a counter that keeps unique value of index 
        private int _rrsStoryIndex = 0;

        // Constructors
        #region Constructors
        /// <summary>
        /// Constructor with feed address set to "http://feeds.bbci.co.uk/news/rss.xml"
        /// </summary>
        public RssReader()
        {
            _feed = XElement.Load(FEED_ADDRESSS).Element("channel");
            Initialie();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="feedAddress"></param>
        public RssReader(string feedAddress)
        {
            _feed = XElement.Load(feedAddress).Element("channel");
            Initialie();
        }

        #endregion
        #region initialize 
        /// <summary>
        /// Initializes story lookup, story index, and list of stories
        /// </summary>
        private void Initialie()
        {
            _storyLookup = new Dictionary<int, RssStory>();
            _storyIndex = new ThreadSafeQueue();
            _stories = new List<RssStory>();

        }
        #endregion



        /**********************
        *  ALL STORIES MUST BE
        *  LOADED PRIOR TO USE.
        ***********************/
        public void LoadStories()
        {
            


            //Kamal : put this code under try
            try
            {

                    foreach (var item in _feed.Elements("item"))
                {
                    
                    _stories.Add(new RssStory(item.Element("title").Value, item.Element("description").Value, item.Element("link").Value));
                    _stories.Last().Published = DateTime.Parse(item.Element("pubDate").Value);
                    _stories.Last().Index = ++_rrsStoryIndex;
                    //if (_storyLookup.ContainsKey(_stories.Last().Index))
                    //{
                    //    // Story is already in the list.
                    //    // Not sure why this happens, but we should remove it.
                    //    //Kamal : this may happen as index is set to a random number not a unique number so Index may have a repeat value
                    //    _stories.Remove(_stories.Last());
                    //}
                    //else
                    //{
                        _storyLookup.Add(_stories.Last().Index, _stories.Last());
                    //}

                    //foreach (RssStory st in _stories)
                    //{
                    //    //if (!_storyLookup.ContainsKey(st.Index))
                    //    //{
                    //        _storyLookup.Add(st.Index, st);
                    //   // }
                    //}
                }

                //foreach (var item in _storyLookup)
                //{

                //    Console.WriteLine(item.Key.ToString());
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error happened while loading stories. Details:" + ex.Message);
            }
        }

        public IEnumerable<RssStory> GetTopStories()
        {
            var latestStories = _stories.OrderBy(s => s.Published);
            var recentStories = new List<RssStory>();

            // Get the five most recent stories from the feed.
            var fiveLatestStoryIndices = latestStories.Take(5).Select(s => s.Index);
            //var fiveLatestStoryIndices = latestStories.TakeWhile(s => s.Published < DateTime.Now.AddMinutes(30)).Select(s => s.Index);


            foreach(var item in fiveLatestStoryIndices)
            {
                //Kamal: not sure if LIFO (stack) really need to use here but will keep it as is 
                //or it item could direclty added to recent strories as there are only latest five stories
                _storyIndex.Enqueue(Convert.ToInt32(item)); 
            }
           
            while (true)
            {
                try
                {
                    recentStories.Add(_storyLookup[_storyIndex.Pop()]);
                }
                catch (Exception ) //Kamal : not sure if exception it needed to be thrown or if it could be handled by checking stack count
                {
                    // Queue is empty.
                    break;
                }
            }

            return recentStories;
        }
    }
}
