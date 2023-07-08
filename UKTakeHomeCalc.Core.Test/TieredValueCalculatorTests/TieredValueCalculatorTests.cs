using UKTakeHomeCalc.Core.Helpers;
using UKTakeHomeCalc.Core.Models;
using UKTakeHomeCalc.Core.TieredValueCalculators;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.TieredValueCalculatorTests
{
    public class TieredValueCalculatorTests
    {
        private TieredValueCalculator _sut;

        public TieredValueCalculatorTests()
        {
            _sut = new TieredValueCalculator();
        }

        public static TheoryData<MonetaryValue, MonetaryValue, MonetaryValue, MonetaryValue, string> ShouldCalculateValueBetweenThresholdsTheoryData =>
            new()
            {
                {45m.Weekly(), 120m.Weekly(), 560m.Weekly(), 0m.Weekly(), "Value < Lower Threshold"},
                {120m.Weekly(), 120m.Weekly(), 560m.Weekly(), 0m.Weekly(), "Value = Lower Threshold"},
                {245m.Weekly(), 120m.Weekly(), 560m.Weekly(), 125m.Weekly(), "Lower Threshold < Value < Upper Threshold"},
                {560m.Weekly(), 120m.Weekly(), 560m.Weekly(), 440m.Weekly(), "Value = Upper Threshold"},
                {2345m.Weekly(), 120m.Weekly(), 560m.Weekly(), 440m.Weekly(), "Value > Upper Threshold"},
            };



        [Theory]
        [MemberData(nameof(ShouldCalculateValueBetweenThresholdsTheoryData))]
        public void ShouldCalculateValueBetweenThresholds(MonetaryValue value, MonetaryValue lowerThreshold, MonetaryValue upperThreshold, MonetaryValue expectedResult, string testDataName)
        {
            var actualResult = _sut.CalculateValueBetweenThresholds(value, lowerThreshold, upperThreshold);

            Assert.Equal(expectedResult, actualResult);
        }

        public static List<TieredValueRule> tieredValueRules =>
            new()
            {
                new TieredValueRule(12000m.Annually(), 50700m.Annually(), 0.2m),
                new TieredValueRule(50700m.Annually(), 125000m.Annually(), 0.4m),
            };

        public static TheoryData<MonetaryValue, IEnumerable<TieredValueRule>, IEnumerable<TieredValueResult>, string> ShouldCalculateTieredValueResultsTheoryData =>
            new()
            {
                {100m.Annually(), tieredValueRules, new List<TieredValueResult> () { 
                    new TieredValueResult(tieredValueRules[0], 0m.Annually()), 
                    new TieredValueResult(tieredValueRules[1], 0m.Annually()) 
                }, "Value < 1st rule lower threshold" },

                {12000m.Annually(), tieredValueRules, new List<TieredValueResult> () {
                    new TieredValueResult(tieredValueRules[0], 0m.Annually()),
                    new TieredValueResult(tieredValueRules[1], 0m.Annually())
                }, "Value = 1st rule lower threshold" },

                {20000m.Annually(), tieredValueRules, new List<TieredValueResult> () {
                    new TieredValueResult(tieredValueRules[0], 1600m.Annually()),
                    new TieredValueResult(tieredValueRules[1], 0m.Annually())
                }, "Value within 1st rule thresholds exclusive" },

                {50700m.Annually(), tieredValueRules, new List<TieredValueResult> () {
                    new TieredValueResult(tieredValueRules[0], 7740m.Annually()),
                    new TieredValueResult(tieredValueRules[1], 0m.Annually())
                }, "Value = 1st rule upper threshold" },

                {50700m.Annually(), tieredValueRules, new List<TieredValueResult> () {
                    new TieredValueResult(tieredValueRules[0], 7740m.Annually()),
                    new TieredValueResult(tieredValueRules[1], 0m.Annually())
                }, "Value = 2st rule lower threshold" },

                {95000m.Annually(), tieredValueRules, new List<TieredValueResult> () {
                    new TieredValueResult(tieredValueRules[0], 7740m.Annually()),
                    new TieredValueResult(tieredValueRules[1], 17720m.Annually())
                }, "Value within 2st rule thresholds exclusive" },

                {125000m.Annually(), tieredValueRules, new List<TieredValueResult> () {
                    new TieredValueResult(tieredValueRules[0], 7740m.Annually()),
                    new TieredValueResult(tieredValueRules[1], 29720m.Annually())
                }, "Value = 2st rule upper threshold" },

                {245000m.Annually(), tieredValueRules, new List<TieredValueResult> () {
                    new TieredValueResult(tieredValueRules[0], 7740m.Annually()),
                    new TieredValueResult(tieredValueRules[1], 29720m.Annually())
                }, "Value > 2st rule upper threshold" },
            };

        [Theory]
        [MemberData(nameof(ShouldCalculateTieredValueResultsTheoryData))]
        public void ShouldCalculateTieredValueResults(MonetaryValue value, IEnumerable<TieredValueRule> rules, IEnumerable<TieredValueResult> expectedResult, string testDataName)
        {
            var actualResult = _sut.CalculateTieredValueResults(value, rules);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
