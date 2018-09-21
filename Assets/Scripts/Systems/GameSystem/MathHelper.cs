using System;

namespace Assets.Scripts.Systems.GameSystem
{
    public class MathHelper
    {
        private static Random Random = new Random();

        public static float Float()
        {
            return (float) Random.NextDouble();
        }
    }
}