using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;

namespace NewsIndiaDAL
{
    public class StateManager
    {
        public static List<StateMaster> GetStateList()
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return nie.States.Where(m => m.IsActive).Select(
                        m => new StateMaster()
                        {
                            StateId = m.StateId,
                            StateName = m.StateName,
                        }
                        ).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<StateMaster>();
            }
        }

        /// <summary>
        /// Used to get state information based on country id
        /// </summary>
        /// <returns></returns>
        public static List<StateInformation> GetStateByCountry(int countryId)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return nie.States.Where(m => m.IsActive && m.CountryId==countryId).Select(
                        m => new StateInformation()
                        {
                            StateId = m.StateId,
                            StateName = m.StateName,
                        }
                        ).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<StateInformation>();
            }
        }
    }
}
