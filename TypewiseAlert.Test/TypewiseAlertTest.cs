using Xunit;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        [Fact]
        public static void InfersBreachAsPerLimits()
        {
            Assert.True(TypewiseAlert.InferBreach(12, 20, 30) == TypewiseAlert.BreachType.TooLow);
        }

        [Fact]
        public static void InfersBreachHigh()
        {
            Assert.True(TypewiseAlert.InferBreach(40, 20, 30) == TypewiseAlert.BreachType.TooHigh);
        }

        [Fact]
        public static void InfersBreachNormal()
        {
            Assert.True(TypewiseAlert.InferBreach(25, 20, 30) == TypewiseAlert.BreachType.Normal);
        }

        [Fact]
        public static void ClasifyTemperatureBreachAsPerLimits()
        {
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.PassiveCooling, 30.0) == TypewiseAlert.BreachType.Normal);
        }

        [Fact]
        public static void ClasifyTemperatureBreachHighActiveCooling()
        {
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.HighActiveCooling, 50.0) == TypewiseAlert.BreachType.TooHigh);
        }

        [Fact]
        public static void ClasifyTemperatureBreachMediumActiveCooling()
        {
            Assert.False(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.MediumActiveCooling, -10.0) == TypewiseAlert.BreachType.Normal);
        }

        [Fact]
        public static void CheckAndAlertToEmail()
        {
            BatteryCharacter batteryChar = new BatteryCharacter();
            batteryChar.coolingType = CoolingType.HighActiveCooling;
            TypewiseAlert.CheckAndAlert(AlertTarget.ToEmail, batteryChar, 40.5);
        }

        [Fact]
        public static void CheckAndAlertToController()
        {
            BatteryCharacter batteryChar = new BatteryCharacter();
            batteryChar.coolingType = CoolingType.MediumActiveCooling;
            TypewiseAlert.CheckAndAlert(AlertTarget.ToController, batteryChar, 20.0);
        }
    }
}
