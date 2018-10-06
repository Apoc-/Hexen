﻿using System;
using System.Collections.Generic;

namespace Systems.GameSystem
{
    public static class MathHelper
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


        //extension method for List shuffeling
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}