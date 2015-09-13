//Kamal : delete references those are not being used
//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace RssReader
{
    // Ready for DI.
    public interface IRssReader
    {
        void LoadStories();
        IEnumerable<RssStory> GetTopStories();
    }
}
