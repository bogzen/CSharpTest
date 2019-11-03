using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count-1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25))
            }; 

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25)),
                new WeekEnd(new DateTime(2017, 4, 29), new DateTime(2017, 4, 29))
            };
            
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }

        [TestMethod]
        public void TestNormalTwoMonthsWeekends()
        {
            DateTime startDate = new DateTime(2019, 11, 28);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2019, 11, 30), new DateTime(2019, 12, 1))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2019, 12, 4)));
        }

        [TestMethod]
        public void TestNormalTwoMonthsLeapYear()
        {
            DateTime startDate = new DateTime(2019, 2, 27);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2019, 2, 28), new DateTime(2019, 3, 1))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2019, 3, 5)));
        }

        [TestMethod]
        public void TestTwoDaysWeekend()
        {
            DateTime startDate = new DateTime(2019, 3, 27);
            int count = 2;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2019, 3, 28), new DateTime(2019, 3, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2019, 3, 30)));
        }

        [TestMethod]
        public void TestOneDayWeekend()
        {
            DateTime startDate = new DateTime(2019, 3, 27);
            int count = 1;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2019, 3, 28), new DateTime(2019, 3, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2019, 3, 27)));
        }

        [TestMethod]
        public void TestTwoWeekendPeriods()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 6;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25)),
                new WeekEnd(new DateTime(2017, 4, 29), new DateTime(2017, 4, 30))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 5, 01)));
        }
    }
}
