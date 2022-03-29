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
            else if (value > upperLimit)
            {
                return BreachType.TooHigh;
            }
            else
            {
                return BreachType.Normal;
            }              
        }
       
        public static BreachType ClassifyTemperatureBreach(
            CoolingType coolingType, double temperatureInC)
        {
            int upperLimit = 0;
            switch (coolingType)
            {
                case CoolingType.PassiveCooling:
                    upperLimit = 35;
                    break;
                case CoolingType.HighActiveCooling:
                    upperLimit = 45;
                    break;
                case CoolingType.MediumActiveCooling:
                    upperLimit = 40;
                    break;
            }
            return InferBreach(temperatureInC, lowerLimit, upperLimit);
        }
       
        
        public static void CheckAndAlert(
            AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC)
        {

            BreachType breachType = ClassifyTemperatureBreach(
              batteryChar.coolingType, temperatureInC
            );

            switch (alertTarget)
            {
                case AlertTarget.ToController:
                    SendToController(breachType);
                    break;
                case AlertTarget.ToEmail:
                    SendToEmail(breachType);
                    break;
                default:
                    break;
            }
        }
        public static void SendToController(BreachType breachType)
        {
            Console.WriteLine("{0} : {1}\n", header, breachType);
        }
        public static void SendToEmail(BreachType breachType)
        {          
            switch (breachType)
            {
                case BreachType.TooLow:
                    Console.WriteLine("To: {0}\n", recepient);
                    Console.WriteLine("Hi, the temperature is too low\n");
                    break;
                case BreachType.TooHigh:
                    Console.WriteLine("To: {0}\n", recepient);
                    Console.WriteLine("Hi, the temperature is too high\n");
                    break;
                case BreachType.Normal:
                    break;
                default:
                    break;
            }
        }
    }
}
