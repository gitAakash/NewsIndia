using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;

namespace NewsIndiaDAL
{
    public class CountryData
    {
        public static List<CountryMaster> GetCountryList()
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return nie.Countries.Where(m=>m.IsActive).Select(
                        m => new CountryMaster()
                        {
                            CountryId = m.CountryId,
                            CountryName = m.CountryName,
                        }
                        ).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<CountryMaster>();
            }
        }
    }
}
