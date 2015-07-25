using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using NewsIndiaDAL;

namespace NewsIndiaBAL
{
    public class StateList
    {
        public static List<StateMaster> GetStateList(int subCategory = 0)
        {
            try
            {

                return NewsIndiaDAL.StateManager.GetStateList().ToList();

            }
            catch (Exception ex)
            {
                return new List<StateMaster>();
            }
        }

        /// <summary>
        /// Used to get the state information based on countryId
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public static List<StateInformation> GetStateByCountry(int countryId)
        {
            try
            {

                return NewsIndiaDAL.StateManager.GetStateByCountry(countryId);

            }
            catch (Exception ex)
            {
                return new List<StateInformation>();
            }
        }
    }
}
