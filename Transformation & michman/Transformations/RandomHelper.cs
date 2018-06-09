using System;

/// <summary> Helper class which contains various methods to generate a pseudo random in a specified way.</summary>
public static class RandomHelper
{ 
    public static Random Randomizer { get { return random; } }
    private static Random random = new Random();

    /// <summary>Returns a random floating point value between the given minimum and maximum</summary>
    /// <param name="minValue"> Minimum value of the return value.</param>
    /// <param name="maxValue"> Maximum value of the return value.</param>
    /// <returns> Random value between minValue and maxValue.</returns>
    public static float FloatBetween(float minValue, float maxValue)
    {
        if (minValue > maxValue) throw new Exception("Invalid arguments, the right hand value needs to be bigger or equal to the right hand value");

        return (float)random.NextDouble() * (maxValue - minValue) + minValue;
    }
}