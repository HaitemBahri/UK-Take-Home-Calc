using UKTakeHomeCalc.Core.Models;
using Xunit;

namespace UKTakeHomeCalc.Core.Test.MonetaryValueTests
{
    public class MonetaryValueTests
    {
        public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldConvertUsingExtensionMethodsTheoryData =>
            new()
            {
                {new MonetaryValue(4500m, Frequency.Weekly), 4500m.Weekly(), "Weekly Value"},
                {new MonetaryValue(4500m, Frequency.Monthly), 4500m.Monthly(), "Monthly Value"},
                {new MonetaryValue(4500m, Frequency.Annually), 4500m.Annually(), "Annually Value" },
                {new MonetaryValue(4500m, Frequency.Weekly), 4500m.Every(Frequency.Weekly), "Weekly Value using Every() Methed"},
                {new MonetaryValue(4500m, Frequency.Monthly), 4500m.Every(Frequency.Monthly), "Monthly Value using Every() Methed"},
                {new MonetaryValue(4500m, Frequency.Annually), 4500m.Every(Frequency.Annually), "Annually Value using Every() Methed" },
            };

        [Theory]
        [MemberData(nameof(ShouldConvertUsingExtensionMethodsTheoryData))]
        public void ShouldConvertUsingExtensionMethods(MonetaryValue expectedResult, MonetaryValue actualResult, string testDataName)
        {
            Assert.Equal(expectedResult, actualResult);
        }

        public static TheoryData<MonetaryValue, MonetaryValue, MonetaryValue, string> ShouldAddTwoMonetaryValuesTheoryData =>
            new()
            {
                {450m.Monthly(), 1200m.Monthly(), 1650m.Monthly(), "Positive + Positive"},
                {450m.Weekly(), -1200m.Weekly(), -750m.Weekly(), "Positive + Negative"},
                {-450m.Annually(), -1200m.Annually(), -1650m.Annually(), "Negative + Negative" },
            };

        [Theory]
        [MemberData(nameof(ShouldAddTwoMonetaryValuesTheoryData))]
        public void ShouldAddTwoMonetaryValues(MonetaryValue value1, MonetaryValue value2, MonetaryValue expectedResult, string testDataName)
        {
            var actualResult = value1 + value2;

            Assert.Equal(expectedResult, actualResult);
        }


        public static TheoryData<MonetaryValue, MonetaryValue, MonetaryValue, string> ShouldSubtractTwoMonetaryValuesTheoryData =>
            new()
            {
                {1450m.Monthly(), 1200m.Monthly(), 250m.Monthly(), "Positive - Positive"},
                {450m.Monthly(), -1200m.Monthly(), 1650m.Monthly(), "Positive - Negative"},
                {-450m.Monthly(), -1200m.Monthly(), 750m.Monthly(), "Negative - Negative" },
                {-900m.Monthly(), 9200m.Monthly(), -10100m.Monthly(), "Negative - Positive" },
            };

        [Theory]
        [MemberData(nameof(ShouldSubtractTwoMonetaryValuesTheoryData))]
        public void ShouldSubtractTwoMonetaryValues(MonetaryValue value1, MonetaryValue value2, MonetaryValue expectedResult, string testDataName)
        {
            var actualResult = value1 - value2;

            Assert.Equal(expectedResult, actualResult);
        }


        public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldNegateValue_WhenPreceededWithNegativeSignTheoryData =>
            new()
            {
                {1450m.Monthly(), -1450m.Monthly(), "Positive value"},
                {-560m.Annually(), 560m.Annually(), "Negative value"},

            };

        [Theory]
        [MemberData(nameof(ShouldNegateValue_WhenPreceededWithNegativeSignTheoryData))]
        public void ShouldNegateValue_WhenPreceededWithNegativeSign(MonetaryValue value, MonetaryValue expectedResult, string testDataName)
        {
            var actualResult = -value;

            Assert.Equal(expectedResult, actualResult);
        }

        public static TheoryData<MonetaryValue, decimal, MonetaryValue, string> ShouldMultiplyWithDecimalTheoryData =>
            new()
            {
                {55m.Monthly(), 6.9m, 379.5m.Monthly(), "Positive multiplier"},
                {69000m.Annually(), -0.4m, -27600m.Annually(), "Negative multiplier"},
            };

        [Theory]
        [MemberData(nameof(ShouldMultiplyWithDecimalTheoryData))]
        public void ShouldMultiplyWithDecimal(MonetaryValue value, decimal multiplier, MonetaryValue expectedResult, string testDataName)
        {
            var actualResult = value * multiplier;

            Assert.Equal(expectedResult, actualResult);
        }

        public static TheoryData<MonetaryValue, decimal, MonetaryValue, string> ShouldDivideByDecimalTheoryData =>
            new()
            {
                {55m.Monthly(), 6.9m, 7.97m.Monthly(), "Positive Divider"},
                {69000m.Annually(), -0.4m, -172500m.Annually(), "Negative Divider"},
            };

        [Theory]
        [MemberData(nameof(ShouldDivideByDecimalTheoryData))]
        public void ShouldDivideByDecimal(MonetaryValue value, decimal divider, MonetaryValue expectedResult, string testDataName)
        {
            var actualResult = value / divider;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldThrowDivideByZeroExceptionWhenDividingByZero()
        {
            var _sut = 4599.39903m.Annually();
            var divider = 0;

            Assert.Throws<DivideByZeroException>(() => _sut / divider);
        }

        public static TheoryData<MonetaryValue, string, string> ShouldReturnCorrectStringTheoryData =>
            new()
            {
                {55m.Monthly(), "55.00/Monthly", "Positive, Zero Decimal"},
                {-155.99m.Weekly(), "-155.99/Weekly", "Negative, Non Zero Decimal"},
                {19500m.Annually(), "19,500.00/Annually", "Decimal Separator"},
                {-6919500.966m.Monthly(), "-6,919,500.97/Monthly", "Rounding Second Number"},
            };

        [Theory]
        [MemberData(nameof(ShouldReturnCorrectStringTheoryData))]
        public void ShouldReturnCorrectString(MonetaryValue value, string expectedResult, string testDataName)
        {
            var actualResult = value.ToString();

            Assert.Equal(expectedResult, actualResult);
        }

        public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldTwoMonetaryValuesBeEqualTheoryData =>
            new()
            {
                {900m.Monthly(), 900m.Monthly(), "Same Value and Frequency"},
                {-900m.Monthly(), -210m.Weekly(), "Different Value and Different Frequency"},
                {900.68m.Annually(), 900.68m.Annually(), "Same Value with Decimal values"},
                {1320.557m.Weekly(), 1320.56m.Weekly(), "Rounding up"},
                {16.9243m.Monthly(), 16.92m.Monthly(), "Not Rounding up"},
                {3.135m.Weekly(), 3.14m.Weekly(), "Rounding [Odd] second decimal up with .5"},
                {3.145m.Monthly(), 3.14m.Monthly(), "Not Rounding [Even] second decimal up with .5"},
            };

        [Theory]
        [MemberData(nameof(ShouldTwoMonetaryValuesBeEqualTheoryData))]
        public void ShouldTwoMonetaryValuesBeEqual(MonetaryValue value1, MonetaryValue value2, string testDataName)
        {
            Assert.Equal(value1, value2);
            Assert.True(value1 == value2);
        }

        public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldTwoMonetaryValuesNotBeEqualTheoryData =>
            new()
            {
                {900m.Monthly(), 63m.Monthly(), "Different Value but Same Frequency"},
                {-900m.Monthly(), -900m.Weekly(), "Same Value but Different Frequency"},
            };

        [Theory]
        [MemberData(nameof(ShouldTwoMonetaryValuesNotBeEqualTheoryData))]
        public void ShouldTwoMonetaryValuesNotBeEqual(MonetaryValue value1, MonetaryValue value2, string testDataName)
        {
            Assert.NotEqual(value1, value2);
            Assert.True(value1 != value2);
        }

        public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldOneValueBeGreaterThanOtherValueTheoryData =>
            new()
            {
                {900m.Weekly(), 63m.Weekly(), "Positive - Different Values and Same Frequency"},
                {-290m.Monthly(), -1800m.Monthly(), "Negative - Different Values and Same Frequency"},
                {2500m.Monthly(), 5000m.Annually(), "Different Frequencies"},
            };

        [Theory]
        [MemberData(nameof(ShouldOneValueBeGreaterThanOtherValueTheoryData))]
        public void ShouldOneValueBeGreaterThanOtherValue(MonetaryValue value1, MonetaryValue value2, string testDataName)
        {
            Assert.True(value1 > value2);
        }

        [Theory]
        [MemberData(nameof(ShouldOneValueBeGreaterThanOtherValueTheoryData))]
        [MemberData(nameof(ShouldTwoMonetaryValuesBeEqualTheoryData))]
        public void ShouldOneValueBeGreaterThanOrEqualOtherValue(MonetaryValue value1, MonetaryValue value2, string testDataName)
        {
            Assert.True(value1 >= value2);
        }

        public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldOneValueBeLessThanOtherValueTheoryData =>
            new()
            {
                {63m.Weekly(), 900m.Weekly(), "Positive - Different Values and Same Frequency"},
                {-1800m.Monthly(), -290m.Monthly(), "Negative - Different Values and Same Frequency"},
                {5000m.Annually(), 2500m.Monthly(), "Different Frequencies"},
            };

        [Theory]
        [MemberData(nameof(ShouldOneValueBeLessThanOtherValueTheoryData))]
        public void ShouldOneValueBeLessThanOtherValue(MonetaryValue value1, MonetaryValue value2, string testDataName)
        {
            Assert.True(value1 < value2);
        }

        [Theory]
        [MemberData(nameof(ShouldOneValueBeLessThanOtherValueTheoryData))]
        [MemberData(nameof(ShouldTwoMonetaryValuesBeEqualTheoryData))]
        public void ShouldOneValueBeLessThanOrEqualOtherValue(MonetaryValue value1, MonetaryValue value2, string testDataName)
        {
            Assert.True(value1 <= value2);
        }


    }
}
