using System;
using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        [Fact]
        public static void InfersBreachAsPerLimits()
        {
            Assert.True(TypewiseAlert.InferBreach(12, 20, 30) == TypewiseAlert.BreachType.TooLow);
        }
    }
}
