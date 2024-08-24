using System;
using System.Collections.Generic;

namespace DeveloperSample.ClassRefactoring
{
    public enum SwallowType
    {
        African,
        European
    }

    public enum SwallowLoad
    {
        None,
        Coconut
    }

    public class SwallowFactory
    {
        public Swallow GetSwallow(SwallowType swallowType) => new Swallow(swallowType);
    }

    public class Swallow
    {
        private static readonly Dictionary<SwallowType, Dictionary<SwallowLoad, double>> AirspeedVelocities = new()
        {
            {
                SwallowType.African, new Dictionary<SwallowLoad, double>
                {
                    { SwallowLoad.None, 22 },
                    { SwallowLoad.Coconut, 18 }
                }
            },
            {
                SwallowType.European, new Dictionary<SwallowLoad, double>
                {
                    { SwallowLoad.None, 20 },
                    { SwallowLoad.Coconut, 16 }
                }
            }
        };

        public SwallowType Type { get; }
        public SwallowLoad Load { get; private set; } = SwallowLoad.None;

        public Swallow(SwallowType swallowType)
        {
            Type = swallowType;
        }

        public void ApplyLoad(SwallowLoad load)
        {
            Load = load;
        }

        public double GetAirspeedVelocity()
        {
            if (AirspeedVelocities.TryGetValue(Type, out var loadVelocities) &&
                loadVelocities.TryGetValue(Load, out var velocity))
            {
                return velocity;
            }
            throw new InvalidOperationException("Invalid swallow type or load.");
        }
    }
}
