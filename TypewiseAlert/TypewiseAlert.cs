using System;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {
        private static readonly int lowerLimit = 0;

        private const ushort header = 0xfeed;

        private const string recepient = "a.b@c.com";

        public enum BreachType
        {
            Normal,
            TooLow,
            TooHigh
        }

        public enum CoolingType
        {
            PassiveCooling,
            HighActiveCooling,
            MediumActiveCooling
        }

        public enum AlertTarget
        {
            ToController,
            ToEmail
        }

        public struct BatteryCharacter
        {
            public CoolingType coolingType;
            public string brand;
        }

        public static BreachType InferBreach(double value, double lowerLimit, double upperLimit)
        {
            if (value < lowerLimit)
            {
                return BreachType.TooLow;
            }
            if (value > upperLimit)
            {
                return BreachType.TooHigh;
            }

            return BreachType.Normal;
        }

        public static BreachType ClassifyTemperatureBreach(
            CoolingType coolingType, double temperatureInC)
        {
            int upperLimit = 0;

            upperLimit = UpperLimitValue(coolingType);
           
            return InferBreach(temperatureInC, lowerLimit, upperLimit);
        }
        public static int UpperLimitValue(CoolingType coolingType)
        {
            if (coolingType == CoolingType.PassiveCooling)
            {
                return 35;
            }
            if (coolingType == CoolingType.HighActiveCooling)
            {
                return 45;
            }
            return 40;
        }
        public static void CheckAndAlert(
            AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC)
        {

            BreachType breachType = ClassifyTemperatureBreach(
              batteryChar.coolingType, temperatureInC
            );

            if (alertTarget == AlertTarget.ToController)
            {
                SendToController(breachType);
            }
            if (alertTarget == AlertTarget.ToEmail)
            {
                SendToEmail(breachType);
            }
        }
        public static void SendToController(BreachType breachType)
        {
            Console.WriteLine("{0} : {1}\n", header, breachType);
        }
        public static void SendToEmail(BreachType breachType)
        {
            
            if (breachType == BreachType.TooLow)
            {
                Console.WriteLine("To: {0}\n", recepient);
                Console.WriteLine("Hi, the temperature is too low\n");
            }

            if(breachType == BreachType.TooHigh)
            {
                Console.WriteLine("To: {0}\n", recepient);
                Console.WriteLine("Hi, the temperature is too high\n");
            }
        }
    }
}
