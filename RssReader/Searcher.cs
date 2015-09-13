using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Kamal : Class description goes here , summarize whats the main purpose of this class

namespace RssReader
{
    /// <summary>
    /// Ensures that only stuff being searched for is returned.
    /// </summary>
    public class Searcher
    {
        //kamal : not and s  could be changed to some meaningful name
        public bool not = false;
        //kamal: could have given s a more meaningful name and set to empty string 
        public String s = String.Empty;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="args">Can take two arguments : first argument is about what to search and second whether to  search or not</param>
        public Searcher(string[] args)
        {
            try
            {
                //Kamal : when only one parameter is given , set to search string otherwise set to empty string 
                s = args.Length == 0 ? String.Empty :args[0];
                //Kamal : when more than one parameter is given and ssecond parameter is 'true' case insensitive, set search string flag to true 
                not = ((args.Length >= 2) && (String.Equals(args[1], "true", StringComparison.CurrentCultureIgnoreCase))) ? true : false;
               
            }
            catch (ArgumentOutOfRangeException ) //Kamal after modifcation of the code above this wont happen enymore
            {
                //Kamal : its better to handle this in code instead of throwing exception and then setting the values
                
                s = String.Empty;
                not = false;
            }
            catch (Exception)
            {
                // more detailed message when something goes wrong in debug mode
#if DEBUG
                Console.WriteLine("Could not parse search parameters. There are " + args.Length + " parameters");
#else
                Console.WriteLine(args[0]);
#endif
                s = String.Empty;
                not = false;
            }
            finally
            {
                //Kamal : dont think this is needed anymore 
                s = s != String.Empty ? s : String.Empty;
            }
        }

        /// <summary>
        /// Check and see if input matches conditions.
        /// </summary>
        /// <param name="toSearch"></param>
        /// <returns>Returns true if search has to be done (2nd argument is true) and first parameter is in the title or returns true if search has to ignore(2nd parameter is true or throws an exception if something else happens</returns>
        public bool SearchItHard(string toSearch)
        {
            //bool matches = (toSearch.Contains(s) && !not) || (!toSearch.Contains(s) && not);

            //Kamal : toSearch.Contains(s) && !not -> it means par 1 is in the tosearch and par 2 is not true
            //!toSearch.Contains(s) && not -> it means par1 is not in the tosearch and par 2 is true
            // it means if par2 is true dont  search just return true and if par 2 is not true then return true only if par 1 
            //is in the tosearch
            try {

                bool matches = (toSearch.Contains(s) || not);

                return matches;
            }
            catch(Exception ex)
            {
                throw new Exception("Error occurred while doing search." + ex.Message);
            }

            //kamal: dont throw exception when match not found

            //if (matches == false)
            //{
            //    throw new Exception("Doesn't match!");
            //}

            //return true;
        }
    }
}
