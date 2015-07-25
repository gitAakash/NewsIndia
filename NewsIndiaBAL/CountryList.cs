using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using NewsIndiaDAL;

namespace NewsIndiaBAL
{
    public class CountryList
    {
        public static List<CountryMaster> GetCountryList(int subCategory = 0)
        {
            try
            {

                return CountryData.GetCountryList();

            }
            catch (Exception ex)
            {
                return new List<CountryMaster>();
            }
        }
    }
}
