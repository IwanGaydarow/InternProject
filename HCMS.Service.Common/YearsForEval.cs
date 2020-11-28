namespace HCMS.Service.Common
{
    using System;
    using System.Collections.Generic;

    public class YearsForEval : IYearsForEval
    {
        public List<int> GetYearsForEval()
        {
            var years = new List<int>();
            var curYear = DateTime.UtcNow.Year;
            for (int i = curYear - 5; i <= curYear; i++)
            {
                years.Add(i);
            }

            return years;
        }
    }
}
