using System;

namespace UnityEngine
{
    public static class Mathf
    {
        public static float Lerp(float a, float b, float t)
        {
            return (a * (1.0f - t) + b * t);
        }

        public static double Lerp(double a, double b, float t)
        {
            return (a * (1.0 - t) + b * t);
        }

        public static int Lerp(int a, int b, float t)
        {
            var v = Math.Round((double) a * (1.0 - t) + (double) b * t);
            return (int) v;
        }

        public static float Round(float a)
        {
            return (float) Math.Round(a);
        }

        public static int RoundToInt(float a)
        {
            return (int) Math.Round(a);
        }

        public static float Ceil(float a)
        {
            return (float) Math.Ceiling(a);
        }

        public static int CeilToInt(float a)
        {
            return (int) Math.Ceiling(a);
        }

        public static float Floor(float a)
        {
            return (float) Math.Floor(a);
        }

        public static int FloorToInt(float a)
        {
            return (int) Math.Floor(a);
        }
    }
}