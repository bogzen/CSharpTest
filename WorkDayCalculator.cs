using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            DateTime currentDay = startDate;
            List<DateTime> allWeekends = new List<DateTime>();
            int countWorkDays = 0;
            if (weekEnds != null)
            {
                foreach (var weekend in weekEnds)
                {
                    allWeekends.AddRange(GetWeekendRange(weekend.StartDate, weekend.EndDate));
                }
            }

            while (true)
            {
                if (weekEnds == null)
                {
                    currentDay = currentDay.AddDays(dayCount - 1);
                    break;
                }
                if (allWeekends.All(x => x != currentDay && dayCount == 1))
                {
                    currentDay = startDate;
                    break;
                }
                if (allWeekends.All(x => x != currentDay))
                {
                    currentDay = currentDay.AddDays(1);
                    countWorkDays++;
                }
                if (allWeekends.Any(x => x == currentDay))
                {
                    currentDay = currentDay.AddDays(1);
                }
                if (countWorkDays == dayCount - 1 && allWeekends.Any(x => x == currentDay))
                {
                    currentDay = currentDay.AddDays(1);
                }
                if (countWorkDays == dayCount - 1 && allWeekends.All(x => x != currentDay))
                {
                    break;
                }
            }

            return currentDay;
        }
        
        static List<DateTime> GetWeekendRange(DateTime start, DateTime end)
        {
            List<DateTime> weekendRange = new List<DateTime>();
            weekendRange.Add(start);
            while (end != weekendRange.Last())
            {
                weekendRange.Add(weekendRange.Last().AddDays(1));
            }

            return weekendRange;
        }
    }
}
