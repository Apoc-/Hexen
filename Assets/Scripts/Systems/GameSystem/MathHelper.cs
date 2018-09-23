using System;

namespace Assets.Scripts.Systems.GameSystem
{
    public class MathHelper
    {
        private static readonly Random Random = new Random();

        public static float RandomFloat()
        {
            return (float) Random.NextDouble();
        }

        public static int RandomInt(int min, int max)
        {
            return Random.Next(min, max);
        }
    }
}