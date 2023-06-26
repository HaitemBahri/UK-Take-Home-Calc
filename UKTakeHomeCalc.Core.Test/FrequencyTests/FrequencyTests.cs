﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.ModelsTests.FrequencyTests
{
    public class FrequencyTests
    {
        [Fact]
        public void ValueComparison_ShouldReturnIntValueForWeeklyFrequency()
        {
            var weeklyFreq = Frequency.Weekly;

            int weeklyFreqInt = (int)weeklyFreq;

            int weeklyFreqIntExpected = 7;

            Assert.Equal(weeklyFreqInt, weeklyFreqIntExpected);
        }

        [Fact]
        public void ValueComparison_ShouldReturnIntValueForMonthlyFrequency()
        {
            var monthlyFreq = Frequency.Monthly;

            int monthlyFreqInt = (int)monthlyFreq;

            int monthlyFreqIntExpected = 30;

            Assert.Equal(monthlyFreqInt, monthlyFreqIntExpected);
        }

        [Fact]
        public void ValueComparison_ShouldReturnIntValueForAnnuallyFrequency()
        {
            var annuallyFreq = Frequency.Annually;

            int annuallyFreqInt = (int)annuallyFreq;

            int annuallyFreqIntExpected = 365;

            Assert.Equal(annuallyFreqInt, annuallyFreqIntExpected);
        }
    }
}